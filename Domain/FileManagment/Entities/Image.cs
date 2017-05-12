using System;

namespace FileManagment.Entities
{
    public class Image
    {
        public Image(Uri bigPhotoUri, Uri smallPhotoUri)
        {
            Original = bigPhotoUri;
            Small = smallPhotoUri;
        }

        public Uri Original { get; set; }
        public Uri Small { get; set; }

        public string Role { get; set; }
    }
}