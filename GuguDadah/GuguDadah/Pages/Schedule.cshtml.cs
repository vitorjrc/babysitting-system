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
        [Display(Name = "Data")]
        public DateTime date { get; set; }

        [BindProperty]
        [Display(Name = "Hora Início")]
        public DateTime startTime { get; set; }

        [BindProperty]
        [Display(Name = "Hora Fim")]
        public DateTime endTime { get; set; }

        [BindProperty]
        [Display(Name = "Rua")]
        public string address { get; set; }

        [BindProperty]
        [Display(Name = "Localidade")]
        public string locality { get; set; }

        [BindProperty]
        public string extra { get; set; }

        public void OnGet() {

        }

        public ActionResult OnPostReturningTempWork() {

            Work work = new Work();

            work.cost = 23;

            TempData["tempWork"] = JsonConvert.SerializeObject(work);

            return RedirectToPage("/ChooseBabysitter");

        }
    }
}