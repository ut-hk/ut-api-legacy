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

namespace UniTime.Activities
{
    public class ActivityTemplateAppService : UniTimeAppServiceBase, IActivityTemplateAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityTemplateManager _activityTemplateManager;

        public ActivityTemplateAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IActivityTemplateManager activityTemplateManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _activityTemplateManager = activityTemplateManager;
        }

        public async Task<GetActivityTemplatesOutput> GetActivityTemplates(GetActivityTemplatesInput input)
        {
            var selectedGeographyPoint = $"POINT({input.Longitude} {input.Latitude})";
            var targetRadiusInStandardDistance = ConvertToStandardDistance(100);

            var activityTemplates = await _abstractActivityRepository.GetAll().OfType<ActivityTemplate>()
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityTemplate => input.TagTexts.Any(tagText => activityTemplate.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(input.Longitude.HasValue && input.Latitude.HasValue,
                    activityTemplate => activityTemplate.Location.Coordinate.Distance(DbGeography.FromText(selectedGeographyPoint)) < targetRadiusInStandardDistance)
                .WhereIf(input.StartTime.HasValue, activityTempalte => activityTempalte.ReferenceStartTime > input.StartTime)
                .WhereIf(input.EndTime.HasValue, activityTempalte => input.EndTime > activityTempalte.ReferenceEndTime)
                .ToListAsync();

            return new GetActivityTemplatesOutput
            {
                ActivityTemplates = activityTemplates.MapTo<List<ActivityTemplateDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityTemplate = await _activityTemplateManager.CreateAsync(ActivityTemplate.Create(
                input.Name,
                input.Description,
                input.ReferenceStarTime,
                input.ReferenceEndTime,
                currentUser
            ));

            return new EntityDto<Guid>(activityTemplate.Id);
        }

        public async Task UpdateActivityTemplate(UpdateAbstractActivityInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activity = await _activityTemplateManager.GetAsync(input.Id);

            activity.Edit(input.Name, input.Description, currentUserId);
        }

        private static double ConvertToStandardDistance(double distanceInMile)
        {
            return distanceInMile / 1609.344;
        }
    }
}