using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Application;
using MGSUBackend.Models;
using MGSUBackend.Models.Mappers;
using MongoDB.Bson;
using UserManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class UserController : ApiController
    {
        // GET: User
        public IEnumerable<UserModel> Get()
        {
            return _userRepository.GetByPredicate()
                .Select(UserMapper.UserToUserModel);
        }

        // GET: User/5
        public UserModel Get(string id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = ModelState.ToString()
                });
            }

            var user = _userRepository.GetById(new ObjectId(id));
            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });
            }

            return UserMapper.UserToUserModel(user);
        }

        // POST: User
        public IHttpActionResult Post([FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = UserMapper.UserModelToUser(userModel);
            var id = _userRepository.Create(userToCreate);

            return Ok(id);
        }

        // PUT: User/5
        public IHttpActionResult Put(string id, [FromBody]UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.GetById(new ObjectId(id)) == null)
            {
                return NotFound();
            }

            var userToUpdateNew = UserMapper.UserModelToUser(userModel);
            _userRepository.Update(userToUpdateNew);

            return Ok();
        }

        // DELETE: User/5
        public IHttpActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.GetById(new ObjectId(id)) == null)
            {
                return NotFound();
            }

            _userRepository.Delete(new ObjectId(id));

            return Ok();
        }

        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
