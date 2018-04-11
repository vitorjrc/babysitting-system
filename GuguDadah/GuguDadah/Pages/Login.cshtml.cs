using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace GuguDadah.Pages
{
    public class LoginModel : PageModel
    {

        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        public void OnGet()
        {

        }
    }
}