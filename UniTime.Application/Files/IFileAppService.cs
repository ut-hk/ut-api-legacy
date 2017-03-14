using System.Threading.Tasks;
using Abp.Application.Services;
using UniTime.Files.Dtos;

namespace UniTime.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<GetMyImagesOutput> GetMyImages();
    }
}