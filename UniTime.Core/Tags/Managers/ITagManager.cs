using System.Threading.Tasks;
using Abp.Domain.Services;

namespace UniTime.Tags.Managers
{
    public interface ITagManager : IDomainService
    {
        Task<Tag> GetAsync(string text);
    }
}