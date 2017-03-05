using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Tags.Dtos;

namespace UniTime.Tags
{
    public interface ITagAppService : IApplicationService
    {
        Task<EntityDto<long>> GetTag(GetTagInput input);
        Task<GetTagsOutput> GetTags(GetTagsInput input);
    }
}