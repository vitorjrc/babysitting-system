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

namespace GuguDadah.Pages
{

    public class EditProfile : PageModel
    {
        [Required]
        [BindProperty]
        [Display(Name = "Nome")]
        public String NameModel { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Contacto")]
        public int ContactModel { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Turno: M, T ou N")]
        public String ShiftModel { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Avatar")]
        public IFormFile AvatarModel { get; set; }

        private readonly AppDbContext dbContext;

        public EditProfile(AppDbContext context)
        {

            dbContext = context;
        }

        public void OnGet()
        {
            if (User.IsInRole("Client"))
            {
                Client Client = dbContext.Clients.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));
                NameModel = Client.Name;
                ContactModel = (int) Client.Contact;
            }

            if (User.IsInRole("Professional"))
            {
                Professional Professional = dbContext.Professionals.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));
                NameModel = Professional.Name;
                ContactModel = (int) Professional.Contact;
                ShiftModel = Professional.Shift;
            }
        }

        // público porque o método é acedido pelo RegisterProfessional
        public MemoryStream GetAvatar(IFormFile imageUploaded)
        {

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user.png");

            byte[] data = null;

            MemoryStream ms1 = new MemoryStream();

            if (imageUploaded != null && imageUploaded.ContentType.ToLower().StartsWith("image/"))
            {

                MemoryStream ms = new MemoryStream();
                imageUploaded.OpenReadStream().CopyTo(ms);
                data = ms.ToArray();

                using (MagickImage image = new MagickImage(data))
                {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);
                    image.Format = MagickFormat.Jpeg;

                    // Save the result
                    image.Write(ms1);

                }

                return ms1;
            }

            if (imageUploaded == null)
            {
                using (MagickImage image = new MagickImage(path))
                {

                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    image.Resize(size);

                    // Save the result
                    image.Write(ms1);

                }
            }

            return ms1;
        }

        public IActionResult OnPostUpdateClientAccount()
        {
            TryUpdateModelAsync(this);

            ModelState.Remove("ShiftModel");
            ModelState.Remove("AvatarModel");

            Client oldClient = dbContext.Clients.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

            if (!ModelState.IsValid) return Page();

            if (AvatarModel != null)
            {

                oldClient.Avatar = GetAvatar(AvatarModel).ToArray();
            }

            oldClient.Name = NameModel;
            oldClient.Contact = ContactModel;

            dbContext.Entry(oldClient).State = EntityState.Modified;

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithSuccess("Perfil", "editado com sucesso.", "3000");

        }

        [Authorize(Roles = "Professional")]
        public IActionResult OnPostUpdateProfessionalAccount()
        {

            TryUpdateModelAsync(this);

            ModelState.Remove("AvatarModel");

            Register register = new Register(dbContext);

            Professional oldProfessional = dbContext.Professionals.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

            if (!ModelState.IsValid) return Page();

            if (ShiftModel != "M" && ShiftModel != "T" && ShiftModel != "N")
            {

                ModelState.AddModelError(string.Empty, "Turno inválido.");

                return Page();
            }

            if (AvatarModel != null)
            {

                oldProfessional.Avatar = register.GetAvatar(AvatarModel).ToArray();
            }

            oldProfessional.Name = NameModel;
            oldProfessional.Contact = ContactModel;
            oldProfessional.Shift = ShiftModel;

            dbContext.Entry(oldProfessional).State = EntityState.Modified;

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Perfil", "editado com sucesso.", "3000");

        }
    }
}