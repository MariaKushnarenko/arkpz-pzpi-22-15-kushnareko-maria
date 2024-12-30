using Microsoft.AspNetCore.Mvc;
using StoreApp.Services.Interfaces;
using StoreApp.DatabaseProvider.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<List<Notification>>> GetNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // GET: api/Notifications/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Notification>>> GetNotificationsByUser(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        // GET: api/Notifications/animal/5
        [HttpGet("animal/{animalId}")]
        public async Task<ActionResult<List<Notification>>> GetNotificationsByAnimal(int animalId)
        {
            var notifications = await _notificationService.GetNotificationsByAnimalIdAsync(animalId);
            return Ok(notifications);
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification([FromBody] Notification notification)
        {
            if (notification == null)
            {
                return BadRequest("Invalid notification data.");
            }

            var createdNotification = await _notificationService.CreateNotificationAsync(notification);
            return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.Id }, createdNotification);
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest("Notification ID mismatch.");
            }

            var updated = await _notificationService.UpdateNotificationAsync(notification);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var deleted = await _notificationService.DeleteNotificationAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
