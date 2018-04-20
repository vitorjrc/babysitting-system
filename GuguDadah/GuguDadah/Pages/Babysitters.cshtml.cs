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

    public class Babysitters : PageModel {

        private readonly AppDbContext dbContext;

        public Babysitters(AppDbContext context) {

            dbContext = context;
        }

        public List<Professional> list = new List<Professional>();

        public int ShowBabysitters { get; set; }

        public int ChooseBabysitters { get; set; }

        public ActionResult OnGetShowBS() {

            ShowBabysitters = 1;

            var query = (from p in dbContext.Professionals
                         orderby p.UserName
                         select p).ToList();

            foreach (var item in query) 
            {
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

        public ActionResult OnGetChooseBS() {

            ChooseBabysitters = 1;

            TimeSpan TimeScheduled = JsonConvert.DeserializeObject<TimeSpan>(TempData["time"].ToString());

            TimeSpan NShiftStart = new TimeSpan(0, 0, 0); 
            TimeSpan NShiftEnd = new TimeSpan(7, 59, 59);

            TimeSpan MShiftStart = new TimeSpan(8, 0, 0); 
            TimeSpan MShiftEnd = new TimeSpan(15, 59, 59);

            TimeSpan TShiftStart = new TimeSpan(16, 0, 0);
            TimeSpan TShiftEnd = new TimeSpan(23, 59, 59);

            string ShiftFilter = null;

            if (TimeScheduled >= MShiftStart && TimeScheduled <= MShiftEnd) {
                ShiftFilter = "M";
            }

            if (TimeScheduled >= TShiftStart && TimeScheduled <= TShiftEnd) {
                ShiftFilter = "T";
            }

            if (TimeScheduled >= NShiftStart && TimeScheduled <= NShiftEnd) {
                ShiftFilter = "N";
            }
            

            var query = (from p in dbContext.Professionals
                         orderby p.UserName
                         where p.Shift == ShiftFilter
                         select p).ToList();

            foreach (var item in query) //retrieve each item and assign to model
            {
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