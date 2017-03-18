using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Descriptions.Dtos;

namespace UniTime.Descriptions
{
    public interface IDescriptionAppService : IApplicationService
    {
        Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input);
        Task<EntityDto<long>> CreateExternalImageDescription(CreateExternalImageDescriptionInput input);
        Task<EntityDto<long>> CreateInternalImageDescription(CreateInternalImageDescriptionInput input);
        Task<EntityDto<long>> CreateYoutubeDescription(CreateYoutubeDescriptionInput input);

        Task UpdateDescription(UpdateDescriptionInput input);
        Task UpdateTextDescription(UpdateTextDescriptionInput input);

        Task RemoveDescription(EntityDto<long> input);
    }
}