// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using BadgeSpace.Models;
using BadgeSpace.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BadgeSpace.Areas.Identity.Pages.Account.Manage
{
    public class ApiKeyModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApiAuthService _apiAuthService;

        public ApiKeyModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApiAuthService apiAuthService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _apiAuthService = apiAuthService;
        }

        public string APIKey { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            APIKey = user.APIKey;
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
        public async Task<IActionResult> OnPostChangeTokenAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            string NewToken = await _apiAuthService.GenerateToken(user);
            user.APIKey = NewToken;
            await _userManager.UpdateAsync(user);
            await LoadAsync(user);
            return Page();
        }
    }
}
