using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings.Managers
{
    public class RatingManager : IRatingManager
    {
        private readonly IRepository<Rating, long> _ratingRepository;

        public RatingManager(
            IRepository<Rating, long> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<Rating> GetAsync(long id)
        {
            var rating = await _ratingRepository.FirstOrDefaultAsync(id);

            if (rating == null)
                throw new UserFriendlyException("The rating with id = " + id + " does not exist.");

            return rating;
        }

        public async Task<Rating> CreateAsync(RatingStatus ratingStatus, Activity activity, User owner)
        {
            var rating = activity.Ratings.FirstOrDefault(r => r.OwnerId == owner.Id && r.AbstractActivityId == activity.Id);

            if (rating != null)
                rating.EditRating(ratingStatus, owner.Id);
            else
                rating = await CreateAsync(Rating.Create(ratingStatus, activity, owner));

            return rating;
        }

        public async Task<Rating> CreateAsync(RatingStatus ratingStatus, ActivityTemplate activityTemplate, User owner)
        {
            var rating = activityTemplate.Ratings.FirstOrDefault(r => r.OwnerId == owner.Id && r.AbstractActivityId == activityTemplate.Id);

            if (rating != null)
                rating.EditRating(ratingStatus, owner.Id);
            else
                rating = await CreateAsync(Rating.Create(ratingStatus, activityTemplate, owner));

            return rating;
        }

        public async Task<Rating> CreateAsync(RatingStatus ratingStatus, ActivityPlan activityPlan, User owner)
        {
            var rating = activityPlan.Ratings.FirstOrDefault(r => r.OwnerId == owner.Id && r.ActivityPlanId == activityPlan.Id);

            if (rating != null)
                rating.EditRating(ratingStatus, owner.Id);
            else
                rating = await CreateAsync(Rating.Create(ratingStatus, activityPlan, owner));
            
            return rating;
        }

        public void EditRating(Rating rating, RatingStatus ratingStatus, long editUserId)
        {
            rating.EditRating(ratingStatus, editUserId);
        }

        private async Task<Rating> CreateAsync(Rating rating)
        {
            rating.Id = await _ratingRepository.InsertAndGetIdAsync(rating);

            return rating;
        }
    }
}