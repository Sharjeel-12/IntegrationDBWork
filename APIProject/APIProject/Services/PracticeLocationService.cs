using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;

namespace APIProject.Services
{
    public class PracticeLocationService: IPracticeLocationService
    {
        private readonly IPracticeLocationRepository _repo;
        public PracticeLocationService(IPracticeLocationRepository repo) { _repo = repo; }
        public async Task<PracticeLocation> GetPracticeLocationRecordAsync(int LocationId){ return await _repo.GetPracticeLocationAsync(LocationId); }
        public async Task<List<PracticeLocation>> GetAllPracticeLocationRecordAsync(){ return await _repo.GetAllPracticeLocationsAsync(); }
        public async Task DeletePracticeLocationRecordAsync(int LocationId){await _repo.DeletePracticeLocationAsync(LocationId); }
        public async Task AddPracticeLocationRecordAsync(CreatePracticeLocationDto practiceLocationDto){ await _repo.AddPracticeLocationAsync(practiceLocationDto); }
        public async Task UpdatePracticeLocationRecordAsync(UpdatePracticeLocationDto updatePracticeLocationDto){ await _repo.UpdatePracticeLocationAsync(updatePracticeLocationDto); }
    }
}
