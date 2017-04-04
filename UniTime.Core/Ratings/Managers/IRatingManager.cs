using System;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Activities;
using UniTime.Ratings.Enums;
using UniTime.Users;

namespace UniTime.Ratings.Managers
{
    public interface IRatingManager : IDomainService
    {
        Task<Rating> GetAsync(long id);

        Task<Rating> CreateAsync(RatingStatus ratingStatus, Activity activity, User owner);
        Task<Rating> CreateAsync(RatingStatus ratingStatus, ActivityTemplate activityTemplate, User owner);
        Task<Rating> CreateAsync(RatingStatus ratingStatus, ActivityPlan activityPlan, User owner);

        void EditRating(Rating rating, RatingStatus ratingStatus, long editUserId);
    }
}