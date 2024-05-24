using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using SolisDensCuraBETA.model;

namespace SolisDensCuraBETA.services.Interface
{
    public interface INotificationService
    {
        IEnumerable<Notification> GetNotifications(ClaimsPrincipal user);
        Task CreateNotificationAsync(string userId, string message);
        void RegisterNotifications(ClaimsPrincipal user);
    }
}
