using StoreApp.DatabaseProvider.Models;

namespace StoreApp.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<List<Notification>> GetAllNotificationsAsync();
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId);
        Task<List<Notification>> GetNotificationsByAnimalIdAsync(int animalId);
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<bool> UpdateNotificationAsync(Notification notification);
        Task<bool> DeleteNotificationAsync(int id);
    }
}