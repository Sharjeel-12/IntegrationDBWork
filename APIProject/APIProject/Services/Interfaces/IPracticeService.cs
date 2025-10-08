using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IPracticeService
    {
        Task<List<Practice>> GetAllPracticeRecordsAsync();
        Task<Practice> GetPracticeRecordAsync(int PracticeId);
        Task AddPracticeRecordAsync(CreatePracticeDto practice);
        Task DeletePracticeRecordAsync(int PracticeId);
    }
}
