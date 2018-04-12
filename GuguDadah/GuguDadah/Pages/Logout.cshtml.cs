using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GuguDadah.Pages
{
    public class Logout : PageModel
    {
        private readonly SignInManager<UserService> _signInManager;
        private readonly ILogger _logger;

        public Logout(SignInManager<UserService> signInManager, ILogger<Logout> logger) {
            _signInManager = signInManager;
            _logger = logger;
        }
        public async Task<IActionResult> OnGet() {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }
    }
}