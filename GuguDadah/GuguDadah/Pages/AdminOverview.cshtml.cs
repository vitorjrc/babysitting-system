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

    public class AdminOverview : PageModel {

        public List<Client> ClientsList { get; set; }

        public List<Professional> ProfessionalsList { get; set; }

        public List<Work> WorksList { get; set; }

        private readonly AppDbContext dbContext;

        public AdminOverview(AppDbContext context) {

            dbContext = context;
        }

    public ActionResult OnGetListOfClients() {

            var query = (from p in dbContext.Clients
                         orderby p.UserName
                         select p).ToList();

            ClientsList = new List<Client>();

            //retrieve each item and assign to model
            foreach (var item in query) {

                string displayStatus = null;
                if (item.Status.Equals("N")) displayStatus = "Normal";
                else displayStatus = "Golden";

                ClientsList.Add(new Client() {
                    UserName = item.UserName,
                    Name = item.Name,
                    Contact = item.Contact,
                    Email = item.Email,
                    Avatar = item.Avatar,
                    Status = displayStatus
                });
            }

            return Page();
        }

        public ActionResult OnGetListOfProfessionals() {
            var query = (from p in dbContext.Professionals
                         orderby p.UserName
                         select p).ToList();

            ProfessionalsList = new List<Professional>();

            //retrieve each item and assign to model
            foreach (var item in query) {
                ProfessionalsList.Add(new Professional() {
                    UserName = item.UserName,
                    Contact = item.Contact,
                    Email = item.Email,
                    Avatar = item.Avatar,
                    Rating = item.Rating,
                    Shift = item.Shift,
                    RegistrationDate = item.RegistrationDate,
                    Name = item.Name
                });
            }

            return Page();
        }

        public ActionResult OnGetListOfWorks() {
            var query = (from work in dbContext.Works
                         join pro in dbContext.Professionals on work.Professional.UserName equals pro.UserName
                         join cli in dbContext.Clients on work.Client.UserName equals cli.UserName
                         orderby work.Date
                         select new { work, pro, cli }).ToList();

            WorksList = new List<Work>();

            //retrieve each item and assign to model
            foreach (var item in query) {
                WorksList.Add(new Work() {
                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Professional = item.pro,
                    Cost = item.work.Cost,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Payment = item.work.Payment,
                    Status = item.work.Status,
                    Rating = item.work.Rating,
                    Type = item.work.Type
                });
            }

            return Page();
        }



    }
}