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

namespace GuguDadah.Pages {

    public class Register : PageModel {

        [BindProperty]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Email")]
        public string eMail { get; set; }

        [BindProperty]
        [Display(Name = "Contacto Telefónico")]
        public string contact { get; set; }

        [BindProperty]
        [Display(Name = "Password")]
        public string password { get; set; }

        [BindProperty]
        [Display(Name = "Confirmar Password")]
        public string confirmPassword { get; set; }

        [Required]
        [BindProperty]
        public IFormFile Avatar { get; set; }

        private readonly AppDbContext dbContext;

        public Register(AppDbContext context) {

            dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync() {
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

                    Client imageEntity = new Client() {
                        ID = 1,
                        userName = this.userName,
                        avatar = ms1.ToArray(),
                        password = this.password,
                        eMail = this.eMail,
                        contact = this.contact
                    };

                    dbContext.Clients.Add(imageEntity);

                    dbContext.SaveChanges();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
