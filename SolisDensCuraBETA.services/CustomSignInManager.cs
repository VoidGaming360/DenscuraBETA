using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    // Custom SignInManager class
    public class CustomSignInManager : SignInManager<ApplicationUser>
    {
        private readonly NotificationService _notificationService;

        public CustomSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation, NotificationService notificationService)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _notificationService = notificationService;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByNameAsync(userName);
                await CreateLoginNotification(user);
            }

            return result;
        }

        private async Task CreateLoginNotification(ApplicationUser user)
        {
            // Create a notification for the logged-in user
            await _notificationService.CreateNotificationAsync(user.Id, $"{user.UserName} has successfully logged in.");
        }
    }

}
