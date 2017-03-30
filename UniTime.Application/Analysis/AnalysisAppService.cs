using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
using UniTime.Analysis.Dtos;
using UniTime.Analysis.Managers;

namespace UniTime.Analysis
{
    public class AnalysisAppService : UniTimeAppServiceBase, IAnalysisAppService
    {
        private readonly IGuestManager _guestManager;
        private readonly ILocationHistoryManager _locationHistoryManager;
        private readonly IRepository<LocationHistory, long> _locationHistoryRepository;
        private readonly IRouteHistoryManager _routeHistoryManager;
        private readonly IRepository<RouteHistory, long> _routeHistoryRepository;

        public AnalysisAppService(
            IRepository<RouteHistory, long> routeHistoryRepository,
            IRepository<LocationHistory, long> locationHistoryRepository,
            IGuestManager guestManager,
            IRouteHistoryManager routeHistoryManager,
            ILocationHistoryManager locationHistoryManager)
        {
            _routeHistoryRepository = routeHistoryRepository;
            _locationHistoryRepository = locationHistoryRepository;
            _guestManager = guestManager;
            _routeHistoryManager = routeHistoryManager;
            _locationHistoryManager = locationHistoryManager;
        }

        public async Task<EntityDto<Guid>> GetGuest(GetGuestInput input)
        {
            var currentUserId = AbpSession.UserId;

            Guest guest;

            if (currentUserId.HasValue)
            {
                guest = await _guestManager.GetByUserIdAsync(currentUserId.Value);

                await CheckGuestIdAsync(guest, input.GuestId);
            }
            else
            {
                guest = await _guestManager.CreateAsync();
            }

            return new EntityDto<Guid>(guest.Id);
        }

        [AbpAuthorize]
        public async Task<GetAnalysisInsightsOutput> GetAnaylsisInsights()
        {
            var routeHistories = await _routeHistoryRepository.GetAll()
                .ProjectTo<RouteHistoryDto>()
                .ToListAsync();

            var locationHistories = await _locationHistoryRepository.GetAll()
                .ProjectTo<LocationHistoryDto>()
                .ToListAsync();

            return new GetAnalysisInsightsOutput
            {
                RouteHistories = routeHistories,
                LocationHistories = locationHistories
            };
        }

        public async Task<EntityDto<long>> CreateRouteHistory(CreateRouteHistoryInput input)
        {
            var currentUserId = AbpSession.UserId;

            Guest guest;
            if (currentUserId.HasValue)
            {
                guest = await _guestManager.GetByUserIdAsync(currentUserId.Value);

                await CheckGuestIdAsync(guest, input.GuestId);
            }
            else
            {
                guest = await _guestManager.GetAnonymousGuestAsync(input.GuestId);
            }

            var routeHistory = await _routeHistoryManager.CreateAsync(RouteHistory.Create(input.RouteName, input.Parameters, input.Referer, guest, currentUserId));

            return new EntityDto<long>(routeHistory.Id);
        }

        public async Task CreateLocationHistory(CreateLocationHistoryInput input)
        {
            var currentUserId = AbpSession.UserId;

            Guest guest;
            if (currentUserId.HasValue)
            {
                guest = await _guestManager.GetByUserIdAsync(currentUserId.Value);

                await CheckGuestIdAsync(guest, input.GuestId);
            }
            else
            {
                guest = await _guestManager.GetAnonymousGuestAsync(input.GuestId);
            }

            await _locationHistoryManager.CreateAsync(LocationHistory.Create(input.Longitude, input.Latitude, guest, currentUserId));
        }

        private async Task CheckGuestIdAsync(Guest guest, Guid? guestId)
        {
            if (guestId.HasValue)
                if (guest.Id != guestId.Value)
                {
                    var anonymousGuest = await _guestManager.GetAnonymousGuestAsync(guestId.Value);
                    _guestManager.MergeGuests(guest, anonymousGuest);
                }
        }
    }
}