using System;
using System.Linq;
using MGSUBackend.Authentification;
using MGSUBackend.Models.Mappers;
using MGSUCore.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostManagment;
using UserManagment.Application;

namespace MGSUCore.Controllers
{
    [CustomExceptionFilterAttribute]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly IAttendanceManager _attendanceManager;
        private const string eventsCategoryName = "events";

        public EventsController(IPostManager postManager, IAttendanceManager attendanceManager)
        {
            _postManager = postManager;
            _attendanceManager = attendanceManager;
        }
        
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return
                Ok(_postManager.GetPostsByCategory(eventsCategoryName)
                                .Select(PostMapper.PostToPostModel));
        }

        [HttpGet("{year}/{month}")]
        [Authorize("User")]
        public IActionResult GetEventsByYearAndMonth(int year, int month)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var beginOfMonth = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeZoneInfo.Local.BaseUtcOffset);
            var beginOfNextMonth = beginOfMonth.AddMonths(1);

            var allAttendancesOfUser = _attendanceManager.GetAllIdsOfUserEvents(User.GetId());

            var eventsForUser = allAttendancesOfUser.Select(attendance => _postManager.GetPostById(attendance.EventId));

            var postsToReturn = eventsForUser.Where(
                    post => post.Category == eventsCategoryName && 
                            post.Date >= beginOfMonth &&
                            post.Date < beginOfNextMonth);

            return Ok(postsToReturn.Select(PostMapper.PostToPostModel));
        }

        [HttpPost("attend")]
        [Authorize("User")]
        public IActionResult AttendUserToEvend()
        {
            throw new NotImplementedException();
        }
    }
}