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

namespace GuguDadah.Pages {

    [AllowAnonymous]
    public class RegisterProfessional : PageModel {

        [BindProperty]
        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Email")]
        public string eMail { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Contacto Telefónico")]
        public string contact { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

        [BindProperty]
        [Required]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirmar Password")]
        public string confirmPassword { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Turno")]
        public string shift { get; set; }

        [BindProperty]
        public IFormFile Avatar { get; set; }

        private readonly AppDbContext dbContext;

        public RegisterProfessional(AppDbContext context) {

            dbContext = context;
        }

        [HttpPost]
        public IActionResult OnPost() {
            IFormFile uploadedImage = Avatar;
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/")) {

                {

                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);
                    byte[] data = ms.ToArray();


                    MemoryStream ms1 = new MemoryStream();

                    using (MagickImage image = new MagickImage(data)) {

                        image.Resize(200, 0);
                        image.Format = MagickFormat.Jpeg;

                        // Save the result
                        image.Write(ms1);
                    }

                    Professional newProfessional = new Professional() {
                        userName = this.userName,
                        avatar = ms1.ToArray(),
                        password = this.password,
                        eMail = this.eMail,
                        contact = this.contact,
                        shift = this.shift
                    };

                    dbContext.Professionals.Add(newProfessional);

                    dbContext.SaveChanges();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
