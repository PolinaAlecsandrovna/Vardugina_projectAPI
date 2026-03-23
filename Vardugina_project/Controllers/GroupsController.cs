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
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IScheduleService service, ILogger<GroupsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GroupDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<GroupDto>>> GetAllGroups([FromQuery] string? search = null)
        {
            try
            {
                _logger.LogInformation("Запрос списка групп с поиском: {Search}", search);

                var groups = await _service.GetAllGroupsAsync();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    groups = groups
                        .Where(g => g.GroupName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    _logger.LogInformation("Найдено {Count} групп по запросу '{Search}'",
                        groups.Count, search);
                }
                else
                {
                    _logger.LogInformation("Получено {Count} групп", groups.Count);
                }

                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка групп");
                return StatusCode(500, new
                {
                    error = "Не удалось загрузить список групп.",
                    details = ex.Message
                });
            }
        }
    }
}

