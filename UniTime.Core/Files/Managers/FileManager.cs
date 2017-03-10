using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.UI;
using UniTime.Users;

namespace UniTime.Files.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IFileHandler _fileHandler = new LocalFileHandler();
        private readonly IRepository<File, Guid> _fileRepository;

        public FileManager(
            IRepository<File, Guid> fileRepository
        )
        {
            _fileRepository = fileRepository;
        }

        public async Task<File> GetAsync(Guid id)
        {
            var file = await _fileRepository.GetAsync(id);

            if (file == null)
                throw new UserFriendlyException($"The file with id = {id} does not exist.");

            return file;
        }

        public async Task<ICollection<File>> GetFilesAsync(ICollection<Guid> ids)
        {
            var files = await _fileRepository.GetAllListAsync(file => ids.Contains(file.Id));

            if (files.Distinct().Count() != ids.Count)
                throw new UserFriendlyException("Some files does not exist.");

            return files;
        }

        public FileStream GetStream(File file)
        {
            return _fileHandler.Get(file.RemoteFileName);
        }

        public async Task<Image> CreateImageAsync(Image image, Stream imageStream, User createUser)
        {
            _fileHandler.Create(image.RemoteFileName, imageStream);

            image.Id = await _fileRepository.InsertAndGetIdAsync(image);

            return image;
        }
    }
}