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

namespace GuguDadah.Pages {

    public class Schedule : PageModel {

        [BindProperty]
        [Display(Name = "Hora Início")]
        public DateTime StartTime { get; set; }

        [BindProperty]
        [Display(Name = "Hora Fim")]
        public DateTime EndTime { get; set; }

        [BindProperty]
        [Display(Name = "Localidade")]
        public string Locality { get; set; }

        [BindProperty]
        public string Extra { get; set; }

        [BindProperty]
        public Work Work { get; set; }

        private readonly AppDbContext dbContext;

        public Schedule(AppDbContext context) {

            dbContext = context;
        }

        public ActionResult OnPostReturningTempWork() {

            Work.Cost = 23;
            Work.Client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(User.Identity.Name));
            Work.Payment = "N";
            Work.Rating = 0;
            Work.Status = "O";
            Work.Type = "N";

            TempData["tempWork"] = JsonConvert.SerializeObject(Work);

            return RedirectToPage("/ChooseBabysitter");

        }
    }
}