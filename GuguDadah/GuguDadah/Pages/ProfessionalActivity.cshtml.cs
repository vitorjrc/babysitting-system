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
                ProfessionalHistoryList.Add(new Work() {

                    Id = item.work.Id,
                    Client = item.cli,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Rating = item.work.Rating,
                    Type = item.work.Type
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
                ProfessionalPendentList.Add(new Work() {

                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Cost = item.work.Cost,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Observations = item.work.Observations,
                    Type = item.work.Type
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
                ProfessionalOffersList.Add(new Work() {

                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Type = item.work.Type
                });
            }

            return Page();
        }


    }
}
