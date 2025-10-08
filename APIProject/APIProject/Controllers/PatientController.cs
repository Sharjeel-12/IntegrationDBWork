using Microsoft.AspNetCore.Mvc;
using APIProject.Services.Interfaces;
using APIProject.Models;
using APIProject.Dtos;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service=service;
        }
        [HttpGet("/AllPatientRecord")]
        public async Task<ActionResult<List<Patient>>> getAllPatientData()
        {
           List<Patient> PatientRecord=await _service.GetAllPatientRecordAsync();
           return Ok(PatientRecord);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> getPatientById([FromRoute] int id)
        {
            Patient patient=await _service.GetPatientRecordAsync(id);
            if (patient == null) { return  BadRequest(); }
            return Ok(patient);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] CreatePatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddPatientRecordAsync(patientDto);
            return Ok( "Patient record created successfully.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePatientRecord([FromBody] UpdatePatientDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.UpdatePatientRecordAsync(dto);
            return Ok("Record updated successfully");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            await _service.DeletePatientRecordAsync(id);
            return Ok($"Patient record with ID {id} deleted successfully.");
        }

    }
}
