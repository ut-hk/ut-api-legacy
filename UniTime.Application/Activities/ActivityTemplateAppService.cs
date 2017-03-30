using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;
using UniTime.Descriptions.Enums;
using UniTime.Locations.Managers;
using UniTime.Tags;

namespace UniTime.Activities
{
    public class ActivityTemplateAppService : UniTimeAppServiceBase, IActivityTemplateAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Tag, long> _tagRepository;

        public ActivityTemplateAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Tag, long> tagRepository,
            IActivityTemplateManager activityTemplateManager,
            ILocationManager locationManager
        )
        {
            _abstractActivityRepository = abstractActivityRepository;
            _tagRepository = tagRepository;
            _activityTemplateManager = activityTemplateManager;
            _locationManager = locationManager;
        }

        public async Task<GetActivityTemplateOutput> GetActivityTemplate(EntityDto<Guid> input)
        {
            var activityTemplate = await _activityTemplateManager.GetAsync(input.Id);

            return new GetActivityTemplateOutput
            {
                ActivityTemplate = activityTemplate.MapTo<ActivityTemplateDto>()
            };
        }

        public async Task<GetActivityTemplatesOutput> GetActivityTemplates(GetActivityTemplatesInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var activityTemplates = await _abstractActivityRepository.GetAll()
                .OfType<ActivityTemplate>()
                .Include(activityTemplate => activityTemplate.Descriptions)
                .Include(activityTemplate => activityTemplate.Location)
                .Include(activityTemplate => activityTemplate.Tags)
                .Include(activityTemplate => activityTemplate.Ratings)
                .Include(activityTemplate => activityTemplate.Comments)
                .Include(activityTemplate => activityTemplate.Owner)
                .Include(activityTemplate => activityTemplate.ReferenceTimeSlots)
                .Where(activityTemplate => activityTemplate.ReferenceTimeSlots.FirstOrDefault().StartTime > DateTime.UtcNow)

                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityTemplate => input.TagTexts.Any(tagText => activityTemplate.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0, activityTemplate => queryKeywords.Any(queryKeyword => activityTemplate.Name.Contains(queryKeyword)))
                .WhereIf(input.Longitude.HasValue && input.Latitude.HasValue && input.Distance.HasValue,
                    activityTemplate => activityTemplate.Location.Coordinate.Distance(DbGeography.FromText($"POINT({input.Longitude.Value} {input.Latitude.Value})")) < ConvertToStandardDistance(input.Distance.Value))
                .WhereIf(input.StartTime.HasValue, activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.StartTime > input.StartTime))
                .WhereIf(input.EndTime.HasValue, activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.EndTime < input.EndTime))
                .OrderBy(activityTemplate => activityTemplate.ReferenceTimeSlots.FirstOrDefault().StartTime)
                .PageBy(input)
                .ToListAsync();

            var activityTemplateDtos = activityTemplates.MapTo<List<ActivityTemplateDto>>();

            foreach (var activityTemplateDto in activityTemplateDtos)
            {
                activityTemplateDto.CoverImageDescription = activityTemplateDto.Descriptions
                    .FirstOrDefault(at => at.Type == DescriptionType.ExternalImage || at.Type == DescriptionType.InternalImage);
            }

            return new GetActivityTemplatesOutput
            {
                ActivityTemplates = activityTemplateDtos
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;

            var activityTemplate = await _activityTemplateManager.CreateAsync(ActivityTemplate.Create(
                input.Name,
                location,
                input.ReferenceTimeSlots.Select(timeSlot => ActivityTemplateReferenceTimeSlot.Create(timeSlot.StartTime, timeSlot.EndTime)).ToList(),
                currentUser,
                input.ReferenceId
            ));

            return new EntityDto<Guid>(activityTemplate.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivityTemplate(UpdateActivityTemplateInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityTemplate = await _activityTemplateManager.GetAsync(input.Id);
            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;
            var tags = await _tagRepository.GetAllListAsync(tag => input.TagIds.Contains(tag.Id));

            _activityTemplateManager.EditActivityTemplate(
                activityTemplate,
                input.Name,
                input.ReferenceTimeSlots.Select(timeSlot => ActivityTemplateReferenceTimeSlot.Create(timeSlot.StartTime, timeSlot.EndTime)).ToList(),
                location,
                tags,
                currentUserId
            );
            _activityTemplateManager.EditDescriptions(activityTemplate, input.DescriptionIds, currentUserId);
        }

        private static double ConvertToStandardDistance(double distanceInMile)
        {
            return distanceInMile / 1609.344;
        }
    }
}