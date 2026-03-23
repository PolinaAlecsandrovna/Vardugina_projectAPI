using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vardugina_project.Data;
using Vardugina_project.Services;



namespace Vardugina_project.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        private readonly AppDbContext _db;
        public ScheduleController(IScheduleService service, AppDbContext db)
        {
            _service = service;
            _db = db;
        }
        [HttpGet("group/{groupName}")]
        public async Task<IActionResult> GetSchedule(string groupName, DateTime start, DateTime end)
        {
            var result = await _service.GetScheduleForGroup(groupName, start, end);
            return Ok(result);
        }

        [HttpGet("groups")]
        public async Task<ActionResult<List<string>>> GetAllGroups()
        {
            var groups = await _db.StudentGroups
                .OrderBy(g => g.GroupName)
                .Select(g => g.GroupName)
                .ToListAsync();

            return Ok(groups);
        }

        [HttpGet("groups/search")]
        public async Task<ActionResult<List<string>>> SearchGroups(
            [FromQuery] string query)
        {
            var groups = await _db.StudentGroups
                .Where(g => g.GroupName.Contains(query))
                .OrderBy(g => g.GroupName)
                .Select(g => g.GroupName)
                .ToListAsync();

            return Ok(groups);
        }
    }
}
