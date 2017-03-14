using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Ratings.Dtos;

namespace UniTime.Ratings
{
    public interface IRatingAppService : IApplicationService
    {
        Task<GetRatingsOutput> GetMyRatings();

        Task<EntityDto<long>> CreateRating(CreateRatingInput input);
    }
}