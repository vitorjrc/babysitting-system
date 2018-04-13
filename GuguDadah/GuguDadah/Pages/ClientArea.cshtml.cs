using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;

using GuguDadah.Data;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Authentication;

namespace GuguDadah.Pages {

    public class ClientArea : PageModel {

        [BindProperty]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [BindProperty]
        [Display(Name = "Email")]
        public string eMail { get; set; }

        [BindProperty]
        [Display(Name = "Contacto Telefónico")]
        public string contact { get; set; }

        [BindProperty]
        public byte[] Avatar { get; set; }

        private readonly AppDbContext dbContext;
        private IUserService _userService;

        public ClientArea(AppDbContext context, IUserService userService) {

            _userService = userService;
            dbContext = context;
        }

        public void OnGet() {

            var userName = User.Identity.Name;

            Client client = dbContext.Clients.FirstOrDefault(m => m.userName.Equals(userName));

            this.userName = client.userName;
            this.eMail = client.eMail;
            this.contact = client.contact;
            this.Avatar = client.avatar;
        }

    }
}