using System.Collections.Generic;

namespace UniTime.Invitations.Dtos
{
    public class GetFriendInvitationsOutput
    {
        public IReadOnlyList<FriendInvitationDto> FriendInvitations { get; set; }
    }
}