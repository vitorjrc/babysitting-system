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
using GuguDadah.Includes;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Professional")]
    public class ProfessionalActivity : PageModel {

        private readonly AppDbContext dbContext;

        public List<Work> ProfessionalHistoryList { get; set; }

        public List<Work> ProfessionalOffersList { get; set; }

        public List<Work> ProfessionalPendentList { get; set; }

        public ProfessionalActivity(AppDbContext context) {

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
                         where work.Status == "C" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalHistoryList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";

                // adiciona os trabalhos à lista que será lida pela view
                ProfessionalHistoryList.Add(new Work() {

                    Id = item.work.Id,
                    Client = item.cli,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Rating = item.work.Rating,
                    Type = displayType
                });
            }

            return Page();
        }

        // método que é chamado quando se carrega nos trabalhos pendentes
        public ActionResult OnGetPendentList() {

            // vai buscar à BD os trabalhos com estado pendente, ordenados pela data
            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "P" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalPendentList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";

                // adiciona os trabalhos à lista que será lida pela view
                ProfessionalPendentList.Add(new Work() {

                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Cost = item.work.Cost,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Observations = item.work.Observations,
                    Type = displayType
                });
            }

            return Page();
        }

        // método que é chamado quando se carrega na oferta de trabalhos
        public ActionResult OnGetOffersList() {

            // vai buscar à BD os trabalhos com estado oferta, ordenados pela data
            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "O" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalOffersList = new List<Work>();

            // itera pela query
            foreach (var item in query) {

                // muda os carateres para strings, para mostrar na view
                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";

                // adiciona os trabalhos à lista que será lida pela view
                ProfessionalOffersList.Add(new Work() {

                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Type = displayType

                });
            }

            return Page();
        }

        // método chamado quando o profissional carrega em aceitar oferta. Traz o id do trabalho em questão...
        public IActionResult OnPostAcceptOffer(int id) {

            // vai buscar o trabalho à BD
            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            // muda o estado do trabalho para pendente
            query.work.Status = "P";

            // envia email a informar o cliente
            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Oferta aceite";
            string body = "A sua proposta foi aceite. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            // guarda as alterações na BD
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Proposta", "aceite com sucesso.", "2000");

        }

        // método chamado quando o profissional carrega em rejeitar oferta. Traz o id do trabalho em questão...
        public IActionResult OnPostRejectOffer(int id) {

            // vai buscar o trabalho à BD
            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            // envia email a informar o cliente
            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Oferta rejeitada";
            string body = "A sua proposta foi rejeitada. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            // remove o trabalho da BD
            dbContext.Works.Remove(query.work);

            // guarda as alterações
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Proposta", "rejeitada com sucesso.", "2000");

        }

        // método chamado quando o profissional carrega em trabalho realizado. Traz o id do trabalho em questão...
        public IActionResult OnPostMarkAsDone(int id) {

            // vai buscar o trabalho à BD
            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            // envia email ao cliente a informar
            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Avalie o trabalho";
            string body = "O trabalho foi concluído... Agradecíamos que o avaliasse. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            // muda o estado do trabalho para completo
            query.work.Status = "C";

            // guarda as alterações na BD
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Trabalho", "marcado como realizado com sucesso.", "2000");

        }


    }
}
