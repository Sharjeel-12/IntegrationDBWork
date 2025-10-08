using Microsoft.AspNetCore.Mvc;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _service;

        public ResourcesController(IResourcesService service)
        {
            _service = service;
        }

        [HttpGet("AllResources")]
        public async Task<ActionResult<List<Resource>>> GetAllResources()
        {
            var resources = await _service.GetAllResources();

            if (resources == null || resources.Count == 0)
                return NotFound("No resources found.");

            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> AddResource([FromBody] CreateResourceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddResourceRecordAsync(dto);
            return Ok("Resource added successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource([FromRoute] int id)
        {
            await _service.DeleteResourceRecordAsync(id);
            return Ok($"Resource with ID {id} deleted successfully.");
        }
    }
}
