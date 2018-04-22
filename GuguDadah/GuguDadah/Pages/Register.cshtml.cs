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
using Microsoft.EntityFrameworkCore;

namespace GuguDadah.Pages {

    [AllowAnonymous]
    public class Register : PageModel {

        [Required]
        [BindProperty]
        [Display(Name = "Confirmar Password")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile Avatar { get; set; }

        [BindProperty]
        public Client Client { get; set; }

        private readonly AppDbContext dbContext;

        public Register(AppDbContext context) {

            dbContext = context;
        }

        // público porque o método é acedido pelo RegisterProfessional
        public MemoryStream GetAvatar(IFormFile imageUploaded) {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user.png");

            byte[] data = null;

            MemoryStream ms1 = new MemoryStream();

            if (imageUploaded != null && imageUploaded.ContentType.ToLower().StartsWith("image/")) {

                MemoryStream ms = new MemoryStream();
                imageUploaded.OpenReadStream().CopyTo(ms);
                data = ms.ToArray();

                using (MagickImage image = new MagickImage(data)) {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);
                    image.Format = MagickFormat.Jpeg;

                    // Save the result
                    image.Write(ms1);

                }

                return ms1;
            }

            if (imageUploaded == null) {
                using (MagickImage image = new MagickImage(path)) {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    // Save the result
                    image.Write(ms1);

                }
            }

            return ms1;
        }

        [AllowAnonymous]
        public IActionResult OnPostCreateAccount() {

            TryUpdateModelAsync(this);

            ModelState.Remove("Client.Avatar");
            ModelState.Remove("Client.Status");

            if (!ModelState.IsValid) return Page();


            if (ModelState.IsValid) {

                //check whether username is already exists in the database or not
                bool clientUsernameAlreadyExists = dbContext.Clients.Any(o => o.UserName == Client.UserName);
                bool professionalUsernameAlreadyExists = dbContext.Professionals.Any(o => o.UserName == Client.UserName);

                if (clientUsernameAlreadyExists || professionalUsernameAlreadyExists || Client.UserName.Equals("admin")) {

                    ModelState.AddModelError(string.Empty, "Este nickname já existe no sistema.");

                    return Page();
                }

                //check whether email is already exists in the database or not
                bool clientEmailAlreadyExists = dbContext.Clients.Any(o => o.Email == Client.Email);
                bool professionalEmailAlreadyExists = dbContext.Professionals.Any(o => o.Email == Client.Email);

                if (clientEmailAlreadyExists || professionalEmailAlreadyExists) {

                    ModelState.AddModelError(string.Empty, "Este email já existe no sistema.");

                    return Page();
                }

                if (ConfirmPassword != Client.Password) {

                    ModelState.AddModelError(string.Empty, "Passwords não coincidem");

                    return Page();
                }
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(Client.Password);

            Client newClient = new Client() {
                UserName = Client.UserName,
                Avatar = GetAvatar(Avatar).ToArray(),
                Password = hash,
                Email = Client.Email,
                Contact = Client.Contact,
                Name = Client.Name,
                Status = "N"
            };

            dbContext.Clients.Add(newClient);

            dbContext.SaveChanges();

            return RedirectToPage("./Index").WithSuccess("Utilizador", "registado com sucesso.", "3000");

        }
    }
}