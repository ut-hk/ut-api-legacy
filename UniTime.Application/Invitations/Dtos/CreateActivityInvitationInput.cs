using System;

namespace UniTime.Invitations.Dtos
{
    public class CreateActivityInvitationInput
    {
        public string Content { get; set; }

        public long InviteeId { get; set; }

        public Guid ActivityId { get; set; }
    }
}