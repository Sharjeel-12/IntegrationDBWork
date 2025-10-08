using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IProviderRepository
    {
        public Task AddProviderAsync(CreateProviderDto patient);
        public Task UpdateProviderAsync(UpdateProviderDto patient);
        public Task DeleteProviderAsync(int id);
        public Task<List<Provider>> GetAllProvidersAsync();
        public Task<Provider> GetProviderAsync(int id);
    }
}
