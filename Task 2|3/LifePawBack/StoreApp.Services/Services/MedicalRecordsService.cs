using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class MedicalRecordsService : IMedicalRecordsService
    {
        private readonly DataContext _dataContext;

        public MedicalRecordsService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MedicalRecords> GetMedicalRecordByIdAsync(int id)
        {
            return await _dataContext.MedicalRecords.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MedicalRecords>> GetAllMedicalRecordsAsync()
        {
            return await _dataContext.MedicalRecords.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).ToListAsync();
        }

        public async Task<List<MedicalRecords>> GetMedicalRecordsByAnimalIdAsync(int animalId)
        {
            return await _dataContext.MedicalRecords.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).Where(x => x.AnimalId == animalId).ToListAsync();
        }

        public async Task<MedicalRecords> CreateMedicalRecordAsync(MedicalRecords medicalRecord)
        {
            var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == medicalRecord.AnimalId);
            medicalRecord.Animal = animal;
            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == animal.ShelterId);
            animal.Shelter = shelter;
            var user = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == medicalRecord.AnimalId);
            medicalRecord.User = user;
            await _dataContext.MedicalRecords.AddAsync(medicalRecord);
            await _dataContext.SaveChangesAsync();
            return medicalRecord;
        }

        public async Task<bool> UpdateMedicalRecordAsync(MedicalRecords medicalRecord)
        {
            var existingRecord = await _dataContext.MedicalRecords.FirstOrDefaultAsync(x => x.Id == medicalRecord.Id);
            if (existingRecord == null) return false;

            existingRecord.AnimalId = medicalRecord.AnimalId;
            existingRecord.Date = medicalRecord.Date;
            existingRecord.Description = medicalRecord.Description;
            existingRecord.UserId = medicalRecord.UserId;

            _dataContext.MedicalRecords.Update(existingRecord);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMedicalRecordAsync(int id)
        {
            var record = await _dataContext.MedicalRecords.FirstOrDefaultAsync(x => x.Id == id);
            if (record == null) return false;

            _dataContext.MedicalRecords.Remove(record);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
