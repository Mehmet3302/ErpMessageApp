// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Erp___Kurum_Ici_Haberlesme_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Erp___Kurum_Ici_Haberlesme_2.Data;

namespace Erp___Kurum_Ici_Haberlesme_2.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly ApplicationDbContext _context;

        public LogoutModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<LogoutModel> logger, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                user.OnlineDurumu = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
