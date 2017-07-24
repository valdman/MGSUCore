using System;
using System.Linq;
using MGSUBackend.Models.Mappers;
using MGSUCore.Filters;
using Microsoft.AspNetCore.Mvc;
using PostManagment;

namespace MGSUCore.Controllers
{
    [CustomExceptionFilterAttribute]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly IPostManager _postManager;
        private const string eventsCategoryName = "events";

        public EventsController(IPostManager postManager)
        {
            _postManager = postManager;
        }
        
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return
                Ok(_postManager.GetPostsByCategory(eventsCategoryName)
                                .Select(PostMapper.PostToPostModel));
        }

        [HttpGet("{year}/{month}")]
        public IActionResult GetEventsByYearAndMonth(int year, int month)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var beginOfMonth = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeZoneInfo.Local.BaseUtcOffset);
            var beginOfNextMonth = beginOfMonth.AddMonths(1);

            var postsToReturn = _postManager.GetPostsByPredicate(
                    post => post.Category == eventsCategoryName && 
                            post.Date >= beginOfMonth &&
                            post.Date < beginOfNextMonth);

            return Ok(postsToReturn.Select(PostMapper.PostToPostModel));
        }

        //[HttpPost("attend")]
    }
}