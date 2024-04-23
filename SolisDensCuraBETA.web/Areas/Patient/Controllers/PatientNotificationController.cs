using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SolisDensCuraBETA.utilities;


namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    public class PatientNotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public PatientNotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<IActionResult> PateintNotifications()
        {
            // Trigger notification
            string userId = "userId"; // Specify the recipient user ID
            string message = "Your notification message";
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);

            // Other logic...

            return RedirectToAction("Index"); // Or any other action
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
