﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using UniTime.Activities.Dtos;
using UniTime.Activities.Managers;
using UniTime.Locations.Managers;
using UniTime.Tags;

namespace UniTime.Activities
{
    public class ActivityTemplateAppService : UniTimeAppServiceBase, IActivityTemplateAppService
    {
        private readonly IRepository<AbstractActivity, Guid> _abstractActivityRepository;
        private readonly IActivityTemplateManager _activityTemplateManager;
        private readonly ILocationManager _locationManager;
        private readonly IRepository<Tag, long> _tagRepository;

        public ActivityTemplateAppService(
            IRepository<AbstractActivity, Guid> abstractActivityRepository,
            IRepository<Tag, long> tagRepository,
            IActivityTemplateManager activityTemplateManager,
            ILocationManager locationManager)
        {
            _abstractActivityRepository = abstractActivityRepository;
            _tagRepository = tagRepository;
            _activityTemplateManager = activityTemplateManager;
            _locationManager = locationManager;
        }

        public async Task<GetActivityTemplateOutput> GetActivityTemplate(EntityDto<Guid> input)
        {
            var activityTemplate = await _activityTemplateManager.GetAsync(input.Id);

            return new GetActivityTemplateOutput
            {
                ActivityTemplate = activityTemplate.MapTo<ActivityTemplateDto>()
            };
        }

        public async Task<GetActivityTemplatesOutput> GetActivityTemplates(GetActivityTemplatesInput input)
        {
            var selectedGeographyPoint = $"POINT({input.Longitude} {input.Latitude})";
            var targetRadiusInStandardDistance = ConvertToStandardDistance(100);

            var activityTemplates = await _abstractActivityRepository.GetAll()
                .OfType<ActivityTemplate>()
                .WhereIf(input.TagTexts != null && input.TagTexts.Length > 0,
                    activityTemplate => input.TagTexts.Any(tagText => activityTemplate.Tags.Select(tag => tag.Text).Contains(tagText)))
                .WhereIf(input.Longitude.HasValue && input.Latitude.HasValue,
                    activityTemplate => activityTemplate.Location.Coordinate.Distance(DbGeography.FromText(selectedGeographyPoint)) < targetRadiusInStandardDistance)
                .WhereIf(input.StartTime.HasValue, activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.StartTime > input.StartTime))
                .WhereIf(input.EndTime.HasValue, activityTemplate => activityTemplate.ReferenceTimeSlots.Any(timeSlot => timeSlot.EndTime < input.EndTime))
                .ToListAsync();

            return new GetActivityTemplatesOutput
            {
                ActivityTemplates = activityTemplates.MapTo<List<ActivityTemplateDto>>()
            };
        }

        [AbpAuthorize]
        public async Task<EntityDto<Guid>> CreateActivityTemplate(CreateActivityTemplateInput input)
        {
            var currentUser = await GetCurrentUserAsync();

            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;

            var activityTemplate = await _activityTemplateManager.CreateAsync(ActivityTemplate.Create(
                input.Name,
                input.Description,
                location,
                input.ReferenceTimeSlots.Select(timeSlot => ActivityTemplateReferenceTimeSlot.Create(timeSlot.StartTime, timeSlot.EndTime)).ToList(),
                currentUser
            ));

            return new EntityDto<Guid>(activityTemplate.Id);
        }

        [AbpAuthorize]
        public async Task UpdateActivityTemplate(UpdateActivityTemplateInput input)
        {
            var currentUserId = GetCurrentUserId();
            var activityTemplate = await _activityTemplateManager.GetAsync(input.Id);
            var location = input.LocationId.HasValue ? await _locationManager.GetLocationAsync(input.LocationId.Value) : null;
            var tags = await _tagRepository.GetAllListAsync(tag => input.TagIds.Contains(tag.Id));

            _activityTemplateManager.EditActivityTemplate(
                activityTemplate,
                input.Name,
                input.Description,
                input.ReferenceTimeSlots.Select(timeSlot => ActivityTemplateReferenceTimeSlot.Create(timeSlot.StartTime, timeSlot.EndTime)).ToList(),
                location,
                tags,
                currentUserId
            );
        }

        private static double ConvertToStandardDistance(double distanceInMile)
        {
            return distanceInMile / 1609.344;
        }
    }
}