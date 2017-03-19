using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;

namespace UniTime.Descriptions.Managers
{
    public class DescriptionManager : IDescriptionManager
    {
        private readonly IRepository<Description, long> _descriptionRepository;

        public DescriptionManager(
            IRepository<Description, long> descriptionRepository)
        {
            _descriptionRepository = descriptionRepository;
        }

        public async Task<Description> GetAsync(long id)
        {
            var description = await _descriptionRepository.FirstOrDefaultAsync(id);

            if (description == null)
                throw new UserFriendlyException("The description with id = " + id + " does not exist.");

            return description;
        }

        public async Task<Description> CreateAsync(Description description)
        {
            description.Id = await _descriptionRepository.InsertAndGetIdAsync(description);

            return description;
        }

        public void EditDescription(Description description, string[] htmlClasses, long editUserId)
        {
            description.EditHTMLClasses(htmlClasses, editUserId);
        }

        public void EditTextDescription(TextDescription textDescription, string text, long editUserId)
        {
            textDescription.EditText(text, editUserId);
        }

        public void EditInternalImageDescription(InternalImageDescription internalImageDescription, string text, long editUserId)
        {
            var image = internalImageDescription.Image;

            image.EditDescription(text);
        }

        public async Task RemoveAsync(Description description)
        {
            await _descriptionRepository.DeleteAsync(description);
        }
    }
}