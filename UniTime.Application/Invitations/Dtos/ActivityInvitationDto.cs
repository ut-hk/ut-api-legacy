using Abp.AutoMapper;
using UniTime.AbstractActivities.Dtos;
using UniTime.Activities;

namespace UniTime.Invitations.Dtos
{
    [AutoMapFrom(typeof(ActivityInvitation))]
    public class ActivityInvitationDto : InvitationDto
    {
        public ActivityDto Activity { get; set; }
    }
}