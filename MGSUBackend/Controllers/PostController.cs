using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using FileManagment.Entities;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using MongoDB.Bson;
using PostManagment;
using PostManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class PostController : ApiController
    {
        // GET: api/Post?one=value&two=secondValue
        public IEnumerable<PostModel> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            var urlKeyValues = allUrlKeyValues as KeyValuePair<string, string>[] ?? allUrlKeyValues.ToArray();

            if (urlKeyValues.Any(pair => pair.Key == "category"))
            {
                var categoryName = urlKeyValues.Single(pair => pair.Key == "category").Value;

                return
                    _postManager.GetPostsByCategory(categoryName).Select(PostMapper.PostToPostModel);
            }

            return _postManager.GetPostsByPredicate().Select(PostMapper.PostToPostModel);
        }

        // GET: api/Post/5
        public PostModel Get(string id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = ModelState.ToString()
                });
            }

            var postToReturn = _postManager.GetPostById(new ObjectId(id));

            if (postToReturn == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = "Post not found"
                });
            }

            return
                PostMapper.PostToPostModel(postToReturn);
        }
     
        // POST: api/Post
        public string Post([FromBody]PostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = ModelState.ToString()
                });
            }

            var postToCreate = PostMapper.PostModelToPost(postModel);

            return _postManager.CreatePost(postToCreate).ToString();
        }

        // PUT: api/Post/5
        public void Put(string id, [FromBody]PostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = ModelState.ToString()
                });
            }

            var oldPost = _postManager.GetPostById(new ObjectId(id));

            if (oldPost == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ReasonPhrase = "Post with this id not found"
                });
            }

            var postToUpdate = new Post
            {
                Title = postModel.Title,
                Category = postModel.Category,
                Content = postModel.Content,
                CreatingTime = BsonDateTime.Create(DateTime.UtcNow),
                Date = BsonDateTime.Create(postModel.Date),
                Description = postModel.Description
            };

            _postManager.UpdatePost(postToUpdate);
        }

        // DELETE: api/Post/5
        public void Delete(string id)
        {
            var oldPost = _postManager.GetPostById(new ObjectId(id));

            if (oldPost == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ReasonPhrase = "Post with this id not found"
                });
            }

            _postManager.DeletePost(new ObjectId(id));
        }

        private readonly IPostManager _postManager;

        public PostController(IPostManager postManager)
        {
            _postManager = postManager;
        }
    }
}
