using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly DataContext _context;
        public NotificationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
        {
            notification.DateCreated = DateTime.UtcNow;
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }

        [HttpPut("user/{userId}/mark-as-read")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            var userNotifications = await _context.Notifications.Where(n => n.userId == userId).ToListAsync();

            if (userNotifications == null || userNotifications.Count == 0)
            {
                return NotFound();
            }

            foreach (var notification in userNotifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return NoContent();
}


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByUserId(string userId)
        {
            var userNotifications = await _context.Notifications.Where(n => n.userId == userId).ToListAsync();

            if (userNotifications == null || userNotifications.Count == 0)
            {
                return NotFound();
            }

            return Ok(userNotifications);
        }

        // Get a specific notification by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotificationById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // Delete a specific notification by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationById(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete all notifications for a specific user
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> DeleteNotificationsByUserId(string userId)
        {
            var userNotifications = await _context.Notifications.Where(n => n.userId == userId).ToListAsync();

            if (userNotifications == null || userNotifications.Count == 0)
            {
                return NotFound();
            }

            _context.Notifications.RemoveRange(userNotifications);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Get count of all unread notifications for a specific user
        [HttpGet("user/{userId}/unread/count")]
        public async Task<ActionResult<int>> GetUnreadNotificationCountByUserId(string userId)
        {
            var unreadCount = await _context.Notifications.CountAsync(n => n.userId == userId && !n.IsRead);
            return Ok(unreadCount);
        }
    }
}
