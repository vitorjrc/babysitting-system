using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

using GuguDadah.Data;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace GuguDadah.Pages {

    public class Schedule : PageModel {

        [Required]
        [BindProperty]
        [Display(Name = "Data Início")]
        public string StartDate { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Data Fim")]
        public string EndDate { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Hora Início")]
        public string StartTime { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Hora Fim")]
        public string EndTime { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [BindProperty]
        public Work Work { get; set; }

        private readonly AppDbContext dbContext;

        public Schedule(AppDbContext context) {

            dbContext = context;
        }

        public ActionResult OnPostReturningTempWork() {

            TryUpdateModelAsync(this);

            ModelState.Remove("Work.Cost");
            ModelState.Remove("Work.Date");
            ModelState.Remove("Work.Client");
            ModelState.Remove("Work.Payment");
            ModelState.Remove("Work.Status");
            ModelState.Remove("Work.Type");
            ModelState.Remove("Work.Duration");
            ModelState.Remove("Work.Professional");

            if (!ModelState.IsValid) return Page();

            Work.Payment = "N";

            // offered
            Work.Status = "O";

            var parsedStartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", null);
            var parsedStartTime = DateTime.ParseExact(StartTime, "HH:mm", null);

            DateTime FinalStartTime = new DateTime(parsedStartDate.Year, parsedStartDate.Month, parsedStartDate.Day, parsedStartTime.Hour, parsedStartTime.Minute, 0);

            var parsedEndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", null);
            var parsedEndTime = DateTime.ParseExact(EndTime, "HH:mm", null);

            DateTime FinalEndTime = new DateTime(parsedEndDate.Year, parsedEndDate.Month, parsedEndDate.Day, parsedEndTime.Hour, parsedEndTime.Minute, 0);

            var diff = FinalEndTime.Subtract(FinalStartTime).TotalHours;

            Work.Duration = (int) Math.Ceiling(diff);

            Work.Cost = FinalPrice(Type, (int) Work.Duration);

            Work.Date = FinalStartTime;

            if (Type.Equals("exterior")) {
                Work.Type = "E";
            }

            if (Type.Equals("study")) {
                Work.Type = "S";
            }

            if (Type.Equals("normal")) {
                Work.Type = "N";
            }

            TempData["tempWork"] = JsonConvert.SerializeObject(Work);

            return RedirectToPage("/ChooseBabysitter", "ChooseBS");

        }


        private decimal FinalPrice(string typeOfWork, int duration) {

            // preço à hora
            decimal hourPrice = 10;

            // multiplicador para o caso de ser ou não cliente golden
            decimal multiplier = 1;

            // obtém o username do utilizador logado
            var LoggedUser = User.Identity.Name;

            // vai à BD buscar o cliente logado
            var client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(LoggedUser));

            if (client.Status.Equals("G")) multiplier = 0.8m;
            

            if (typeOfWork.Equals("exterior")) hourPrice += 5;

            if (typeOfWork.Equals("study")) hourPrice += 10;

            decimal price = (hourPrice * duration) * multiplier;


            return price;
        }

    }
}