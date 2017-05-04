using System;
using FileManagment.Entities;
using MongoDB.Bson;
using PostManagment.Entities;

namespace MGSUBackend.Models.Mappers
{
    public static class PostMapper
    {
        public static PostModel PostToPostModel(Post post)
        {
            return new PostModel
            {
                Id = post.Id.ToString(),
                Title = post.Title,
                Category = post.Category,
                Content = post.Content,
                CreatingTime = post.CreatingTime.ToString(),
                Date = post.Date?.AsString,
                Description = post.Description,
                Img = new ImageModel
                {
                    Original = post.Img.Original,
                    Small = post.Img.Small,
                    Role = post.Img.Role
                }
            };
        }

        public static Post PostModelToPost(PostModel postModel)
        {
            return new Post
            {
                Title = postModel.Title,
                Category = postModel.Category,
                Content = postModel.Content,
                CreatingTime = BsonDateTime.Create(DateTime.UtcNow),
                Date = postModel.Date == null ? null : BsonDateTime.Create(postModel.Date),
                Description = postModel.Description,
                Img = new Image
                {
                    Original = postModel.Img?.Original,
                    Small = postModel.Img?.Small,
                    Role = postModel.Img?.Role
                }
            };
        }
    }
}