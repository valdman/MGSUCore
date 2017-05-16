using System;
using System.IO;

namespace Common.Entities
{
    public class Image
    {
        public Image(){}

        public Image(FileInfo bigPhotoUri, FileInfo smallPhotoUri)
        {
            Original = bigPhotoUri;
            Small = smallPhotoUri;
        }

        public FileInfo Original { get; set; }
        public FileInfo Small { get; set; }

        public string Role { get; set; }
    }
}