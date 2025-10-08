using Microsoft.AspNetCore.Mvc;
using APIProject.Models;
using APIProject.Dtos;
using APIProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace APIProject.Controllers
{
    [Authorize(Roles ="1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }


        [HttpGet("all")]
        public async Task<ActionResult<List<ProviderSchedule>>> GetAllSchedules()
        {
            var schedules = await _service.GetAllScheduleRecordAsync();

            if (schedules == null || schedules.Count == 0)
                return NotFound("No schedules found.");

            return Ok(schedules);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderSchedule>> GetScheduleById([FromRoute] int id)
        {
            var schedule = await _service.GetScheduleRecordByIdAsync(id);

            if (schedule == null)
                return NotFound($"Schedule with ID {id} not found.");

            return Ok(schedule);
        }


        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] CreateProviderScheduleDto scheduleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddScheduleRecordAsync(scheduleDto);
            return Ok("Schedule record created successfully.");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateSchedule([FromBody] UpdateProviderScheduleDto scheduleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateScheduleRecordAsync(scheduleDto);
            return Ok("Schedule record updated successfully.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule([FromRoute] int id)
        {
            await _service.DeleteScheduleRecordAsync(id);
            return Ok($"Schedule record with ID {id} deleted successfully.");
        }
    }
}
