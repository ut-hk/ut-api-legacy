using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Activities;
using UniTime.Comments.Dtos;

namespace UniTime.Comments
{
    public interface IReplyAppService : IApplicationService
    {
        Task<EntityDto<long>> CreateReply(CreateReplyInput input);
    }
}