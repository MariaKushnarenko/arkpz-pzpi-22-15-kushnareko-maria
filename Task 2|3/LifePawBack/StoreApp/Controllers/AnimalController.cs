using Microsoft.AspNetCore.Mvc;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnimalById(int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            if (animal == null)
                return NotFound();

            return Ok(animal);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnimals()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            return Ok(animals);
        }

        [HttpGet("shelter/{shelterId}")]
        public async Task<IActionResult> GetAnimalsByShelterId(int shelterId)
        {
            var animals = await _animalService.GetAnimalsByShelterIdAsync(shelterId);
            return Ok(animals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal([FromBody] Animal animal)
        {
            if (animal == null)
                return BadRequest("Animal data is null.");

            var createdAnimal = await _animalService.CreateAnimalAsync(animal);
            return CreatedAtAction(nameof(GetAnimalById), new { id = createdAnimal.Id }, createdAnimal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal animal)
        {
            if (animal == null || id != animal.Id)
                return BadRequest("Invalid animal data.");

            var result = await _animalService.UpdateAnimalAsync(animal);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var result = await _animalService.DeleteAnimalAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
