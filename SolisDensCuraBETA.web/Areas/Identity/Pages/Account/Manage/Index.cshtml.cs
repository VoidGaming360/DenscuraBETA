using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolisDensCuraBETA.model;
using SolisDensCuraBETA.utilities;

namespace SolisDensCuraBETA.web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ImageOperations _imageOperations;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ImageOperations imageOperations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageOperations = imageOperations;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string PictureUri { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Name { get; set; }

            public string Nationality { get; set; }

            public string Address { get; set; }

            public Gender Gender { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var pictureUri = user.PictureUri;

            Username = userName;
            PictureUri = pictureUri;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = user.Name,
                Nationality = user.Nationality,
                Address = user.Address,
                Gender = user.Gender
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile profilePicture)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (profilePicture != null)
            {
                var pictureUri = await _imageOperations.ImageUpload(profilePicture);
                if (!string.IsNullOrEmpty(pictureUri))
                {
                    user.PictureUri = pictureUri;
                }
            }

            // Update additional user information
            user.Name = Input.Name;
            user.Nationality = Input.Nationality;
            user.Address = Input.Address;
            user.Gender = Input.Gender;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
