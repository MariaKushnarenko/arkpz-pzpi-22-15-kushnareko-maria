using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class ShelterService : IShelterService
    {
        private readonly DataContext _dataContext;

        public ShelterService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Shelter> GetShelterByIdAsync(int id)
        {
            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == id);

            return shelter;
        }

        public async Task<Shelter> CreateShelterAsync(Shelter shelter)
        {
            await _dataContext.Shelter.AddAsync(shelter);
            await _dataContext.SaveChangesAsync();

            return shelter;
        }

        public async Task<List<Shelter>> GetAllSheltersAsync()
        {
            return await _dataContext.Shelter.ToListAsync();
        }

        public async Task<bool> UpdateShelterAsync(Shelter shelter)
        {
            var existingShelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == shelter.Id);

            if (existingShelter == null)
                return false;

            existingShelter.Name = shelter.Name;
            existingShelter.Location = shelter.Location;
            existingShelter.Contacts = shelter.Contacts;

            _dataContext.Shelter.Update(existingShelter);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteShelterAsync(int id)
        {
            var shelter = await _dataContext.Shelter.FirstOrDefaultAsync(x => x.Id == id);

            if (shelter == null)
                return false;

            _dataContext.Shelter.Remove(shelter);
            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}
