using System.Threading.Tasks;
using Abp.Application.Services;
using UniTime.Users.Dtos;

namespace UniTime.Users
{
    public interface ITrackAppService : IApplicationService
    {
        Task<GetTrackingUsersOutput> GetTrackingUsers(TargetUserInput input);
        Task<GetTrackedByUsersOutput> GetTrackedByUsers(TargetUserInput input);
        Task<GetFriendsOutput> GetFriends(TargetUserInput input);

        Task Track(TargetUserInput input);
    }
}