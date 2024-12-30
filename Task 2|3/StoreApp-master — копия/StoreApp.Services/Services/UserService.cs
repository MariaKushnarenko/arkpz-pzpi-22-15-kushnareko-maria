using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dataContext.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dataContext.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dataContext.User.ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _dataContext.User.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (existingUser == null) return false;

            existingUser.Name = user.Name;
            existingUser.Role = user.Role;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            _dataContext.User.Update(existingUser);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;

            _dataContext.User.Remove(user);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
