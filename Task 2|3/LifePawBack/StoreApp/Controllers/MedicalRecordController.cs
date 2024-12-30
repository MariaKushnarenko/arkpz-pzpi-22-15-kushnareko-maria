using Microsoft.AspNetCore.Mvc;
using StoreApp.Services.Interfaces;
using StoreApp.DatabaseProvider.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsService _medicalRecordsService;

        public MedicalRecordsController(IMedicalRecordsService medicalRecordsService)
        {
            _medicalRecordsService = medicalRecordsService;
        }

        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<List<MedicalRecords>>> GetMedicalRecords()
        {
            var records = await _medicalRecordsService.GetAllMedicalRecordsAsync();
            return Ok(records);
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecords>> GetMedicalRecord(int id)
        {
            var record = await _medicalRecordsService.GetMedicalRecordByIdAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // GET: api/MedicalRecords/animal/5
        [HttpGet("animal/{animalId}")]
        public async Task<ActionResult<List<MedicalRecords>>> GetMedicalRecordsByAnimal(int animalId)
        {
            var records = await _medicalRecordsService.GetMedicalRecordsByAnimalIdAsync(animalId);
            return Ok(records);
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecords>> CreateMedicalRecord([FromBody] MedicalRecords medicalRecord)
        {
            if (medicalRecord == null)
            {
                return BadRequest("Invalid medical record data.");
            }

            var createdRecord = await _medicalRecordsService.CreateMedicalRecordAsync(medicalRecord);
            return CreatedAtAction(nameof(GetMedicalRecord), new { id = createdRecord.Id }, createdRecord);
        }

        // PUT: api/MedicalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] MedicalRecords medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return BadRequest("Medical record ID mismatch.");
            }

            var updated = await _medicalRecordsService.UpdateMedicalRecordAsync(medicalRecord);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/MedicalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var deleted = await _medicalRecordsService.DeleteMedicalRecordAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
