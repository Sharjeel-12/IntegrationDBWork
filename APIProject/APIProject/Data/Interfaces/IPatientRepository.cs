using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IPatientRepository
    {
        public Task AddPatientAsync(CreatePatientDto patient);
        public Task UpdatePatientAsync(UpdatePatientDto patient);
        public Task DeletePatientAsync(int id);
        public Task<List<Patient>> GetAllPatientsAsync();
        public Task<Patient> GetPatientAsync(int id);

    }
}
