using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UniTime.Analysis.Dtos;

namespace UniTime.Analysis
{
    public interface IAnalysisAppService : IApplicationService
    {
        Task<EntityDto<Guid>> GetGuest(GetGuestInput input);
        Task<GetAnalysisInsightsOutput> GetAnaylsisInsights();
        Task<GeSocialGraphOutput> GetSocialGraph();

        Task<EntityDto<long>> CreateRouteHistory(CreateRouteHistoryInput input);
        Task CreateLocationHistory(CreateLocationHistoryInput input);
    }
}