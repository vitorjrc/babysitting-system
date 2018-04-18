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
            ModelState.Remove("Work.Rating");
            ModelState.Remove("Work.Status");
            ModelState.Remove("Work.Type");
            ModelState.Remove("Work.Duration");
            ModelState.Remove("Work.Professional");

            if (!ModelState.IsValid) return Page();

            Work.Cost = 20;
            Work.Payment = "N";
            Work.Rating = 0;
            Work.Status = "P";

            var parsedStartDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", null);
            var parsedStartTime = DateTime.ParseExact(StartTime, "hh:mm", null);

            DateTime FinalStartTime = new DateTime(parsedStartDate.Year, parsedStartDate.Month, parsedStartDate.Day, parsedStartTime.Hour, parsedStartTime.Minute, 0);

            var parsedEndDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", null);
            var parsedEndTime = DateTime.ParseExact(EndTime, "hh:mm", null);

            DateTime FinalEndTime = new DateTime(parsedEndDate.Year, parsedEndDate.Month, parsedEndDate.Day, parsedEndTime.Hour, parsedEndTime.Minute, 0);

            var diff = (int) FinalEndTime.Subtract(FinalStartTime).TotalHours;
            Work.Duration = diff;

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

            return RedirectToPage("/ChooseBabysitter");

        }
    }
}