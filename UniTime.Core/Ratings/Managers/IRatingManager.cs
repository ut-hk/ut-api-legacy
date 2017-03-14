using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Ratings.Managers
{
    public interface IRatingManager : IDomainService
    {
        Task<Rating> GetAsync(long id);

        Task<Rating> CreateAsync(Rating rating);
    }
}