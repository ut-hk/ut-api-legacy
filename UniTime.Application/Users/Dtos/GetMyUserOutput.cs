using System;

namespace UniTime.Users.Dtos
{
    public class GetMyUserOutput
    {
        public MyUserDto MyUser { get; set; }

        public Guid GuestId { get; set; }

        public int NumberOfActivityInvitations { get; set; }

        public int NumberOfFriendInvitations { get; set; }

        public int NumberOfFriends { get; set; }
    }
}