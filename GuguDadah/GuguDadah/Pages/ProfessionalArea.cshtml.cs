using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages {

    [Authorize(Policy = "ProfessionalOnly")]
    public class ProfessionalArea : PageModel {

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

        public ProfessionalArea(AppDbContext context) {

            dbContext = context;
        }

        public void OnGet() {

            var userName = User.Identity.Name;

            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.userName.Equals(userName));

            this.userName = professional.userName;
            this.eMail = professional.eMail;
            this.contact = professional.contact;
            this.Avatar = professional.avatar;
        }

    }
}