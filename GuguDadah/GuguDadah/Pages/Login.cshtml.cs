using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using GuguDadah.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages
{
    [AllowAnonymous]
    public class Login : PageModel
    {

        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [BindProperty]
        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }

        //instanciando uma classe de serviço por injeção
        private IUserService _userService;
        public string Message { get; private set; } = string.Empty;

        public Login(IUserService userService) {

            _userService = userService;
        }

        //fazendo bind da model para ser usada no front-end
        [BindProperty]
        public Client client { get; set; }


        //metodo Get inicial
        public void OnGet() {
            //verifica se o usuário está autenticado
            if (User.Identity.IsAuthenticated) {
                Message += "Olá User, você está autenticado";

            }
            else {
                Message += "Você não está autenticado";
            }
        }

        //metodo post do formulario
        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return RedirectToPage("/Index");
            }

            //faz a busca do usuário e verifica se existe
            var user = _userService.Authenticate(client.userName, client.password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                 new Claim(ClaimTypes.Name, user.userName)
            };


            //faz autenticação via Cookie
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties {
                    IsPersistent = this.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });

            // redireciona para a Index novamente, porém já autenticado
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLogout() {

            Response.Cookies.Delete("GuguDadahLogin");

            return RedirectToPage("/Index");
        }

    }
}