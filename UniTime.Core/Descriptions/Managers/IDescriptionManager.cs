using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Descriptions.Managers
{
    public interface IDescriptionManager : IDomainService
    {
        Task<Description> GetAsync(long id);

        Task<Description> CreateAsync(Description description);

        void EditDescription(Description description, string[] htmlClasses, long editUserId);
        void EditTextDescription(TextDescription textDescription, string text, long editUserId);
        void EditInternalImageDescription(InternalImageDescription internalImageDescription, string text, long editUserId);


        Task RemoveAsync(Description description);
    }
}