using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IPracticeLocationService
    {
        Task<PracticeLocation> GetPracticeLocationRecordAsync(int LocationId);
        Task<List<PracticeLocation>> GetAllPracticeLocationRecordAsync();
        Task DeletePracticeLocationRecordAsync(int LocationId);
        Task AddPracticeLocationRecordAsync(CreatePracticeLocationDto practiceLocationDto);
        Task UpdatePracticeLocationRecordAsync(UpdatePracticeLocationDto updatePracticeLocationDto);
    }
}
