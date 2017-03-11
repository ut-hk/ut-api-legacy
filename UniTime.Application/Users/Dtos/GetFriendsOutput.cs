using System.Collections.Generic;

namespace UniTime.Users.Dtos
{
    public class GetFriendsOutput
    {
        public IReadOnlyList<UserListDto> Friends { get; set; }
    }
}