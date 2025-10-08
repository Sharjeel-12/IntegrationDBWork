using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IPracticeLocationRepository
    {
        Task<PracticeLocation> GetPracticeLocationAsync(int LocationId);
        Task<List<PracticeLocation>> GetAllPracticeLocationsAsync();
        Task DeletePracticeLocationAsync(int LocationId);
        Task AddPracticeLocationAsync(CreatePracticeLocationDto practiceLocationDto);
        Task UpdatePracticeLocationAsync(UpdatePracticeLocationDto updatePracticeLocationDto);
    }
}
