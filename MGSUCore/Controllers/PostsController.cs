using System;
using System.Collections.Generic;
using System.Linq;
using MGSUBackend.Authentification;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PostManagment;
using UserManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostManager _postManager;

        public PostsController(IPostManager postManager)
        {
            _postManager = postManager;
        }

        // GET: api/Posts?one=value&two=secondValue
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

        // GET: api/Posts/5
        public IHttpActionResult Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postToReturn = _postManager.GetPostById(new ObjectId(id));

            if (postToReturn == null)
                return NotFound();

            return Ok(PostMapper.PostToPostModel(postToReturn));
        }

        // POST: api/Posts
        [Authorization(UserRole.Admin)]
        public IHttpActionResult Post([FromBody] PostModel postModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postToCreate = PostMapper.PostModelToPost(postModel);

            return Ok(_postManager.CreatePost(postToCreate).ToString());
        }

        // PUT: api/Posts/5
        [Authorization(UserRole.Admin)]
        public IHttpActionResult Put(string id, [FromBody] PostModel postModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldPost = _postManager.GetPostById(new ObjectId(id));

            if (oldPost == null)
                return NotFound();

            oldPost.Title = postModel.Title ?? oldPost.Title;
            oldPost.Category = postModel.Category ?? oldPost.Category;
            oldPost.Content = postModel.Content ?? oldPost.Content;
            oldPost.Date = postModel.Date.IsEmpty() ? oldPost.Date : BsonDateTime.Create(postModel.Date);
            oldPost.Description = postModel.Description ?? oldPost.Description;

            _postManager.UpdatePost(oldPost);
            return Ok();
        }

        // DELETE: api/Posts/5
        [Authorization(UserRole.Admin)]
        public IHttpActionResult Delete(string id)
        {
            var oldPost = _postManager.GetPostById(new ObjectId(id));

            if (oldPost == null)
                return NotFound();

            _postManager.DeletePost(new ObjectId(id));
            return Ok();
        }
    }
}