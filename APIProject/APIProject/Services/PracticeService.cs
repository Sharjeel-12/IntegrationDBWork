using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;

namespace APIProject.Services
{
    public class PracticeService: IPracticeService
    {
        private readonly IPracticeRepository _repo;
        public PracticeService(IPracticeRepository repo) { _repo = repo; }
        public async Task<List<Practice>> GetAllPracticeRecordsAsync() { return await _repo.GetAllPracticesAsync(); }
        public async Task<Practice> GetPracticeRecordAsync(int PracticeId) { return await _repo.GetPracticeAsync(PracticeId); }
        public async Task AddPracticeRecordAsync(CreatePracticeDto practice) { await _repo.AddPracticeAsync(practice); }
        public async Task DeletePracticeRecordAsync(int PracticeId) { await _repo.DeletePracticeAsync(PracticeId); }
    }
}
