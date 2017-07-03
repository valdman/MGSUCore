using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGSUCore.Filters;
using MGSUCore.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MGSUCore.Controllers
{
	[CustomExceptionFilterAttribute]
	[Route("[controller]")]
    public class ProjectsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

            var projectToReturn = _projectManager.GetProjectById(new ObjectId(id));

			if (projectToReturn  == null)
				return NotFound();

			return Ok(ProjectMapper.PostToPostModel(projectToReturn));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ProjectModel projectModel)
        {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var postToCreate = ProjectMapper.PostModelToPost(projectModel);

			return Ok(_projectManager.CreateProject(postToCreate).ToString());
        }

        // PUT values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
