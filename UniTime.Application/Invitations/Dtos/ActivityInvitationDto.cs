using Abp.AutoMapper;
using UniTime.Activities;
using UniTime.Activities.Dtos;

namespace UniTime.Invitations.Dtos
{
    [AutoMapFrom(typeof(ActivityInvitation))]
    public class ActivityInvitationDto : InvitationDto
    {
        public ActivityDto Activity { get; set; }
    }
}