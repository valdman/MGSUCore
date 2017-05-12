using System;
using System.IO;
using System.Threading.Tasks;
using FileManagment.Entities;

namespace FileManagment
{
    public interface IFileManager
    {
		Stream GetFile(string fileName);

		Stream GetImage(string imageName);

        Task<string> UploadFileAsync(Stream content);

        Task<Image> UploadImageAsync(Stream content);
    }
}
