using Vardugina_project.DTO;

namespace Vardugina_project.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleByDateDto>> GetScheduleForGroup(string groupName, DateTime startDate, DateTime endDate);
        Task<List<GroupDto>> GetAllGroupsAsync();
        
    }
}
