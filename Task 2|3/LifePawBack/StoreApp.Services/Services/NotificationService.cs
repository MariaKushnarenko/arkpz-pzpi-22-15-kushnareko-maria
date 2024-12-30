using Microsoft.EntityFrameworkCore;
using StoreApp.DatabaseProvider;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext _dataContext;

        public NotificationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _dataContext.Notifications.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _dataContext.Notifications.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _dataContext.Notifications.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Notification>> GetNotificationsByAnimalIdAsync(int animalId)
        {
            return await _dataContext.Notifications.Include(x => x.User).Include(x => x.Animal).ThenInclude(x => x.Shelter).Where(x => x.AnimalId == animalId).ToListAsync();
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            var animal = await _dataContext.Animals.FirstOrDefaultAsync(x => x.Id == notification.AnimalId);
            notification.Animal = animal;
            var user = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == notification.AnimalId);
            notification.User = user;
            await _dataContext.Notifications.AddAsync(notification);
            await _dataContext.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> UpdateNotificationAsync(Notification notification)
        {
            var existingNotification = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == notification.Id);
            if (existingNotification == null) return false;

            existingNotification.Type = notification.Type;
            existingNotification.Message = notification.Message;
            existingNotification.DateTime = notification.DateTime;
            existingNotification.UserId = notification.UserId;
            existingNotification.AnimalId = notification.AnimalId;

            _dataContext.Notifications.Update(existingNotification);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == id);
            if (notification == null) return false;

            _dataContext.Notifications.Remove(notification);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}
