using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages {

    public class Babysitters : PageModel {

        private readonly AppDbContext dbContext;

        public Babysitters(AppDbContext context) {

            dbContext = context;
        }

        public List<Professional> list = new List<Professional>();

        public int ShowBabysitters { get; set; }

        public int ChooseBabysitters { get; set; }

        public IActionResult OnGet() {

            return Unauthorized();
        }

        // método que é chamado quando o cliente quer ver os babysitters do sistema
        [Authorize(Roles = "Client")]
        public ActionResult OnGetShowBS() {

            // define que devem ser apenas mostrados os babysitters sem botão pra escolher
            ShowBabysitters = 1;

            // vai buscar todos os profissionais à BD
            var query = (from p in dbContext.Professionals
                         orderby p.Rating descending
                         select p).ToList();

            // itera pela query e adiciona à lista que será lida pela view
            foreach (var item in query) {

                list.Add(new Professional() {
                    UserName = item.UserName,
                    Name = item.Name,
                    Contact = item.Contact,
                    Email = item.Email,
                    Avatar = item.Avatar,
                    Shift = item.Shift,
                    Rating = item.Rating
                });
            }

            return Page();
        }

        // método que é chamado quando o cliente quer escolher um babysitter
        [Authorize(Roles = "Client")]
        public ActionResult OnGetChooseBS() {

            // define que devem ser mostrados os babysitters do turno e um botão para escolher
            ChooseBabysitters = 1;

            // recebe o custo calculado na página anterior (página do agendamento)
            Object cost = TempData["cost"];

            // manda o custo para a página seguinte (página do pagamento)
            TempData["cost2"] = cost;

            // recebe a hora de início do trabalho introduzido na página anterior;
            TimeSpan TimeScheduled = JsonConvert.DeserializeObject<TimeSpan>(TempData["time"].ToString());

            // turno da noite
            TimeSpan NShiftStart = new TimeSpan(0, 0, 0);
            TimeSpan NShiftEnd = new TimeSpan(7, 59, 59);

            // turno da manhã
            TimeSpan MShiftStart = new TimeSpan(8, 0, 0);
            TimeSpan MShiftEnd = new TimeSpan(15, 59, 59);

            // turno da tarde
            TimeSpan TShiftStart = new TimeSpan(16, 0, 0);
            TimeSpan TShiftEnd = new TimeSpan(23, 59, 59);

            string ShiftFilter = null;

            // vê se a hora de início se enquadra no turno da manhã
            if (TimeScheduled >= MShiftStart && TimeScheduled <= MShiftEnd) {
                ShiftFilter = "M";
            }

            // vê se a hora de início se enquadra no turno da tarde
            if (TimeScheduled >= TShiftStart && TimeScheduled <= TShiftEnd) {
                ShiftFilter = "T";
            }

            // vê se a hora de início se enquadra no turno da noite
            if (TimeScheduled >= NShiftStart && TimeScheduled <= NShiftEnd) {
                ShiftFilter = "N";
            }

            // vai buscar os profissionais à BD que têm o turno definido
            var query = (from p in dbContext.Professionals
                         orderby p.Rating descending
                         where p.Shift == ShiftFilter
                         select p).ToList();

            // itera pela query e adiciona os profissionais à lista que será mostrada na view
            foreach (var item in query) {

                list.Add(new Professional() {
                    UserName = item.UserName,
                    Name = item.Name,
                    Avatar = item.Avatar,
                    Rating = item.Rating,
                    RegistrationDate = item.RegistrationDate,
                    Presentation = item.Presentation
                });
            }

            return Page();

        }
    }
}