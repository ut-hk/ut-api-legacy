using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.Domain.Services;
using UniTime.Users;

namespace UniTime.Files.Managers
{
    public interface IFileManager : IDomainService
    {
        Task<File> GetAsync(Guid id);
        Task<ICollection<File>> GetFilesAsync(ICollection<Guid> ids);
        FileStream GetStream(File file);

        Task<Image> CreateImageAsync(Image image, Stream imageStream, User createUser);
    }
}