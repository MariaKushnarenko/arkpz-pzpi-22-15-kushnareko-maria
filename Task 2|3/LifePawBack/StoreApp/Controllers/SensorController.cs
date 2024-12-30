using Microsoft.AspNetCore.Mvc;
using StoreApp.Services.Interfaces;
using StoreApp.DatabaseProvider.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorsController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<List<Sensor>>> GetSensors()
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            return Ok(sensors);
        }

        // GET: api/Sensors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }

        // GET: api/Sensors/animal/5
        [HttpGet("animal/{animalId}")]
        public async Task<ActionResult<List<Sensor>>> GetSensorsByAnimal(int animalId)
        {
            var sensors = await _sensorService.GetSensorsByAnimalIdAsync(animalId);
            return Ok(sensors);
        }

        // POST: api/Sensors
        [HttpPost]
        public async Task<ActionResult<Sensor>> CreateSensor([FromBody] Sensor sensor)
        {
            if (sensor == null)
            {
                return BadRequest("Invalid sensor data.");
            }

            var createdSensor = await _sensorService.CreateSensorAsync(sensor);
            return CreatedAtAction(nameof(GetSensor), new { id = createdSensor.Id }, createdSensor);
        }

        // PUT: api/Sensors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody] Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest("Sensor ID mismatch.");
            }

            var updated = await _sensorService.UpdateSensorAsync(sensor);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Sensors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var deleted = await _sensorService.DeleteSensorAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}
