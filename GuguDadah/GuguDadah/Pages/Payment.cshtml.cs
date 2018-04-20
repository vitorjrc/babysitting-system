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

        private readonly AppDbContext dbContext;

        public Payment(AppDbContext context) {

            dbContext = context;
        }

        public void OnPostPaymentEnter(string username) {

            TempData["proUsername"] = username;

        }

        public IActionResult OnPostChoosedPayment() {

            TryUpdateModelAsync(this);

            Work work;
            work = JsonConvert.DeserializeObject<Work>(TempData["tempWork"].ToString());

            if (work == null) return Page();

            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(TempData["proUsername"]));

            Client Client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(User.Identity.Name));

            if (PaymentType.Equals("money")) work.Payment = "N";

            if (PaymentType.Equals("card")) work.Payment= "S";

            work.Client = Client;
            work.Professional = professional;

            dbContext.Works.Add(work);

            dbContext.SaveChanges();

            return RedirectToPage("/Index").WithSuccess("Agendamento", "efetuado com sucesso.", "3000");
        }
    }
}