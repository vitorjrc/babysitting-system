using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GuguDadah.Pages {
    public class ClientActivity : PageModel {

        private readonly AppDbContext dbContext;

        public ClientActivity(AppDbContext context) {

            dbContext = context;
        }

        public List<Work> ClientHistoryList { get; set; }

        public List<Work> ClientOffersList { get; set; }

        public List<Work> ClientPendentList { get; set; }

        private ActionResult getHistoryList() {

            var query = (from p in dbContext.Works
                         orderby p.Date
                         where p.Status == "C" && p.Client.UserName.Equals(User.Identity.Name)
                         select p).ToList();

            ClientHistoryList = new List<Work>();

            foreach (var item in query) {
                ClientHistoryList.Add(new Work() {

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

            ClientPendentList = new List<Work>();

            foreach (var item in query) {
                ClientPendentList.Add(new Work() {

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

            ClientOffersList = new List<Work>();

            foreach (var item in query) {
                ClientOffersList.Add(new Work() {

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