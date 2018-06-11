using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using GuguDadah.Includes;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Client")]
    public class ClientActivity : PageModel {

        private readonly AppDbContext dbContext;

        [BindProperty]
        public float Rating { get; set; }

        public List<Work> ClientHistoryList { get; set; }

        public List<Work> ClientOffersList { get; set; }

        public List<Work> ClientPendentList { get; set; }

        public ClientActivity(AppDbContext context) {

            dbContext = context;
        }

        public IActionResult OnGet() {

            return Unauthorized();
        }

        // método que é chamado quando se carrega no histórico de trabalhos
        public ActionResult OnGetHistoryList() {

            // vai buscar à BD os trabalhos com estado completo, ordenados pela data
            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "C" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientHistoryList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

                // adiciona os trabalhos à lista que será lida pela view
                ClientHistoryList.Add(new Work() {

                    Id = item.work.Id,
                    Professional = item.pro,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Address = item.work.Address,
                    Cost = item.work.Cost,
                    Payment = displayPayment,
                    Rating = item.work.Rating
                });
            }

            return Page();
        }

        // método que é chamado quando se carrega nos trabalhos pendentes
        public ActionResult OnGetPendentList() {

            // vai buscar à BD os trabalhos com estado pendente, ordenados pela data
            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "P" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientPendentList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

                // adiciona os trabalhos à lista que será lida pela view
                ClientPendentList.Add(new Work() {

                    Id = item.work.Id,
                    Professional = item.pro,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Address = item.work.Address,
                    Cost = item.work.Cost,
                    Payment = displayPayment
                });
            }

            return Page();
        }

        // método que é chamado quando se carrega nos trabalhos oferecidos
        public ActionResult OnGetOffersList() {

            // vai buscar à BD os trabalhos com estado oferta, ordenados pela data
            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "O" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientOffersList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

                // adiciona os trabalhos à lista que será lida pela view
                ClientOffersList.Add(new Work() {

                    Id = item.work.Id,
                    Professional = item.pro,
                    Date = item.work.Date,
                    Cost = item.work.Cost,
                    Payment = displayPayment,
                    Rating = item.work.Rating
                });
            }

            return Page();
        }

        // método chamado pelo botão cancelar oferta. Traz o id do trabalho em questão...
        public IActionResult OnPostCancelOffer(int id) {

            // vai buscar o trabalho à BD
            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            // remove o trabalho
            dbContext.Works.Remove(work);

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithSuccess("Oferta", "cancelada com sucesso.", "2000");

        }

        // método chamado pelo botão avaliar trabalho. Traz o id do trabalho em questão...
        public IActionResult OnPostRateOffer(int id) {

            // vai buscar o trabalho à BD, assim como o profissional do trabalho
            var query = (from work in dbContext.Works
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         where work.Id == id
                         select new { work, pro }).FirstOrDefault();

            // define a classificação do trabalho
            query.work.Rating = Rating;

            // calcula o novo rating do profissional... 80% é o rating antigo + 20% do rating do trabalho recente
            query.pro.Rating = (float) (query.pro.Rating * 0.8) + (float) (Rating * 0.2);

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn").WithSuccess("Trabalho", "avaliado com sucesso.", "2000");
        }


    }
}