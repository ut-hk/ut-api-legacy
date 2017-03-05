using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Descriptions.Dtos;

namespace UniTime.Descriptions
{
    public interface IDescriptionAppService : IApplicationService
    {
        Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input);

        Task UpdateTextDescription(UpdateTextDescriptionInput input);
    }
}