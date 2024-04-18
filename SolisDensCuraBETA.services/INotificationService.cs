using SolisDensCuraBETA.model;
using System.Security.Claims;

namespace SolisDensCuraBETA.services
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetNotifications(ClaimsPrincipal user);
        Task CreateNotificationAsync(string userId, string message);
    }
}