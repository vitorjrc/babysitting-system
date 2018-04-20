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

            var query = (from p in dbContext.Professionals
                         orderby p.UserName
                         select p).ToList();

            foreach (var item in query) //retrieve each item and assign to model
            {
                list.Add(new Professional() {
                    UserName = item.UserName,
                    Name = item.Name,
                    Contact = item.Contact,
                    Email = item.Email,
                    Avatar = item.Avatar,
                    Shift = item.Shift,
                    Rating = item.Rating,
                    RegistrationDate = item.RegistrationDate,
                    Presentation = item.Presentation
                });
            }

            return Page();

        }
    }
}