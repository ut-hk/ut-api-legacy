using System;

namespace UniTime.Invitations.Dtos
{
    public class CreateActivityInvitationsInput
    {
        public string Content { get; set; }

        public long[] InviteeIds { get; set; }

        public Guid ActivityId { get; set; }
    }
}