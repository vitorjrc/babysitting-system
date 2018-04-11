using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;


namespace GuguDadah.Pages
{
    public class ScheduleModel : PageModel
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime date {get;set;}

        [Display(Name = "Hora Início")]
        public DateTime startTime { get; set; }

        [Display(Name = "Hora Fim")]
        public DateTime endTime { get; set; }

        [Display(Name = "Rua")]
        public string address { get; set; }

        [Display(Name = "Localidade")]
        public string locality { get; set; }

        public string extra { get; set; }

        public void OnGet()
        {

        }
    }
}