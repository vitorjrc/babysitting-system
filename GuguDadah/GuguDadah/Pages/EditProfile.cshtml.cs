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

    public class EditProfile : PageModel {
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

        public EditProfile(AppDbContext context) {

            dbContext = context;
        }

        // método que é chamado automaticamente quando se entra na página
        public void OnGet() {

            // se o utilizador logado é um cliente
            if (User.IsInRole("Client")) {

                // vai buscar o cliente à BD
                Client Client = dbContext.Clients.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

                // passa o nome dele para a variável local da página que será mostrada na view
                NameModel = Client.Name;

                // passa o contacto dele para a variável local da página que será mostrada na view
                ContactModel = (int)Client.Contact;
            }

            // se o utilizador logado é um cliente
            if (User.IsInRole("Professional")) {

                // vai buscar o profissional à BD
                Professional Professional = dbContext.Professionals.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

                // passa o nome dele para a variável local da página que será mostrada na view
                NameModel = Professional.Name;

                // passa o contacto dele para a variável local da página que será mostrada na view
                ContactModel = (int)Professional.Contact;

                // passa o turno dele para a variável local da página que será mostrada na view
                ShiftModel = Professional.Shift;
            }
        }

        // recebe a imagem que o user deu update
        public MemoryStream GetAvatar(IFormFile imageUploaded) {

            // define a path do avatar default
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user.png");

            byte[] data = null;

            MemoryStream ms1 = new MemoryStream();

            // se a imagem do upload é diferente de nula e se é realmente uma IMAGEM
            if (imageUploaded != null && imageUploaded.ContentType.ToLower().StartsWith("image/")) {

                MemoryStream ms = new MemoryStream();

                // passa a imagem para um array de bytes
                imageUploaded.OpenReadStream().CopyTo(ms);
                data = ms.ToArray();

                // usa uma biblioteca de tratamento de imagens
                using (MagickImage image = new MagickImage(data)) {

                    // define um tamanho para a imagem (150x150)
                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    // dá resize da imagem
                    image.Resize(size);

                    // converte-a para JPEG
                    image.Format = MagickFormat.Jpeg;

                    // guarda o resultado
                    image.Write(ms1);

                }

                return ms1;
            }

            // se o que deu upload não é uma imagem ou se é null
            else {

                // vai buscar a imagem padrão
                using (MagickImage image = new MagickImage(path)) {

                    // define tamanho 150x150
                    MagickGeometry size = new MagickGeometry(150, 150);
                    size.IgnoreAspectRatio = true;

                    // dá resize à imagem
                    image.Resize(size);

                    // guarda o resultado
                    image.Write(ms1);

                }
            }

            return ms1;
        }

        // método chamado quando se carrega no botão editar cliente
        [Authorize(Roles = "Client")]
        public IActionResult OnPostUpdateClientAccount() {

            TryUpdateModelAsync(this);

            ModelState.Remove("ShiftModel");
            ModelState.Remove("AvatarModel");

            // vai buscar o cliente à BD
            Client oldClient = dbContext.Clients.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

            // retorna a página em si pq os campos estão mal preenchidos
            if (!ModelState.IsValid) return Page();

            // calcula o avatar/imagem
            if (AvatarModel != null) {

                oldClient.Avatar = GetAvatar(AvatarModel).ToArray();
            }

            // muda os dados do cliente na BD
            oldClient.Name = NameModel;
            oldClient.Contact = ContactModel;

            dbContext.Entry(oldClient).State = EntityState.Modified;

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithSuccess("Perfil", "editado com sucesso.", "3000");

        }

        [Authorize(Roles = "Professional")]
        public IActionResult OnPostUpdateProfessionalAccount() {

            TryUpdateModelAsync(this);

            ModelState.Remove("AvatarModel");

            // vai buscar o profissional à BD
            Professional oldProfessional = dbContext.Professionals.FirstOrDefault(o => o.UserName.Equals(User.Identity.Name));

            // retorna a página em si pq os campos estão mal preenchidos
            if (!ModelState.IsValid) return Page();

            // retorna erro se o utilizador mudou para um turno inválido
            if (ShiftModel != "M" && ShiftModel != "T" && ShiftModel != "N") {

                ModelState.AddModelError(string.Empty, "Turno inválido.");

                return Page();
            }

            // calcula o avatar
            if (AvatarModel != null) {

                oldProfessional.Avatar = GetAvatar(AvatarModel).ToArray();
            }

            // muda os dados do profissional na BD
            oldProfessional.Name = NameModel;
            oldProfessional.Contact = ContactModel;
            oldProfessional.Shift = ShiftModel;

            dbContext.Entry(oldProfessional).State = EntityState.Modified;

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Perfil", "editado com sucesso.", "3000");

        }
    }
}