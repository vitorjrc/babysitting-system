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
    public class Register : PageModel {

        [Required]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile Avatar { get; set; }

        [BindProperty]
        public Client client { get; set; }

        private readonly AppDbContext dbContext;

        public Register(AppDbContext context) {

            dbContext = context;
        }

        public IActionResult OnPostRegisterClient() {

            ModelState.Remove("client.Avatar");
            ModelState.Remove("client.Status");

            if (!ModelState.IsValid) return Page();


            if (ModelState.IsValid) {

                //check whether name is already exists in the database or not
                bool clientAlreadyExists = dbContext.Clients.Any(o => o.UserName == client.UserName);
                bool professionalAlreadyExists = dbContext.Professionals.Any(o => o.UserName == client.UserName);

                if (clientAlreadyExists || professionalAlreadyExists || client.UserName.Equals("admin")) {

                    ModelState.AddModelError(string.Empty, "O nickname já existe.");

                    return Page();
                }

                if (ConfirmPassword != client.Password) {

                    ModelState.AddModelError(string.Empty, "Passwords não coincidem");

                    return Page();
                }
            }


            IFormFile uploadedImage = Avatar;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user.png");

            byte[] data = null;

            MemoryStream ms1 = new MemoryStream();

            if (uploadedImage != null && uploadedImage.ContentType.ToLower().StartsWith("image/")) {

                MemoryStream ms = new MemoryStream();
                uploadedImage.OpenReadStream().CopyTo(ms);
                data = ms.ToArray();

                using (MagickImage image = new MagickImage(data)) {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);
                    image.Format = MagickFormat.Jpeg;

                    // Save the result
                    image.Write(ms1);

                }
            }

            if (uploadedImage == null) {
                using (MagickImage image = new MagickImage(path)) {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    // Save the result
                    image.Write(ms1);

                }
            }


            Client newClient = new Client() {
                UserName = client.UserName,
                Avatar = ms1.ToArray(),
                Password = client.Password,
                Email = client.Email,
                Contact = client.Contact,
                Name = client.Name,
                Status = "N"
            };

            dbContext.Clients.Add(newClient);

            dbContext.SaveChanges();

            return RedirectToPage("./Index");
        }

    }
}
