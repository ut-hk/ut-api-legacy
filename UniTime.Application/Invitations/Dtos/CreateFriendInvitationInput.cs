namespace UniTime.Invitations.Dtos
{
    public class CreateFriendInvitationInput
    {
        public string Content { get; set; }

        public long InviteeId { get; set; }
    }
}