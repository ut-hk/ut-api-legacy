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
    }
}