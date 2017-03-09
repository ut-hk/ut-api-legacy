using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using UniTime.Analysis.Dtos;
using UniTime.Analysis.Managers;

namespace UniTime.Analysis
{
    public class AnalysisAppService : UniTimeAppServiceBase, IAnalysisAppService
    {
        private readonly IGuestManager _guestManager;
        private readonly IRouteHistoryManager _routeHistoryManager;

        public AnalysisAppService(
            IGuestManager guestManager,
            IRouteHistoryManager routeHistoryManager)
        {
            _guestManager = guestManager;
            _routeHistoryManager = routeHistoryManager;
        }

        public async Task<EntityDto<long>> CreateRouteHistory(CreateRouteHistoryInput input)
        {
            var guest = await _guestManager.GetAsync(input.GuestId);

            var routeHistory = await _routeHistoryManager.CreateAsync(RouteHistory.Create(input.RouteName, input.Parameters, input.Referer, guest));

            return new EntityDto<long>(routeHistory.Id);
        }

        public async Task<EntityDto<Guid>> CreateGuest()
        {
            var currentUserId = AbpSession.UserId;

            var guest = currentUserId.HasValue
                ? await _guestManager.GetByUserIdAsync(currentUserId.Value)
                : await _guestManager.CreateAsync();

            return new EntityDto<Guid>(guest.Id);
        }
    }
}