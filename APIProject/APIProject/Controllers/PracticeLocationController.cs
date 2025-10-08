using Microsoft.AspNetCore.Mvc;
using APIProject.Models;
using APIProject.Dtos;
using APIProject.Services.Interfaces; 

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeLocationController : ControllerBase
    {
        private readonly IPracticeLocationService _service;

        public PracticeLocationController(IPracticeLocationService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<PracticeLocation>>> GetAllPracticeLocations()
        {
            var locations = await _service.GetAllPracticeLocationRecordAsync();

            if (locations == null || locations.Count == 0)
                return NotFound("No practice locations found.");

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PracticeLocation>> GetPracticeLocationById([FromRoute] int id)
        {
            var location = await _service.GetPracticeLocationRecordAsync(id);

            if (location == null)
                return NotFound($"Practice location with ID {id} not found.");

            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> AddPracticeLocation([FromBody] CreatePracticeLocationDto practiceLocationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddPracticeLocationRecordAsync(practiceLocationDto);
            return Ok("Practice location created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePracticeLocation([FromBody] UpdatePracticeLocationDto updatePracticeLocationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdatePracticeLocationRecordAsync(updatePracticeLocationDto);
            return Ok("Practice location updated successfully.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePracticeLocation([FromRoute] int id)
        {
            await _service.DeletePracticeLocationRecordAsync(id);
            return Ok($"Practice location with ID {id} deleted successfully.");
        }
    }
}
