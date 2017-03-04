using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Comments.Dtos;

namespace UniTime.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<GetCommentsOutput> GetComments();

        Task<EntityDto<long>> CreateComment(CreateCommentInput input);
    }
}