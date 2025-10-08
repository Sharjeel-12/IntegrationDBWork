using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Services.Interfaces
{
    public interface IResourcesService
    {
        public Task AddResourceRecordAsync(CreateResourceDto resource);
        public Task DeleteResourceRecordAsync(int ResourceId);
        public Task<List<Resource>> GetAllResources();
    }
}
