using System.Collections.Generic;

namespace UniTime.Users.Dtos
{
    public class GetInterTrackingUsersOutput
    {
        public IReadOnlyList<UserListDto> InterTrackingUsers { get; set; }
    }
}