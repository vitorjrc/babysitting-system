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

        private ActionResult getHistoryList() {

            var query = (from p in dbContext.Works
                         orderby p.Date
                         where p.Status == "C" && p.Client.UserName.Equals(User.Identity.Name)
                         select p).ToList();

            ProfessionalHistoryList = new List<Work>();

            foreach (var item in query) {
                ProfessionalHistoryList.Add(new Work() {

                    Address = item.Address,
                    Cost = item.Cost,
                    Date = item.Date,
                    Duration = item.Duration,
                    Payment = item.Payment,
                    Professional = item.Professional,
                    Rating = item.Rating,
                    Status = item.Status,
                    Type = item.Type
                });
            }

            return Page();
        }

        private ActionResult getPendentList() {

            var query = (from p in dbContext.Works
                         orderby p.Date
                         where p.Status == "P" && p.Client.UserName.Equals(User.Identity.Name)
                         select p).ToList();

            ProfessionalPendentList = new List<Work>();

            foreach (var item in query) {
                ProfessionalPendentList.Add(new Work() {

                    Address = item.Address,
                    Cost = item.Cost,
                    Date = item.Date,
                    Duration = item.Duration,
                    Payment = item.Payment,
                    Professional = item.Professional,
                    Rating = item.Rating,
                    Status = item.Status,
                    Type = item.Type
                });
            }

            return Page();
        }

        private ActionResult getOffersList() {

            var query = (from p in dbContext.Works
                         orderby p.Date
                         where p.Status == "P" && p.Client.UserName.Equals(User.Identity.Name)
                         select p).ToList();

            ProfessionalOffersList = new List<Work>();

            foreach (var item in query) {
                ProfessionalOffersList.Add(new Work() {

                    Address = item.Address,
                    Cost = item.Cost,
                    Date = item.Date,
                    Duration = item.Duration,
                    Payment = item.Payment,
                    Professional = item.Professional,
                    Rating = item.Rating,
                    Status = item.Status,
                    Type = item.Type
                });
            }

            return Page();
        }


    }
}