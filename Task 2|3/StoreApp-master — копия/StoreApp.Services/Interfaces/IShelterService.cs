using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface IShelterService
    {
        Task<Shelter> GetShelterByIdAsync(int id);
        Task<Shelter> CreateShelterAsync(Shelter shelter);
        Task<List<Shelter>> GetAllSheltersAsync();
        Task<bool> UpdateShelterAsync(Shelter shelter);
        Task<bool> DeleteShelterAsync(int id);
    }
}
