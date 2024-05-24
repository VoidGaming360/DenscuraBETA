using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolisDensCuraBETA.services.Interface;

namespace SolisDensCuraBETA.web.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private IApplicationUserService _userService;

        public UsersController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int PageNumber=1, int PageSize=10)
        {

            return View(_userService.GetAll(PageNumber, PageSize));
        }

        public IActionResult AllDentists(int PageNumber = 1, int PageSize = 10)
        {

            return View(_userService.GetAllDentist(PageNumber, PageSize));
        }
    }
}
