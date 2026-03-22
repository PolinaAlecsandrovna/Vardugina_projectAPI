using Vardugina_project.DTO;
using Vardugina_project.Services;
using Microsoft.AspNetCore.Mvc;


namespace Vardugina_project.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupsController : Controller
    {
        private readonly IScheduleService _service;

        public GroupsController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GroupDto>), 200)]
        public async Task<ActionResult<List<GroupDto>>> GetAllGroups([FromQuery] string? search = null)
        {
            try
            {
                var groups = await _service.GetAllGroupsAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    groups = groups
                        .Where(g => g.GroupName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Не удалось загрузить список групп.", details = ex.Message });
            }
        }

    }
}
