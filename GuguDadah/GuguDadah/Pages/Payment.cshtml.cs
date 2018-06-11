using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json;
using GuguDadah.Includes;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Client")]
    public class Payment : PageModel {

        [Required]
        [BindProperty]
        [Display(Name = "Método de Pagamento")]
        public string PaymentType { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Preço")]
        public double Cost { get; set; }

        private readonly AppDbContext dbContext;

        public Payment(AppDbContext context) {

            dbContext = context;
        }

        public IActionResult OnGet() {

            return Unauthorized();
        }

        // método chamado quando escolhe o babysitter... Recebe o username do profissional...
        public void OnPostPaymentEnter(string username) {

            // guarda o profisional num buffer
            TempData["proUsername"] = username;

            // recebe o custo da página anterior
            Cost = (double) TempData["cost2"];

        }

        // método chamado quando o utilizador paga
        public IActionResult OnPostChoosedPayment() {

            TryUpdateModelAsync(this);

            // recebe todo o trabalho
            Work work;
            work = JsonConvert.DeserializeObject<Work>(TempData["tempWork"].ToString());

            // se for null, alguma coisa de errado aconteceu
            if (work == null) return Page();

            // recebe o profissional anteriormente guardado e vai buscá-lo à BD
            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(TempData["proUsername"]));

            // vai buscar o cliente à BD
            Client Client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(User.Identity.Name));

            // define o pagamento
            if (PaymentType.Equals("money")) work.Payment = "N";

            if (PaymentType.Equals("card")) work.Payment= "S";

            work.Client = Client;
            work.Professional = professional;

            // envia email
            Email email = new Email();
            string destination = professional.Email;
            string subject = "GuguDadah - Oferta recebida";
            string body = "Recebeu uma oferta de trabalho. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            // adiciona o novo trabalho
            dbContext.Works.Add(work);

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithSuccess("Agendamento", "efetuado com sucesso.", "3000");
        }

        public IActionResult OnGetCancelSchedule() {

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithDanger("Agendamento", "cancelado.", "3000");
        }
    }
}