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
        public async Task<IActionResult> OnGet() {

            await HttpContext.Authentication.SignOutAsync("codigosimples");

            return RedirectToPage("/Index");
        }

    }
}