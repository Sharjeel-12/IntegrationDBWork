using APIProject.Data.Interfaces;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace APIProject.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repo;
        public ProviderService(IProviderRepository repo) { _repo = repo; }
        public async Task AddProviderRecordAsync(CreateProviderDto patient) { await _repo.AddProviderAsync(patient); }
        public async Task UpdateProviderRecordAsync(UpdateProviderDto patient){ await _repo.UpdateProviderAsync(patient); }
        public async Task DeleteProviderRecordAsync(int id){ await _repo.DeleteProviderAsync(id); }
        public async Task<List<Provider>> GetAllProviderRecordsAsync(){ return await _repo.GetAllProvidersAsync(); }
        public async Task<Provider> GetProviderRecordAsync(int id){ return await _repo.GetProviderAsync(id); }
    }
}
