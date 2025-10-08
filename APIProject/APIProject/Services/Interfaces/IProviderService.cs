using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IProviderService
    {
        public Task AddProviderRecordAsync(CreateProviderDto patient);
        public Task UpdateProviderRecordAsync(UpdateProviderDto patient);
        public Task DeleteProviderRecordAsync(int id);
        public Task<List<Provider>> GetAllProviderRecordsAsync();
        public Task<Provider> GetProviderRecordAsync(int id);
    }
}
