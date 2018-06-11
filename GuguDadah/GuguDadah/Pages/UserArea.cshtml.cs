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

    [Authorize(Roles = "Client, Professional")]
    public class UserArea : PageModel {

        [BindProperty]
        public Client client { get; set; }

        [BindProperty]
        public Professional professional { get; set; }

        private readonly AppDbContext dbContext;

        public UserArea(AppDbContext context) {

            dbContext = context;
        }

        // método acedido automaticamente qd se entra na página
        public ActionResult OnGet() {

            // retorna unauthorized se o utilizador atual não estiver logado
            if (client == null && professional == null) return Unauthorized();

            return Page();
        }

        // retorna a área do cliente
        public ActionResult OnGetClientLoggedIn() {

            // nega o acesso de um profissional à área de um cliente
            if (User.IsInRole("Professional")) return Unauthorized();

            // username do utilizador logado
            var userName = User.Identity.Name;

            // vai buscar o cliente à BD
            client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(userName));

            return Page();

        }

        // retorna a área do profissional
        public ActionResult OnGetProfessionalLoggedIn() {

            // nega o acesso de um cliente à área de um profissional
            if (User.IsInRole("Client")) return Unauthorized();

            // username do utilizador logado
            var userName = User.Identity.Name;

            // vai buscar o profissional à BD
            professional = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(userName));

            return Page();

        }

    }
}