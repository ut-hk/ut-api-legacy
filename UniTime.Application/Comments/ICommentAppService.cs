using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Comments.Dtos;

namespace UniTime.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<EntityDto<Guid>> CreateTextComment(CreateTextCommentInput input);

        Task<EntityDto<Guid>> CreateInternalImageComment(CreateInternalImageCommentInput input);
    }
}