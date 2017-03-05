using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using UniTime.Tags.Dtos;
using UniTime.Tags.Managers;

namespace UniTime.Tags
{
    public class TagAppService : UniTimeAppServiceBase, ITagAppService
    {
        private readonly ITagManager _tagManager;
        private readonly IRepository<Tag, long> _tagRepository;

        public TagAppService(
            ITagManager tagManager,
            IRepository<Tag, long> tagRepository)
        {
            _tagManager = tagManager;
            _tagRepository = tagRepository;
        }

        [AbpAuthorize]
        public async Task<EntityDto<long>> GetTag(GetTagInput input)
        {
            var tag = await _tagManager.GetAsync(input.Text.Replace(" ", string.Empty));

            return new EntityDto<long>(tag.Id);
        }

        public async Task<GetTagsOutput> GetTags(GetTagsInput input)
        {
            var tags = await _tagRepository.GetAllListAsync(tag => tag.Text.Contains(input.QueryText));

            return new GetTagsOutput
            {
                Tags = tags.MapTo<List<TagDto>>()
            };
        }
    }
}