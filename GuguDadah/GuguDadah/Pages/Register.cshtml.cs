using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuguDadah.Pages
{
    public class Register : PageModel
    {
        public string Message { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }



        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
