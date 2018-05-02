using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Authentication;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Professional")]
    public class GPSLocation : PageModel {

        public string Address{ get; set; }

        public IActionResult OnGet() {

            return Unauthorized();
        }

        // este método é chamado quando se carrega no botão localização e atribui o endereço à variável local.
        public void OnPostGPSLocation(string address) {

            Address = address;
        }

    }
}