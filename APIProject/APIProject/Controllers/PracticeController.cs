using Microsoft.AspNetCore.Mvc;
using APIProject.Dtos;
using APIProject.Models;
using APIProject.Services.Interfaces; 

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        private readonly IPracticeService _service;

        public PracticeController(IPracticeService service)
        {
            _service = service;
        }

        [HttpGet("AllPractices")]
        public async Task<ActionResult<List<Practice>>> GetAllPractices()
        {
            var practices = await _service.GetAllPracticeRecordsAsync();

            if (practices == null || practices.Count == 0)
                return NotFound("No practice records found.");

            return Ok(practices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Practice>> GetPracticeById([FromRoute] int id)
        {
            var practice = await _service.GetPracticeRecordAsync(id);

            if (practice == null)
                return NotFound($"Practice with ID {id} not found.");

            return Ok(practice);
        }

        [HttpPost]
        public async Task<IActionResult> AddPractice([FromBody] CreatePracticeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddPracticeRecordAsync(dto);
            return Ok("Practice record created successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePractice([FromRoute] int id)
        {
            await _service.DeletePracticeRecordAsync(id);
            return Ok($"Practice with ID {id} deleted successfully.");
        }
    }
}
