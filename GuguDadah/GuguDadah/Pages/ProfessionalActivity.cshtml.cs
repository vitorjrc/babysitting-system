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

namespace GuguDadah.Pages {

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

                var ratingRounded = Math.Round((float)item.work.Rating, 1);

                ProfessionalHistoryList.Add(new Work() {

                    Id = item.work.Id,
                    Client = item.cli,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Rating = (float) ratingRounded,
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

        public ActionResult OnPostAcceptOffer(int id) {


            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            work.Status = "P";

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn");

        }

        public ActionResult OnPostRejectOffer(int id) {

            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            dbContext.Works.Remove(work);
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn");

        }

        public ActionResult OnPostMarkAsDone(int id) {


            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            work.Status = "C";

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ProfessionalLoggedIn");

        }


    }
}
