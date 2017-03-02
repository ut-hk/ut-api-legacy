using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;

namespace UniTime.Activities
{
    public class ActivityTemplateAppService : UniTimeAppServiceBase, IActivityTemplateAppService
    {
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;

        public ActivityTemplateAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IActivityTemplateManager activityTemplateManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _activityTemplateManager = activityTemplateManager;
        }

        public async Task<GetActivityTemplatesOutput> GetActivityTemplates()
        {
            var activityTemplates = await _abstractActivityRepository.GetAll().OfType<ActivityTemplate>().ToListAsync();

            return new GetActivityTemplatesOutput
            {
                ActivityTemplates = activityTemplates.MapTo<List<ActivityTemplateDto>>()
            };
        }

        public async Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var activityTemplate = await _activityTemplateManager.CreateActivityTemplateAsync(new ActivityTemplate
            {
                Name = input.Name,
                Description = input.Description,
                ReferenceStarTime = input.ReferenceStarTime,
                ReferenceEndTime = input.ReferenceEndTime,
                Owner = currentUser,
                OwnerId = currentUser.Id
            });

            return new EntityDto<Guid>(activityTemplate.Id);
        }
    }
}