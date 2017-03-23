using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
using UniTime.Files.Dtos;

namespace UniTime.Files
{
    [AbpAuthorize]
    public class FileAppService : UniTimeAppServiceBase, IFileAppService
    {
        private readonly IRepository<File, Guid> _fileRepository;

        public FileAppService(
            IRepository<File, Guid> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<GetMyImagesOutput> GetMyImages()
        {
            var currentUserId = GetCurrentUserId();

            var images = await _fileRepository.GetAll()
                .OfType<Image>()
                .Where(image => image.OwnerId == currentUserId)
                .ProjectTo<FileDto>()
                .ToListAsync();

            return new GetMyImagesOutput
            {
                Images = images
            };
        }
    }
}