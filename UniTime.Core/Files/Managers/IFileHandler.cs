using System.IO;

namespace UniTime.Files.Managers
{
    public interface IFileHandler
    {
        FileStream Get(string remoteFileName);
        void Create(string remoteFileName, Stream stream);
        void Delete(string remoteFileName);
    }
}