using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ProviderSchedule>> GetAllScheduleRecordAsync();
        Task<ProviderSchedule> GetScheduleRecordByIdAsync(int id);
        Task AddScheduleRecordAsync(CreateProviderScheduleDto schedule);
        Task UpdateScheduleRecordAsync(UpdateProviderScheduleDto schedule);
        Task DeleteScheduleRecordAsync(int id);
    }
}
