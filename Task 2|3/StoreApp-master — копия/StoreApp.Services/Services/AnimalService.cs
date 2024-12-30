using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly DataContext _dataContext;

        public AnimalService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Animal> GetAnimalByIdAsync(int id)
        {
            var animal = await _dataContext.Animals.Include(x => x.Shelter).FirstOrDefaultAsync(x => x.Id == id);
            return animal;
        }

        public async Task<List<Animal>> GetAllAnimalsAsync()
        {
            return await _dataContext.Animals.Include(x => x.Shelter).ToListAsync();
        }

        public async Task<List<Animal>> GetAnimalsByShelterIdAsync(int shelterId)
        {
            return await _dataContext.Animals.Include(x => x.Shelter).Where(x => x.ShelterId == shelterId).ToListAsync();
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {

            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == animal.ShelterId);
            animal.Shelter = shelter;
            await _dataContext.Animals.AddAsync(animal);
            await _dataContext.SaveChangesAsync();
            animal.Shelter = shelter;
            return animal;
        }

        public async Task<bool> UpdateAnimalAsync(Animal animal)
        {
            var existingAnimal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == animal.Id);
            if (existingAnimal == null) return false;

            existingAnimal.Name = animal.Name;
            existingAnimal.Species = animal.Species;
            existingAnimal.Breed = animal.Breed;
            existingAnimal.Age = animal.Age;
            existingAnimal.ShelterId = animal.ShelterId;

            _dataContext.Animals.Update(existingAnimal);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnimalAsync(int id)
        {
            var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == id);
            if (animal == null) return false;

            _dataContext.Animals.Remove(animal);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
