using APIProject.Dtos;
using APIProject.Models;
using System.ComponentModel.Design;
using APIProject.Data.Interfaces;
namespace APIProject.Services.Interfaces;

public class ResourceService:IResourcesService
{
    private readonly IResourceRepository _repo;
    public ResourceService(IResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task AddResourceRecordAsync(CreateResourceDto resource) { await _repo.AddResourceAsync(resource); }
    public async Task DeleteResourceRecordAsync(int ResourceId){ await _repo.DeleteResourceAsync(ResourceId); }
    public async Task<List<Resource>> GetAllResources(){ return await _repo.GetAllResources(); }
}
