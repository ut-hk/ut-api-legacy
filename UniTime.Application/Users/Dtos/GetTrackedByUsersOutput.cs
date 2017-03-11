using System.Collections.Generic;

namespace UniTime.Users.Dtos
{
    public class GetTrackedByUsersOutput
    {
        public IReadOnlyList<UserListDto> TrackedByUsers { get; set; }
    }
}