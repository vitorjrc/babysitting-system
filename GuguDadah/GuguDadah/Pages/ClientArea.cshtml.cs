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

        public ClientArea(AppDbContext context) {

            dbContext = context;
        }

        public void OnGet() {

            Client client = dbContext.Clients.FirstOrDefault(m => m.ID == 1);

            this.userName = client.userName;
            this.eMail = client.eMail;
            this.contact = client.contact;
            this.Avatar = client.avatar;
        }

    }
}