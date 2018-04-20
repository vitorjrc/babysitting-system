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

    [Authorize(Roles = "Admin")]
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

            // vai buscar o conteúdo de toda a query
            foreach (var item in query) {

                string displayStatus = null;
                if (item.Status.Equals("N")) displayStatus = "Normal";
                if (item.Status.Equals("G")) displayStatus = "Golden";

                // coloca numa lista que será lida pela view
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

            // vai buscar o conteúdo de toda a query
            foreach (var item in query) {

                // coloca numa lista que será lida pela view
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

            // vai buscar o conteúdo de toda a query
            foreach (var item in query) {

                string displayStatus = null;
                string displayPayment = null;

                if (item.work.Status.Equals("O")) displayStatus = "Oferta";
                if (item.work.Status.Equals("P")) displayStatus = "Pendente";
                if (item.work.Status.Equals("C")) displayStatus = "Completo";
                if (item.work.Payment.Equals("S")) displayPayment = "Pago";
                if (item.work.Payment.Equals("N")) displayPayment = "Não Pago";

                // coloca numa lista que será lida pela view
                WorksList.Add(new Work() {
                    Id = item.work.Id,
                    Address = item.work.Address,
                    Client = item.cli,
                    Professional = item.pro,
                    Cost = item.work.Cost,
                    Date = item.work.Date,
                    Duration = item.work.Duration,
                    Payment = displayPayment,
                    Status = displayStatus,
                    Rating = item.work.Rating,
                    Type = item.work.Type
                });
            }

            return Page();
        }

        public IActionResult OnPostMarkAsGolden(string username) {


            var client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(username));

            client.Status = "G";

            dbContext.SaveChanges();

            return RedirectToPage("/AdminOverview", "ListOfClients").WithSuccess("Admin", "Cliente marcado como golden com sucesso.", "2000");
        }

        public IActionResult OnPostMarkAsNormal(string username) {


            var client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(username));

            client.Status = "N";

            dbContext.SaveChanges();

            return RedirectToPage("/AdminOverview", "ListOfClients").WithSuccess("Admin", "Cliente marcado como normal com sucesso.", "2000");
        }

        public IActionResult OnPostMarkAsPaid(int id) {


            var work = dbContext.Works.FirstOrDefault(m => m.Id.Equals(id));

            work.Payment = "S";

            dbContext.SaveChanges();

            return RedirectToPage("/AdminOverview", "ListOfWorks").WithSuccess("Admin", "O trabalho foi marcado como pago com sucesso.", "2000");
        }

        public IActionResult OnPostDeleteProfessional(string username) {


            var pro = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(username));

            dbContext.Professionals.Remove(pro);

            dbContext.SaveChanges();

            return RedirectToPage("/AdminOverview", "ListOfWorks").WithSuccess("Admin", "O profissional foi removido com sucesso.", "2000");
        }

    }
}