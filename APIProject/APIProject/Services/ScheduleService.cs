using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;

namespace APIProject.Services
{
    public class ScheduleService:IScheduleService
    {
        private readonly IScheduleRepository _repo;
        public ScheduleService(IScheduleRepository repo ) { _repo = repo; }
        public async Task<List<ProviderSchedule>> GetAllScheduleRecordAsync() { return await _repo.GetAllSchedulesAsync(); }
        public async Task<ProviderSchedule> GetScheduleRecordByIdAsync(int id){ return await _repo.GetScheduleByIdAsync(id); }
        public async Task AddScheduleRecordAsync(CreateProviderScheduleDto schedule){ await _repo.AddScheduleAsync(schedule); }
        public async Task UpdateScheduleRecordAsync(UpdateProviderScheduleDto schedule){ await _repo.UpdateScheduleAsync(schedule); }
        public async Task DeleteScheduleRecordAsync(int id){ await _repo.DeleteScheduleAsync(id); }
    }
}
