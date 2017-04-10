namespace UniTime.Users.Dtos
{
    public class GetUserOutput
    {
        public UserDto User { get; set; }

        public int NumberOfFriends { get; set; }

        public bool IsFriend { get; set; }

        public bool HasInvited { get; set; }
    }
}