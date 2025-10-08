using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IPracticeRepository
    {
        Task<List<Practice>> GetAllPracticesAsync();
        Task<Practice> GetPracticeAsync(int PracticeId);
        Task AddPracticeAsync(CreatePracticeDto practice);

        //Task UpdatePracticeAsync(UpdatePracticeDto practice);
        Task DeletePracticeAsync(int PracticeId);


    }
}
