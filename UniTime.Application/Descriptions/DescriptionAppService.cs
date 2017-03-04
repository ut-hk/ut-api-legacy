using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Activities.Managers;
using UniTime.Descriptions.Managers;

namespace UniTime.Descriptions
{
    [AbpAuthorize]
    public class DescriptionAppService : UniTimeAppServiceBase, IDescriptionAppService
    {
        private readonly IActivityPlanManager _activityPlanManager;
        private readonly IDescriptionManager _descriptionManager;
        private readonly IRepository<Description, long> _descriptionRepository;

        public DescriptionAppService(
            IRepository<Description, long> descriptionRepository,
            IDescriptionManager descriptionManager,
            IActivityPlanManager activityPlanManager)
        {
            _descriptionRepository = descriptionRepository;
            _descriptionManager = descriptionManager;
            _activityPlanManager = activityPlanManager;
        }

        public async Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityPlan = await _activityPlanManager.GetAsync(input.ActivityPlanId);

            var textDescription = await _descriptionManager.CreateAsync(new TextActivityPlanDescription
            {
                ActivityPlan = activityPlan,
                ActivityPlanId = activityPlan.Id
            });

            return new EntityDto<long>(textDescription.Id);
        }

        public async Task UpdateTextDescription(UpdateTextDescriptionInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var textDescription = await _descriptionManager.GetAsync(input.Id) as TextActivityPlanDescription;

            if (textDescription == null) throw new UserFriendlyException("The text description with id = " + input.Id + " does not exist.");

            textDescription.Text = input.Text;
        }
    }

    public interface IDescriptionAppService : IApplicationService
    {
        Task<EntityDto<long>> CreateTextDescription(CreateTextDescriptionInput input);

        Task UpdateTextDescription(UpdateTextDescriptionInput input);
    }

    public class UpdateTextDescriptionInput : EntityDto<long>
    {
        public string Text { get; set; }
    }

    public class CreateTextDescriptionInput
    {
        public Guid ActivityPlanId { get; set; }
    }
}