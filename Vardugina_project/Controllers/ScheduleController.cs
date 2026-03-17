using Microsoft.AspNetCore.Mvc;
using Vardugina_project.Data;
using Vardugina_project.Services;
using Vardugina_project.Models;
using Microsoft.EntityFrameworkCore;


namespace Vardugina_project.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service, AppDbContext db)
        {
            _service = service;
        }
        [HttpGet("group/{groupName}")]
        public async Task<IActionResult> GetSchedule(string groupName, DateTime start, DateTime end)
        {
            var result = await _service.GetScheduleForGroup(groupName, start, end);
            return Ok(result);
        }

    }
}
