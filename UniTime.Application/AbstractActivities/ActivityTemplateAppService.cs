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
using Abp.UI;
using AutoMapper.QueryableExtensions;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;
using UniTime.Activities.Managers;
using UniTime.Descriptions;
using UniTime.Descriptions.Dtos;
using UniTime.Locations.Managers;
using UniTime.Ratings;
using UniTime.Ratings.Enums;
using UniTime.Tags.Managers;

namespace UniTime.AbstractActivities
{
    public class ActivityTemplateAppService : UniTimeAppServiceBase, IActivityTemplateAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly IRepository<ActivityTemplateReferenceTimeSlot, long> _activityTemplateReferenceTimeSlotRepository;
        private readonly IRepository<Description, long> _descriptionRepository;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Rating, long> _ratingRepository;
        private readonly ITagManager _tagManager;

        public ActivityTemplateAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Description, long> descriptionRepository,
            IRepository<ActivityTemplateReferenceTimeSlot, long> activityTemplateReferenceTimeSlotRepository,
            IActivityTemplateManager activityTemplateManager,
            ILocationManager locationManager,
            IRepository<Rating, long> ratingRepository,
            ITagManager tagManager
        )
        {
            _abstractActivityRepository = abstractActivityRepository;
            _descriptionRepository = descriptionRepository;
            _activityTemplateReferenceTimeSlotRepository = activityTemplateReferenceTimeSlotRepository;
            _activityTemplateManager = activityTemplateManager;
            _locationManager = locationManager;
            _ratingRepository = ratingRepository;
            _tagManager = tagManager;
        }

        public async Task<GetActivityTemplateOutput> GetActivityTemplate(EntityDto<Guid> input)
        {
            var activityTemplate = await _abstractActivityRepository.GetAll()
                .OfType<ActivityTemplate>()
                .Include(at => at.Descriptions)
                .Include(at => at.Location)
                .Include(at => at.Tags)
                .Include(at => at.Ratings)
                .Include(at => at.Comments)
                .Include(at => at.Owner)
                .Include(at => at.ReferenceTimeSlots)
                .FirstOrDefaultAsync(at => at.Id == input.Id);

            if (activityTemplate == null)
                throw new UserFriendlyException(_activityTemplateManager.DoesNotExistMessage);

            return new GetActivityTemplateOutput
            {
                ActivityTemplate = activityTemplate.MapTo<ActivityTemplateDto>()
            };
        }

        public async Task<GetActivityTemplatesOutput> GetActivityTemplates(GetActivityTemplatesInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var dbGeography = input.Longitude.HasValue && input.Latitude.HasValue ? CreatePoint(input.Latitude.Value, input.Longitude.Value) : null;

            var activityTemplateListDtosQueryable = _abstractActivityRepository.GetAll()
                .OfType<ActivityTemplate>()
                .Include(activityTemplate => activityTemplate.Location)
                .Include(activityTemplate => activityTemplate.Tags)
                .Include(activityTemplate => activityTemplate.Owner)
                .Where(activityTemplate => activityTemplate.ReferenceTimeSlots.Any(referenceTimeSlot => referenceTimeSlot.StartTime > DateTime.UtcNow))

                // Optional Wheres
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityTemplate => input.TagTexts.Any(tagText => activityTemplate.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0,
                    activityTemplate => queryKeywords.Any(queryKeyword => activityTemplate.Name.Contains(queryKeyword)))
                .WhereIf(input.StartTime.HasValue,
                    activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.StartTime > input.StartTime))
                .WhereIf(input.EndTime.HasValue,
                    activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.EndTime < input.EndTime))
                .WhereIf(input.UserId.HasValue,
                    activityTemplate => activityTemplate.OwnerId == input.UserId.Value);

            if (dbGeography != null)
                activityTemplateListDtosQueryable = activityTemplateListDtosQueryable
                    .Where(activityTemplate => activityTemplate.LocationId.HasValue)
                    .OrderBy(activityTemplate => activityTemplate.Location.Coordinate.Distance(dbGeography))
                    .ThenBy(activityTemplate => activityTemplate.ReferenceTimeSlots.FirstOrDefault().StartTime);
            else
                activityTemplateListDtosQueryable = activityTemplateListDtosQueryable
                    .OrderBy(activityTemplate => activityTemplate.ReferenceTimeSlots.FirstOrDefault().StartTime);

            // View Requirements
            var activityTemplateListDtos = await activityTemplateListDtosQueryable
                .PageBy(input)
                .ProjectTo<ActivityTemplateListDto>()
                .ToListAsync();

            await InjectCoverDescriptionAsync(activityTemplateListDtos);
            await InjectLikesAsync(activityTemplateListDtos);
            await InjectMyRatingStatusAsync(activityTemplateListDtos);
            await InjectStartTime(activityTemplateListDtos);

            return new GetActivityTemplatesOutput
            {
                ActivityTemplates = activityTemplateListDtos
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;

            var tags = await _tagManager.GetTags(input.TagTexts);

            var activityTemplate = await _activityTemplateManager.CreateAsync(ActivityTemplate.Create(
                input.Name,
                location,
                input.ReferenceTimeSlots.Select(timeSlot => ActivityTemplateReferenceTimeSlot.Create(timeSlot.StartTime, timeSlot.EndTime)).ToList(),
                tags,
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
            var tags = await _tagManager.GetTags(input.TagTexts);

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

        [AbpAuthorize]
        public async Task RemoveActivityTemplate(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var activityTemplate = await _activityTemplateManager.GetAsync(input.Id);

            await _activityTemplateManager.RemoveAsync(activityTemplate, currentUserId);
        }

        public static DbGeography CreatePoint(double latitude, double longitude, int srid = 4326)
        {
            return DbGeography.PointFromText($"POINT({longitude} {latitude})", srid);
        }

        private async Task InjectMyRatingStatusAsync(ICollection<ActivityTemplateListDto> activityTemplateListDtos)
        {
            var currentUserId = AbpSession.UserId;

            if (currentUserId.HasValue)
            {
                var activityTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

                var activityRatingStatusDictionary = await _ratingRepository.GetAll()
                    .Where(rating => rating.OwnerId == currentUserId.Value && rating.AbstractActivityId != null && activityTemplateIds.Contains(rating.AbstractActivityId.Value))
                    .GroupBy(rating => rating.AbstractActivityId.Value)
                    .Where(ratingGroup => ratingGroup.Any())
                    .Select(ratingGroup => new {ratingGroup.Key, ratingGroup.FirstOrDefault().RatingStatus})
                    .ToDictionaryAsync(ratingGroup => ratingGroup.Key, ratingGroup => ratingGroup.RatingStatus);

                foreach (var activityTemplateListDto in activityTemplateListDtos)
                {
                    var activityTemplateId = activityTemplateListDto.Id;

                    if (activityRatingStatusDictionary.ContainsKey(activityTemplateId))
                        activityTemplateListDto.MyRatingStatus = activityRatingStatusDictionary[activityTemplateId];
                }
            }
        }

        private async Task InjectLikesAsync(ICollection<ActivityTemplateListDto> activityTemplateListDtos)
        {
            var activityTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

            var likesDictionary = await _ratingRepository.GetAll()
                .Where(rating => rating.AbstractActivityId != null && activityTemplateIds.Contains(rating.AbstractActivityId.Value))
                .GroupBy(rating => rating.AbstractActivityId)
                .Select(ratingGroup => new {ratingGroup.Key, Count = ratingGroup.LongCount(r => r.RatingStatus == RatingStatus.Like)})
                .ToDictionaryAsync(rating => rating.Key, ratings => ratings.Count);

            foreach (var activityTemplateListDto in activityTemplateListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (likesDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.Likes = likesDictionary[activityTemplateId];
            }
        }

        private async Task InjectCoverDescriptionAsync(ICollection<ActivityTemplateListDto> activityTemplateListDtos)
        {
            var acitivtyTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

            var activityImageDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is ExternalImageDescription || d is InternalImageDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyTemplateIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            var activityTextDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is TextDescription)
                .Where(description => description.AbstractActivityId != null && acitivtyTemplateIds.Contains(description.AbstractActivityId.Value))
                .GroupBy(description => description.AbstractActivityId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            foreach (var activityTemplateListDto in activityTemplateListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (activityImageDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverImageDescription = activityImageDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();

                if (activityTextDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverTextDescription = activityTextDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();
            }
        }

        private async Task InjectStartTime(ICollection<ActivityTemplateListDto> activityTemplateListDtos)
        {
            var activityTemplateIds = activityTemplateListDtos.Select(activity => activity.Id);

            var activityTemplateStartTimeMap = await _activityTemplateReferenceTimeSlotRepository.GetAll()
                .Where(timeSlot => activityTemplateIds.Contains(timeSlot.ActivityTemplateId))
                .Select(timeSlot => new {timeSlot.ActivityTemplateId, timeSlot.StartTime})
                .GroupBy(timeSlot => timeSlot.ActivityTemplateId)
                .Where(timeSlotGroup => timeSlotGroup.Any(timeSlot => timeSlot.StartTime.HasValue))
                .Select(timeSlotGroup => new {timeSlotGroup.Key, timeSlotGroup.FirstOrDefault().StartTime})
                .ToDictionaryAsync(timeSlotGroup => timeSlotGroup.Key, timeSlotGroup => timeSlotGroup.StartTime);

            foreach (var activityTemplateListDto in activityTemplateListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (activityTemplateStartTimeMap.ContainsKey(activityTemplateId))
                    activityTemplateListDto.StartTime = activityTemplateStartTimeMap[activityTemplateId];
            }
        }
    }
}