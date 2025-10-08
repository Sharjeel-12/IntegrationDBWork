using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IScheduleRepository
    {
        Task<List<ProviderSchedule>> GetAllSchedulesAsync();
        Task<ProviderSchedule> GetScheduleByIdAsync(int id);
        Task AddScheduleAsync(CreateProviderScheduleDto schedule);
        Task UpdateScheduleAsync(UpdateProviderScheduleDto schedule);
        Task DeleteScheduleAsync(int id);
    }
}
