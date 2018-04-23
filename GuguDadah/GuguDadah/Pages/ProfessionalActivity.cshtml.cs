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

        public ActionResult OnGetHistoryList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "C" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalHistoryList = new List<Work>();

            foreach (var item in query) {

                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";

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

        public ActionResult OnGetPendentList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "P" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalPendentList = new List<Work>();

            foreach (var item in query) {

                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";

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

        public ActionResult OnGetOffersList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "O" && work.Professional.UserName.Equals(User.Identity.Name)
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         select new { work, cli }).ToList();

            ProfessionalOffersList = new List<Work>();

            foreach (var item in query) {

                string displayType = null;

                if (item.work.Type.Equals("E")) displayType = "Exterior";
                if (item.work.Type.Equals("S")) displayType = "Estudo";
                if (item.work.Type.Equals("N")) displayType = "Normal";


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

        public IActionResult OnPostAcceptOffer(int id) {

            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            query.work.Status = "P";

            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Oferta aceite";
            string body = "A sua proposta foi aceite. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Proposta", "aceite com sucesso.", "2000");

        }

        public IActionResult OnPostRejectOffer(int id) {

            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Oferta rejeitada";
            string body = "A sua proposta foi rejeitada. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            dbContext.Works.Remove(query.work);
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Proposta", "rejeitada com sucesso.", "2000");

        }

        public IActionResult OnPostMarkAsDone(int id) {

            var query = (from work in dbContext.Works
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         where work.Id == id
                         select new { work, cli }).FirstOrDefault();

            Email email = new Email();
            string destination = query.cli.Email;
            string subject = "GuguDadah - Avalie o trabalho";
            string body = "O trabalho foi concluído... Agradecíamos que o avaliasse. Por favor, consulte o site.";

            email.SendEmail(destination, subject, body);

            query.work.Status = "C";

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn").WithSuccess("Trabalho", "marcado como realizado com sucesso.", "2000");

        }


    }
}
