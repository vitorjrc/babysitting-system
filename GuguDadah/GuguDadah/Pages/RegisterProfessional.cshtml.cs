using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Diagnostics;
using ImageMagick;

using GuguDadah.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GuguDadah.Includes;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Admin")]
    public class RegisterProfessional : PageModel {

        [Required]
        [BindProperty]
        [Display(Name = "Confirmar Password")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile Avatar { get; set; }

        [BindProperty]
        public Professional Professional { get; set; }

        private readonly AppDbContext dbContext;

        public RegisterProfessional(AppDbContext context) {

            dbContext = context;
        }

        public IActionResult OnPost() {

            TryUpdateModelAsync(this);

            ModelState.Remove("Professional.Avatar");
            ModelState.Remove("Professional.Rating");
            ModelState.Remove("Professional.RegistrationDate");

            if (!ModelState.IsValid) return Page();


            if (ModelState.IsValid) {

                //check whether username is already exists in the database or not
                bool clientUsernameAlreadyExists = dbContext.Clients.Any(o => o.UserName == Professional.UserName);
                bool professionalUsernameAlreadyExists = dbContext.Professionals.Any(o => o.UserName == Professional.UserName);

                if (clientUsernameAlreadyExists || professionalUsernameAlreadyExists || Professional.UserName.Equals("admin")) {

                    ModelState.AddModelError(string.Empty, "Este nickname já existe no sistema.");

                    return Page();
                }

                //check whether email is already exists in the database or not
                bool clientEmailAlreadyExists = dbContext.Clients.Any(o => o.Email == Professional.Email);
                bool professionalEmailAlreadyExists = dbContext.Professionals.Any(o => o.Email == Professional.Email);

                if (clientEmailAlreadyExists || professionalEmailAlreadyExists) {

                    ModelState.AddModelError(string.Empty, "Este email já existe no sistema.");

                    return Page();
                }

                if (ConfirmPassword != Professional.Password) {

                    ModelState.AddModelError(string.Empty, "Passwords não coincidem");

                    return Page();
                }

                if (Professional.Shift != "M" && Professional.Shift != "T" && Professional.Shift != "N") {

                    ModelState.AddModelError(string.Empty, "Turno inválido.");

                    return Page();
                }
            }

            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            Register register = new Register(dbContext);

            Professional newProfessional = new Professional() {
                UserName = Professional.UserName,
                Avatar = register.GetAvatar(Avatar).ToArray(),
                Password = Professional.Password,
                Email = Professional.Email,
                Contact = Professional.Contact,
                Name = Professional.Name,
                Shift = Professional.Shift,
                Rating = 3,
                RegistrationDate = date
            };

            dbContext.Professionals.Add(newProfessional);

            dbContext.SaveChanges();

            return RedirectToPage("./AdminArea").WithSuccess("Profissional", "registado com sucesso.", "3000");
        }

    }
}