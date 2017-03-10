using Abp.AutoMapper;

namespace UniTime.Invitations.Dtos
{
    [AutoMapFrom(typeof(FriendInvitation))]
    public class FriendInvitationDto : InvitationDto
    {
    }
}