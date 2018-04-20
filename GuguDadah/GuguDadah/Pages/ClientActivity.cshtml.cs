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

        [BindProperty]
        public float Rating { get; set; }

        public List<Work> ClientHistoryList { get; set; }

        public List<Work> ClientOffersList { get; set; }

        public List<Work> ClientPendentList { get; set; }

        public ClientActivity(AppDbContext context) {

            dbContext = context;
        }

        public ActionResult OnGetHistoryList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "C" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientHistoryList = new List<Work>();

            foreach (var item in query) {

                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

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

        public ActionResult OnGetPendentList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "P" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientPendentList = new List<Work>();

            foreach (var item in query) {

                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

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

        public ActionResult OnGetOffersList() {

            var query = (from work in dbContext.Works
                         orderby work.Date
                         where work.Status == "O" && work.Client.UserName.Equals(User.Identity.Name)
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         select new { work, pro }).ToList();

            ClientOffersList = new List<Work>();

            foreach (var item in query) {

                string displayPayment = null;

                if (item.work.Payment.Equals("S")) displayPayment = "Sim";
                if (item.work.Payment.Equals("N")) displayPayment = "Não";

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

        // TODO
        public ActionResult OnPostCancelOffer(int id) {

            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            dbContext.Works.Remove(work);
            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn");

        }

        public ActionResult OnPostRateOffer(int id) {

            var query = (from work in dbContext.Works
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         where work.Id == id
                         select new { work, pro }).FirstOrDefault();

            query.work.Rating = Rating;

            query.pro.Rating = (float) (query.pro.Rating * 0.8) + (float) (Rating * 0.2);

            dbContext.SaveChanges();

            return RedirectToPage("/UserArea", "ClientLoggedIn");
        }


    }
}