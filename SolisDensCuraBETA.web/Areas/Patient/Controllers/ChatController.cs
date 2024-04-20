using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.services;
using SolisDensCuraBETA.viewmodels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SolisDensCuraBETA.repositories;
using SolisDensCuraBETA.repositories.Interfaces;

namespace SolisDensCuraBETA.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient, Dentist")]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IChatService _chatService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IChatService chatService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _chatService = chatService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string selectedUserId = null)
        {
            var currentUserId = GetCurrentUserId();
            var users = _userManager.Users.ToList();

            ViewBag.Users = users;
            ViewBag.CurrentUserId = currentUserId;
            ViewBag.SelectedUserId = selectedUserId;

            if (!string.IsNullOrEmpty(selectedUserId))
            {
                ViewBag.SelectedUserId = selectedUserId;

                var messages = _dbContext.ChatMessages
                .Where(m => (m.SenderId == selectedUserId && m.ReceiverId == currentUserId) ||
                            (m.SenderId == currentUserId && m.ReceiverId == selectedUserId))
                .AsEnumerable() // Execute the query in memory
                .Select(m => new ChatMessageViewModel
                {
                    SenderName = m.SenderId == currentUserId ? GetCurrentUser().Name : (users.FirstOrDefault(u => u.Id == selectedUserId) != null ? users.FirstOrDefault(u => u.Id == selectedUserId).Name : ""),
                    Message = m.Message,
                    SentAt = m.SentAt // Add SentAt property
                })
                .OrderBy(m => m.SentAt) // Order the messages by SentAt
                .ToList();

                // Order the messages by SentAt
                messages = messages.OrderBy(m => m.SentAt).ToList();

                ViewBag.Messages = messages;
            }
            else
            {
                // If no user selected, display messages for the first user in the list
                var firstUser = users.FirstOrDefault();
                if (firstUser != null)
                {
                    var messages = _dbContext.ChatMessages
                        .Where(m => (m.SenderId == currentUserId && m.ReceiverId == firstUser.Id) ||
                                    (m.SenderId == firstUser.Id && m.ReceiverId == currentUserId))
                        .OrderBy(m => m.SentAt)
                        .Select(m => new ChatMessageViewModel
                        {
                            SenderName = m.SenderId == currentUserId ? GetCurrentUser().UserName : firstUser.UserName,
                            Message = m.Message
                        })
                        .ToList();

                    ViewBag.Messages = messages;
                }
                else
                {
                    // If no users found, return an empty list of messages
                    ViewBag.Messages = new List<ChatMessageViewModel>();
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverId, string message)
        {
            var senderId = GetCurrentUserId(); // Get sender's ID

            await _chatService.SendMessageAsync(senderId, receiverId, message);

            // Redirect back to the chat page after sending the message
            return RedirectToAction("Index");
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(User).Result;
        }

        [HttpGet]
        public IActionResult GetMessages(string receiverId)
        {
            // Retrieve messages where either senderId or receiverId matches the provided receiverId
            var messages = _dbContext.ChatMessages
                .Where(m => m.SenderId == receiverId || m.ReceiverId == receiverId)
                .Select(m => new ChatMessageViewModel
                {
                    SenderName = m.Sender.Name,
                    ReceiverName = m.Receiver.Name,
                    Message = m.Message
                })
                .ToList();

            return Json(messages); // Return messages as JSON
        }
    }
}

