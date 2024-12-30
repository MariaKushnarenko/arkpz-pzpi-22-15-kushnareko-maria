using Microsoft.AspNetCore.Mvc;
using StoreApp.Actions;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [HttpGet("{id}")]
        [AdminAuthorizationFilter]
        public async Task<IActionResult> GetShelterById(int id)
        {
            var shelter = await _shelterService.GetShelterByIdAsync(id);
            if (shelter == null)
                return NotFound();

            return Ok(shelter);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShelters()
        {
            var shelters = await _shelterService.GetAllSheltersAsync();
            return Ok(shelters);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShelter([FromBody] Shelter shelter)
        {
            if (shelter == null)
                return BadRequest("Shelter data is null.");

            var createdShelter = await _shelterService.CreateShelterAsync(shelter);
            return CreatedAtAction(nameof(GetShelterById), new { id = createdShelter.Id }, createdShelter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShelter(int id, [FromBody] Shelter shelter)
        {
            if (shelter == null || id != shelter.Id)
                return BadRequest("Invalid shelter data.");

            var result = await _shelterService.UpdateShelterAsync(shelter);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelter(int id)
        {
            var result = await _shelterService.DeleteShelterAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
