using APIProject.Dtos;
using APIProject.Models;

namespace APIProject.Data.Interfaces
{
    public interface IResourceRepository
    {
        public Task AddResourceAsync(CreateResourceDto resource);
        public Task DeleteResourceAsync(int ResourceId);
        public Task<List<Resource>> GetAllResources();

    }
}
