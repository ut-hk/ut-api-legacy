using System.Collections.Generic;
using UniTime.Users.Dtos;

namespace UniTime.Managements.Dtos
{
    public class GetUsersOutput
    {
        public ICollection<UserDto> Users { get; set; }
    }
}