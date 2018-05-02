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

        // método acedido quando o utilizador carrega em criar conta
        [AllowAnonymous]
        public IActionResult OnPostCreateAccount() {

            TryUpdateModelAsync(this);

            ModelState.Remove("Client.Avatar");
            ModelState.Remove("Client.Status");

            // retorna erros se os campos foram incorretamente preenchidos ou não preenchidos
            if (!ModelState.IsValid) return Page();

            if (ModelState.IsValid) {

                // verifica se o username já existe
                bool clientUsernameAlreadyExists = dbContext.Clients.Any(o => o.UserName == Client.UserName);
                bool professionalUsernameAlreadyExists = dbContext.Professionals.Any(o => o.UserName == Client.UserName);

                // retorna erro em caso de já existir
                if (clientUsernameAlreadyExists || professionalUsernameAlreadyExists || Client.UserName.Equals("admin")) {

                    ModelState.AddModelError(string.Empty, "Este nickname já existe no sistema.");

                    return Page();
                }

                // verifica se o email já existe
                bool clientEmailAlreadyExists = dbContext.Clients.Any(o => o.Email == Client.Email);
                bool professionalEmailAlreadyExists = dbContext.Professionals.Any(o => o.Email == Client.Email);

                // retorna erro em caso de já existir
                if (clientEmailAlreadyExists || professionalEmailAlreadyExists) {

                    ModelState.AddModelError(string.Empty, "Este email já existe no sistema.");

                    return Page();
                }

                // retorna erro se as passwords não coincidirem
                if (ConfirmPassword != Client.Password) {

                    ModelState.AddModelError(string.Empty, "Passwords não coincidem");

                    return Page();
                }
            }

            // encripta a password
            var hash = BCrypt.Net.BCrypt.HashPassword(Client.Password);

            // cria o cliente
            Client newClient = new Client() {
                UserName = Client.UserName,
                Avatar = GetAvatar(Avatar).ToArray(),
                Password = hash,
                Email = Client.Email,
                Contact = Client.Contact,
                Name = Client.Name,
                Status = "N"
            };

            // adiciona cliente à BD
            dbContext.Clients.Add(newClient);

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("./Index").WithSuccess("Utilizador", "registado com sucesso.", "3000");

        }
    }
}