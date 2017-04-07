using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities.Managers;
using UniTime.Ratings.Dtos;
using UniTime.Ratings.Managers;

namespace UniTime.Ratings
{
    [AbpAuthorize]
    public class RatingAppService : UniTimeAppServiceBase, IRatingAppService
    {
        private readonly IActivityManager _activityManager;
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly IRatingManager _ratingManager;
        private readonly IRepository<Rating, long> _ratingRepository;

        public RatingAppService(
            IRepository<Rating, long> ratingRepository,
            IRatingManager ratingManager,
            IActivityManager activityManager,
            IActivityPlanManager activityPlanManager,
            IActivityTemplateManager activityTemplateManager)
        {
            _ratingRepository = ratingRepository;
            _ratingManager = ratingManager;
            _activityManager = activityManager;
            _activityPlanManager = activityPlanManager;
            _activityTemplateManager = activityTemplateManager;
        }

        public async Task<GetRatingsOutput> GetMyRatings()
        {
            var currentUserId = GetCurrentUserId();
            var ratings = await _ratingRepository.GetAllListAsync(rating => rating.OwnerId == currentUserId);

            return new GetRatingsOutput
            {
                Ratings = ratings.MapTo<List<RatingDto>>()
            };
        }

        public async Task<EntityDto<long>> CreateRating(CreateRatingInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            Rating rating = null;

            if (input.ActivityId.HasValue)
            {
                var activity = await _activityManager.GetAsync(input.ActivityId.Value);

                rating = await _ratingManager.CreateAsync(input.RatingStatus, activity, currentUser);
            }
            else if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId.Value);

                rating = await _ratingManager.CreateAsync(input.RatingStatus, activityPlan, currentUser);

            }
            else if (input.ActivityTemplateId.HasValue)
            {
                var activityTemplate = await _activityTemplateManager.GetAsync(input.ActivityTemplateId.Value);

                rating = await _ratingManager.CreateAsync(input.RatingStatus, activityTemplate, currentUser);
            }
            else
            {
                throw new UserFriendlyException("Please provide either activityId or activityPlanId or activityTemplateId.");
            }

            return new EntityDto<long>(rating.Id);
        }
    }
}