using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IMedicalRecordsService
    {
        Task<MedicalRecords> GetMedicalRecordByIdAsync(int id);
        Task<List<MedicalRecords>> GetAllMedicalRecordsAsync();
        Task<List<MedicalRecords>> GetMedicalRecordsByAnimalIdAsync(int animalId);
        Task<MedicalRecords> CreateMedicalRecordAsync(MedicalRecords medicalRecord);
        Task<bool> UpdateMedicalRecordAsync(MedicalRecords medicalRecord);
        Task<bool> DeleteMedicalRecordAsync(int id);
    }
}
