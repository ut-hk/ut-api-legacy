using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper.QueryableExtensions;
using UniTime.Activities;
using UniTime.Activities.Managers;
using UniTime.ActivityPlans.Dtos;
using UniTime.Descriptions;
using UniTime.Descriptions.Dtos;
using UniTime.Ratings;
using UniTime.Ratings.Enums;
using UniTime.Tags.Managers;

namespace UniTime.ActivityPlans
{
    public class ActivityPlanAppService : UniTimeAppServiceBase, IActivityPlanAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;
        private readonly IRepository<Description, long> _descriptionRepository;
        private readonly IRepository<Rating, long> _ratingRepository;
        private readonly ITagManager _tagManager;

        public ActivityPlanAppService(
            IRepository<ActivityPlan, Guid> activityPlanRepository,
            IRepository<Rating, long> ratingRepository,
            IRepository<Description, long> descriptionRepository,
            IActivityPlanManager activityPlanManager,
            ITagManager tagManager)
        {
            _activityPlanRepository = activityPlanRepository;
            _ratingRepository = ratingRepository;
            _descriptionRepository = descriptionRepository;
            _activityPlanManager = activityPlanManager;
            _tagManager = tagManager;
        }

        public async Task<GetActivityPlanOutput> GetActivityPlan(EntityDto<Guid> input)
        {
            var currentUserId = AbpSession.UserId;

            var activityPlan = await _activityPlanRepository.GetAll()
                .Include(a => a.Descriptions)
                .Include(a => a.Tags)
                .Include(a => a.Ratings)
                .Include(a => a.Comments)
                .Include(a => a.Owner)
                .Include(a => a.TimeSlots)
                .FirstOrDefaultAsync(a => a.Id == input.Id);

            if (activityPlan == null)
                throw new UserFriendlyException(_activityPlanManager.DoesNotExistMessage);

            var activtyPlanDto = activityPlan.MapTo<ActivityPlanDto>();

            activtyPlanDto.Likes = await _ratingRepository.GetAll().LongCountAsync(r => r.ActivityPlanId == activtyPlanDto.Id && r.RatingStatus == RatingStatus.Like);

            if (currentUserId.HasValue)
            {
                var myRatingStatus = await _ratingRepository.GetAll().Where(r => r.ActivityPlanId == activtyPlanDto.Id && r.OwnerId == currentUserId.Value).Select(r => r.RatingStatus).FirstOrDefaultAsync();
                activtyPlanDto.MyRatingStatus = myRatingStatus;
            }
            else
            {
                activtyPlanDto.MyRatingStatus = null;
            }

            return new GetActivityPlanOutput
            {
                ActivityPlan = activtyPlanDto
            };
        }

        public async Task<GetActivityPlansOutput> GetActivityPlans(GetActivityPlansInput input)
        {
            var queryKeywords = input.QueryKeywords?.Split(' ').Where(queryKeyword => queryKeyword.Length > 0).ToArray();

            var activityPlans = await _activityPlanRepository.GetAll()
                .Include(activityPlan => activityPlan.Tags)
                .Include(activityPlan => activityPlan.Owner)

                // Optional Wheres
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityPlan => input.TagTexts.Any(tagText => activityPlan.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(queryKeywords != null && queryKeywords.Length > 0,
                    activityPlan => queryKeywords.Any(queryKeyword => activityPlan.Name.Contains(queryKeyword)))
                .WhereIf(input.UserId.HasValue,
                    activityPlan => activityPlan.OwnerId == input.UserId.Value)

                // View Requirements
                .OrderByDescending(activityPlan => activityPlan.CreationTime)
                .ProjectTo<ActivityPlanListDto>()
                .PageBy(input)
                .ToListAsync();

            await InjectCoverDescriptionAsync(activityPlans);
            await InjectLikesAsync(activityPlans);
            await InjectMyRatingStatusAsync(activityPlans);

            return new GetActivityPlansOutput
            {
                ActivityPlans = activityPlans
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityPlan(CreateActivityPlanInput input)
        {
            var currentUser = await GetCurrentUserAsync();
            var tags = await _tagManager.GetTags(input.TagTexts);

            var activityPlan = await _activityPlanManager.CreateAsync(ActivityPlan.Create(
                input.Name,
                tags,
                currentUser
            ));

            return new EntityDto<Guid>(activityPlan.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivityPlan(UpdateActivityPlanInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.Id);
            var tags = await _tagManager.GetTags(input.TagTexts);

            _activityPlanManager.EditActivityPlan(activityPlan, input.Name, tags, currentUserId);
            _activityPlanManager.EditDescriptions(activityPlan, input.DescriptionIds, currentUserId);
        }

        [AbpAuthorize]
        public async Task RemoveActivityPlan(EntityDto<Guid> input)
        {
            var currentUserId = GetCurrentUserId();
            var activityPlan = await _activityPlanManager.GetAsync(input.Id);

            await _activityPlanManager.RemoveAsync(activityPlan, currentUserId);
        }

        private async Task InjectCoverDescriptionAsync(ICollection<ActivityPlanListDto> activityPlanListDtos)
        {
            var acitivtyTemplateIds = activityPlanListDtos.Select(activity => activity.Id);

            var activityImageDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is ExternalImageDescription || d is InternalImageDescription)
                .Where(description => description.ActivityPlan != null && acitivtyTemplateIds.Contains(description.ActivityPlanId.Value))
                .GroupBy(description => description.ActivityPlanId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            var activityTextDescriptionDictionary = await _descriptionRepository.GetAll()
                .Where(d => d is TextDescription)
                .Where(description => description.ActivityPlanId != null && acitivtyTemplateIds.Contains(description.ActivityPlanId.Value))
                .GroupBy(description => description.ActivityPlanId.Value)
                .Where(descriptionGroup => descriptionGroup.Any())
                .Select(descriptionGroup => new {descriptionGroup.Key, description = descriptionGroup.FirstOrDefault()})
                .ToDictionaryAsync(a => a.Key, a => a.description);

            foreach (var activityTemplateListDto in activityPlanListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (activityImageDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverImageDescription = activityImageDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();

                if (activityTextDescriptionDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.CoverTextDescription = activityTextDescriptionDictionary[activityTemplateId].MapTo<DescriptionDto>();
            }
        }

        private async Task InjectLikesAsync(ICollection<ActivityPlanListDto> activityPlanListDtos)
        {
            var activityTemplateIds = activityPlanListDtos.Select(activity => activity.Id);

            var likesDictionary = await _ratingRepository.GetAll()
                .Where(rating => rating.ActivityPlanId != null && activityTemplateIds.Contains(rating.ActivityPlanId.Value))
                .GroupBy(rating => rating.ActivityPlanId)
                .Select(ratingGroup => new {ratingGroup.Key, Count = ratingGroup.LongCount(r => r.RatingStatus == RatingStatus.Like)})
                .ToDictionaryAsync(rating => rating.Key, ratings => ratings.Count);

            foreach (var activityTemplateListDto in activityPlanListDtos)
            {
                var activityTemplateId = activityTemplateListDto.Id;

                if (likesDictionary.ContainsKey(activityTemplateId))
                    activityTemplateListDto.Likes = likesDictionary[activityTemplateId];
            }
        }

        private async Task InjectMyRatingStatusAsync(ICollection<ActivityPlanListDto> activityPlanListDtos)
        {
            var currentUserId = AbpSession.UserId;

            if (currentUserId.HasValue)
            {
                var activityTemplateIds = activityPlanListDtos.Select(activity => activity.Id);

                var activityRatingStatusDictionary = await _ratingRepository.GetAll()
                    .Where(rating => rating.OwnerId == currentUserId.Value && rating.ActivityPlanId != null && activityTemplateIds.Contains(rating.ActivityPlanId.Value))
                    .GroupBy(rating => rating.ActivityPlanId.Value)
                    .Where(ratingGroup => ratingGroup.Any())
                    .Select(ratingGroup => new {ratingGroup.Key, ratingGroup.FirstOrDefault().RatingStatus})
                    .ToDictionaryAsync(ratingGroup => ratingGroup.Key, ratingGroup => ratingGroup.RatingStatus);

                foreach (var activityTemplateListDto in activityPlanListDtos)
                {
                    var activityTemplateId = activityTemplateListDto.Id;

                    if (activityRatingStatusDictionary.ContainsKey(activityTemplateId))
                        activityTemplateListDto.MyRatingStatus = activityRatingStatusDictionary[activityTemplateId];
                }
            }
        }
    }
}