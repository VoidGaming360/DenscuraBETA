using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services.Interface;
using SolisDensCuraBETA.viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient, Dentist")]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChatService _chatService;

        public ChatController(UserManager<ApplicationUser> userManager, IChatService chatService)
        {
            _userManager = userManager;
            _chatService = chatService;
        }

        public async Task<IActionResult> Index(string selectedUserId = null)
        {
            var currentUserId = GetCurrentUserId();
            var users = await _userManager.Users.ToListAsync();

            // Filter out the current user and users with the "Admin" role
            var filteredUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (user.Id != currentUserId && !(await _userManager.IsInRoleAsync(user, "Admin")))
                {
                    filteredUsers.Add(user);
                }
            }

            var usersWithMessages = new List<(ApplicationUser User, DateTime? LastMessageDate)>();

            foreach (var user in filteredUsers)
            {
                var messages = await _chatService.GetMessagesAsync(user.Id, currentUserId);
                var lastMessageDate = messages.Max(m => (DateTime?)m.SentAt);
                usersWithMessages.Add((user, lastMessageDate));
            }

            var sortedUsers = usersWithMessages
                .OrderByDescending(u => u.LastMessageDate)
                .Select(u => u.User)
                .ToList();

            ViewBag.Users = sortedUsers;
            ViewBag.CurrentUserId = currentUserId;
            ViewBag.SelectedUserId = selectedUserId;

            if (!string.IsNullOrEmpty(selectedUserId))
            {
                var messages = await _chatService.GetMessagesAsync(currentUserId, selectedUserId);
                var messageViewModels = messages.Select(m => new ChatMessageViewModel
                {
                    SenderName = m.SenderId == currentUserId ? GetCurrentUser().UserName : m.Sender.UserName,
                    ReceiverName = m.ReceiverId == currentUserId ? GetCurrentUser().UserName : m.Receiver.UserName,
                    Message = m.Message,
                    SentAt = m.SentAt,
                    ReceiverId = selectedUserId // Set ReceiverId for referencing
                }).ToList();

                ViewBag.Messages = messageViewModels;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverId, string message)
        {
            var senderId = GetCurrentUserId();

            await _chatService.SendMessageAsync(senderId, receiverId, message);

            return RedirectToAction("Index", new { selectedUserId = receiverId });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }
    }
}
