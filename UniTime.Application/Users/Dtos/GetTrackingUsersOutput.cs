using System.Collections.Generic;

namespace UniTime.Users.Dtos
{
    public class GetTrackingUsersOutput
    {
        public IReadOnlyList<UserListDto> TrackingUsers { get; set; }
    }
}