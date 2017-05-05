using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Activities;
using UniTime.Users.Dtos;

namespace UniTime.AbstractActivities.Dtos
{
    [AutoMapFrom(typeof(ActivityParticipant))]
    public class ActivityParticipantDto : EntityDto<long>
    {
        public UserListDto Owner { get; set; }
    }
}