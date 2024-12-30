using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class SensorService : ISensorService
    {
        private readonly DataContext _dataContext;

        public SensorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Sensor> GetSensorByIdAsync(int id)
        {
            return await _dataContext.Sensor.Include(x => x.Animal).ThenInclude(x => x.Shelter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await _dataContext.Sensor.Include(x => x.Animal).ThenInclude(x => x.Shelter).ToListAsync();
        }

        public async Task<List<Sensor>> GetSensorsByAnimalIdAsync(int animalId)
        {
            return await _dataContext.Sensor.Include(x => x.Animal).ThenInclude(x => x.Shelter).Where(x => x.AnimalId == animalId).ToListAsync();
        }

        public async Task<Sensor> CreateSensorAsync(Sensor sensor)
        {
            var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == sensor.AnimalId);
            sensor.Animal = animal;
            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == animal.ShelterId);
            animal.Shelter = shelter;
            //var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == sensor.AnimalId);
            //sensor.Animal = animal;
            await _dataContext.Sensor.AddAsync(sensor);
            await _dataContext.SaveChangesAsync();
            return sensor;
        }

        public async Task<bool> UpdateSensorAsync(Sensor sensor)
        {
            var existingSensor = await _dataContext.Sensor.FirstOrDefaultAsync(x => x.Id == sensor.Id);
            if (existingSensor == null) return false;

            existingSensor.Temperature = sensor.Temperature;
            existingSensor.Humidity = sensor.Humidity;
            existingSensor.InstallationDate = sensor.InstallationDate;
            existingSensor.Timestamp = sensor.Timestamp;
            var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == sensor.AnimalId);
            existingSensor.Animal = animal;

            _dataContext.Sensor.Update(existingSensor);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSensorAsync(int id)
        {
            var sensor = await _dataContext.Sensor.FirstOrDefaultAsync(x => x.Id == id);
            if (sensor == null) return false;

            _dataContext.Sensor.Remove(sensor);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}