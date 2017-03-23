using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Analysis.Dtos;

namespace UniTime.Analysis
{
    public interface IAnalysisAppService : IApplicationService
    {
        Task<EntityDto<Guid>> CreateGuest();
        Task<EntityDto<long>> CreateRouteHistory(CreateRouteHistoryInput input);

        Task MergeGuestWithOwner(EntityDto<Guid> input);
    }
}