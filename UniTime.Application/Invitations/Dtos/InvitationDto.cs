using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UniTime.Invitations.Enums;
using UniTime.Users.Dtos;

namespace UniTime.Invitations.Dtos
{
    [AutoMapFrom(typeof(Invitation))]
    public class InvitationDto : EntityDto<Guid>
    {
        public string Content { get; set; }

        public InvitationStatus Status { get; set; }

        public UserListDto Invitee { get; set; }

        public UserListDto Owner { get; set; }
    }
}