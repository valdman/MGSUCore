using System;
using System.IO;
using FileManagment.Entities;
using MongoDB.Bson;
using PostManagment.Entities;

namespace MGSUBackend.Models.Mappers
{
    public static class PostMapper
    {
        public static PostModel PostToPostModel(Post post)
        {
            if (post == null) return null;

            return new PostModel
            {
                Id = post.Id.ToString(),
                Title = post.Title,
                Category = post.Category,
                Content = post.Content,
                CreatingTime = post.CreatingTime.ToString(),
                Date = post.Date?.AsString,
                Description = post.Description,
                Img = post.Img == null
                    ? null
                    : new ImageModel
                    {
                        Original = post.Img.Original.ToString(),
                        Small = post.Img.Small.ToString(),
                        Role = post.Img.Role
                    }
            };
        }

        public static Post PostModelToPost(PostModel postModel)
        {
            if (postModel == null) return null;

            return new Post
            {
                Title = postModel.Title,
                Category = postModel.Category,
                Content = postModel.Content,
                CreatingTime = BsonDateTime.Create(DateTime.UtcNow),
                Date = postModel.Date == null ? null : BsonDateTime.Create(postModel.Date),
                Description = postModel.Description,
                Img = postModel.Img == null
                    ? null
                    : new Image
                    {
                        Original = new FileInfo(postModel.Img.Original),
                        Small = new FileInfo(postModel.Img.Small),
                        Role = postModel.Img.Role
                    }
            };
        }
    }
}