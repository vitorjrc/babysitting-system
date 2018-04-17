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

        [BindProperty]
        public List<Client> ClientsList { get; set; }

        [BindProperty]
        public List<Professional> ProfessionalsList { get; set; }

        [BindProperty]
        public List<Work> WorksList { get; set; }

        private readonly AppDbContext dbContext;

        public AdminOverview(AppDbContext context) {

            dbContext = context;
        }

    public ActionResult OnGetListOfClients() {

            var query = (from p in dbContext.Clients
                         orderby p.userName
                         select p).ToList();

            ClientsList = new List<Client>();

            //retrieve each item and assign to model
            foreach (var item in query) {
                ClientsList.Add(new Client() {
                    userName = item.userName,
                    contact = item.contact,
                    eMail = item.eMail,
                    avatar = item.avatar,
                });
            }

            return Page();
        }

        public ActionResult OnGetListOfProfessionals() {
            var query = (from p in dbContext.Professionals
                         orderby p.userName
                         select p).ToList();

            ProfessionalsList = new List<Professional>();

            //retrieve each item and assign to model
            foreach (var item in query) {
                ProfessionalsList.Add(new Professional() {
                    userName = item.userName,
                    contact = item.contact,
                    eMail = item.eMail,
                    avatar = item.avatar,
                });
            }

            return Page();
        }

        public ActionResult OnGetListOfWorks() {
            var query = (from p in dbContext.Works
                         orderby p.date
                         select p).ToList();

            WorksList = new List<Work>();

            //retrieve each item and assign to model
            foreach (var item in query) {
                WorksList.Add(new Work() {
                    address = item.address,
                    client = item.client,
                    professional = item.professional,
                    cost = item.cost,
                    date = item.date,
                    duration = item.duration,
                    payment = item.payment,
                    status = item.status,
                    rating = item.rating,
                    type = item.type
                });
            }

            return Page();
        }



    }
}