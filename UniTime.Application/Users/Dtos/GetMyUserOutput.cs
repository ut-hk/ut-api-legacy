using System;

namespace UniTime.Users.Dtos
{
    public class GetMyUserOutput
    {
        public UserDto MyUser { get; set; }

        public Guid GuestId { get; set; }
    }
}