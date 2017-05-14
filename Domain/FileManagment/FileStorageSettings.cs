using System;
namespace FileManagment
{
    public class FileStorageSettings
    {
        public FileStorageSettings() 
        {
        }

        public FileStorageSettings(string imageStorageFolder,string fileStorageFolder,string[] allowedImageExtensions, int maxImageSize)
        {
            ImageStorageFolder = imageStorageFolder;
            FileStorageFolder = fileStorageFolder;
            AllowedImageExtensions = allowedImageExtensions;
            MaxImageSize = maxImageSize;
        }

        public string ImageStorageFolder { get;  set; }
        public string FileStorageFolder { get;  set; }
        public string[] AllowedImageExtensions { get;  set; }
        public int MaxImageSize { get;  set; }
    }
}
