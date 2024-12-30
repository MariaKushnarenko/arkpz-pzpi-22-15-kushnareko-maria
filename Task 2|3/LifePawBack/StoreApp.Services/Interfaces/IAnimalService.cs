using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<Animal> GetAnimalByIdAsync(int id);
        Task<List<Animal>> GetAllAnimalsAsync();
        Task<List<Animal>> GetAnimalsByShelterIdAsync(int shelterId);
        Task<Animal> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(int id);
    }
}