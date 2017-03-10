using System.Collections.Generic;

namespace UniTime.Invitations.Dtos
{
    public class GetActivityInvitationsOutput
    {
        public IReadOnlyList<ActivityInvitationDto> ActivityInvitations { get; set; }
    }
}