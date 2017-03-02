using System;
using System.IO;

namespace UniTime.Files.Managers
{
    public class LocalFileHandler : IFileHandler
    {
        private readonly string _localStoragePath;

        public LocalFileHandler()
        {
            _localStoragePath = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\Files\";
        }

        public FileStream Get(string remoteFileName)
        {
            return System.IO.File.OpenRead(_localStoragePath + remoteFileName);
        }

        public void Create(string remoteFileName, Stream stream)
        {
            using (Stream destination = System.IO.File.Create(_localStoragePath + remoteFileName))
            {
                stream.CopyTo(destination);
            }
        }

        public void Delete(string remoteFileName)
        {
            System.IO.File.Delete(_localStoragePath + remoteFileName);
        }
    }
}