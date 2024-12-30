using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface ISensorService
    {
        Task<Sensor> GetSensorByIdAsync(int id);
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<List<Sensor>> GetSensorsByAnimalIdAsync(int animalId);
        Task<Sensor> CreateSensorAsync(Sensor sensor);
        Task<bool> UpdateSensorAsync(Sensor sensor);
        Task<bool> DeleteSensorAsync(int id);
    }
}