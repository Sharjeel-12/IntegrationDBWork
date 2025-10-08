using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IPatientService
    {
        public Task AddPatientRecordAsync(CreatePatientDto patient);
        public Task UpdatePatientRecordAsync(UpdatePatientDto patient);
        public Task DeletePatientRecordAsync(int id);
        public Task<List<Patient>> GetAllPatientRecordAsync();
        public Task<Patient> GetPatientRecordAsync(int id);
    }
}
