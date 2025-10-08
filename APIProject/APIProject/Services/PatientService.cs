using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;
namespace APIProject.Services
{
    public class PatientService:IPatientService
    {
        private readonly IPatientRepository _repo;
        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task AddPatientRecordAsync(CreatePatientDto patient) { await _repo.AddPatientAsync(patient); }
        public async Task UpdatePatientRecordAsync(UpdatePatientDto patient) { await _repo.UpdatePatientAsync(patient); }
        public async Task DeletePatientRecordAsync(int id) { await _repo.DeletePatientAsync(id); }
        public async Task<List<Patient>> GetAllPatientRecordAsync() { return await _repo.GetAllPatientsAsync(); }
        public async Task<Patient> GetPatientRecordAsync(int id) { return await _repo.GetPatientAsync(id); }

    }
}
