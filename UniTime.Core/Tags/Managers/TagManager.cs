using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace UniTime.Tags.Managers
{
    public class TagManager : ITagManager
    {
        private readonly IRepository<Tag, long> _tagRepository;

        public TagManager(
            IRepository<Tag, long> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Tag> GetAsync(string text)
        {
            var tag = await _tagRepository.FirstOrDefaultAsync(t => t.Text == text);

            if (tag == null)
            {
                tag = Tag.Create(text);
                tag.Id = await _tagRepository.InsertAndGetIdAsync(tag);
            }

            return tag;
        }

        public async Task<ICollection<Tag>> GetTags(string[] texts)
        {
            var validTexts = texts.Where(text => Regex.Match(text, @"(#[a-z0-9][a-z0-9\-_]*)", RegexOptions.IgnoreCase).Success);

            var existingTags = await _tagRepository.GetAll()
                .Where(tag => validTexts.Any(tagText => tagText == tag.Text))
                .ToArrayAsync();

            var newTags = texts.Except(existingTags.Select(tag => tag.Text)).Select(Tag.Create).ToArray();

            foreach (var newTag in newTags)
                newTag.Id = await _tagRepository.InsertAndGetIdAsync(newTag);

            return existingTags.Union(newTags).ToArray();
        }
    }
}