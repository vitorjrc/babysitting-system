using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    public class Index : PageModel
    {
        public void OnGet()
        {

        }
    }
}
