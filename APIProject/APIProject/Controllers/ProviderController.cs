using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;

        public ProviderController(IProviderService service)
        {
            _service = service;
        }
        [HttpGet("AllProviderRecords")]
        public async Task<ActionResult<List<Provider>>> GetAllProviderRecords()
        {
            List<Provider> providers = await _service.GetAllProviderRecordsAsync();

            if (providers == null || providers.Count == 0)
                return NotFound("No provider records found.");

            return Ok(providers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProviderById([FromRoute] int id)
        {
            Provider provider = await _service.GetProviderRecordAsync(id);

            if (provider == null)
                return NotFound($"Provider with ID {id} not found.");

            return Ok(provider);
        }


        [HttpPost]
        public async Task<IActionResult> AddProvider([FromBody] CreateProviderDto providerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddProviderRecordAsync(providerDto);
            return Ok( "Provider record created successfully.");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProvider([FromBody] UpdateProviderDto providerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateProviderRecordAsync(providerDto);
            return Ok("Provider record updated successfully.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider([FromRoute] int id)
        {
            await _service.DeleteProviderRecordAsync(id);
            return Ok($"Provider record with ID {id} deleted successfully.");
        }

    }
}
