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

        public void OnGetClientLoggedIn() {

            var userName = User.Identity.Name;

            client = dbContext.Clients.FirstOrDefault(m => m.userName.Equals(userName));

        }

        public void OnGetProfessionalLoggedIn() {

            var userName = User.Identity.Name;

            professional = dbContext.Professionals.FirstOrDefault(m => m.userName.Equals(userName));

        }

    }
}