using SolisDensCuraBETA.model;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolisDensCuraBETA.services.Interface;

namespace SolisDensCuraBETA.services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
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
                var notifications = _context.Notifications
                    .Where(n => n.UserId == userId)
                    .ToList();

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

            await SendNotificationToUser(userId, message);
        }

        private async Task SendNotificationToUser(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public void RegisterNotifications(ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(user);
                CreateNotificationAsync(userId, "You have logged in").Wait();
            }
        }
    }
}
