using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SolisDensCuraBETA.services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context; // Assuming ApplicationDbContext is your EF context
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationService(ApplicationDbContext context, IHubContext<NotificationHub> hubContext, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        public IEnumerable<Notification> GetNotifications(ClaimsPrincipal user)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(user);
                // Query the database for notifications belonging to the user
                var notifications = _context.Notifications
                    .Where(n => n.UserId == userId)
                    .ToList(); // Execute the query and retrieve the notifications

                return notifications;
            }
            return Enumerable.Empty<Notification>();
        }

        public async Task CreateNotificationAsync(string userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                Timestamp = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send notification to user using SignalR
            await SendNotificationToUser(userId, message);
        }
        private async Task SendNotificationToUser(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
