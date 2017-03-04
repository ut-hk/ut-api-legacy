using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Ratings.Dtos;
using UniTime.Ratings.Managers;

namespace UniTime.Ratings
{
    public class RatingAppService : UniTimeAppServiceBase, IRatingAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IRepository<ActivityPlan, Guid> _activityPlanRepository;
        private readonly IRatingManager _ratingManager;
        private readonly IRepository<Rating, Guid> _ratingRepository;

        public RatingAppService(
            IRepository<Rating, Guid> ratingRepository,
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<ActivityPlan, Guid> activityPlanRepository,
            IRatingManager ratingManager)
        {
            _ratingRepository = ratingRepository;
            _abstractActivityRepository = abstractActivityRepository;
            _activityPlanRepository = activityPlanRepository;
            _ratingManager = ratingManager;
        }

        public async Task<GetRatingsOutput> GetRatings()
        {
            var ratings = await _ratingRepository.GetAllListAsync();

            return new GetRatingsOutput
            {
                Ratings = ratings.MapTo<List<RatingDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateRating(CreateRatingInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            Rating rating = null;

            if (input.AbstractActivityId.HasValue)
            {
                var abstractActivity = await _abstractActivityRepository.FirstOrDefaultAsync(input.AbstractActivityId.Value);

                rating = await _ratingManager.CreateAsync(new AbstractActivityRating
                {
                    RatingStatus = input.RatingStatus,
                    AbstractActivity = abstractActivity,
                    AbstractActivityId = abstractActivity.Id,
                    Owner = currentUser,
                    OwnerId = currentUser.Id
                });
            }
            if (input.ActivityPlanId.HasValue)
            {
                var activityPlan = await _activityPlanRepository.FirstOrDefaultAsync(input.ActivityPlanId.Value);

                rating = await _ratingManager.CreateAsync(new ActivityPlanRating
                {
                    RatingStatus = input.RatingStatus,
                    ActivityPlan = activityPlan,
                    ActivityPlanId = activityPlan.Id,
                    Owner = currentUser,
                    OwnerId = currentUser.Id
                });
            }

            if (rating == null) throw new UserFriendlyException("Please provide either abstractActivityId or activityPlanId.");

            return new EntityDto<Guid>(rating.Id);
        }
    }
}