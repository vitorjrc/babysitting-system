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
    public class ChooseBabysitter : PageModel {

        private readonly AppDbContext dbContext;

        public ChooseBabysitter(AppDbContext context) {

            dbContext = context;
        }

        public List<Professional> lista = new List<Professional>();

        public void OnGet() {

            var query = (from p in dbContext.Professionals
                         orderby p.UserName
                         select p).ToList();

            foreach (var item in query) //retrieve each item and assign to model
            {
                lista.Add(new Professional() {
                    UserName = item.UserName,
                    Contact = item.Contact,
                    Email = item.Email,
                    Avatar = item.Avatar,
                    Shift = item.Shift,
                    Rating = item.Rating
                });
            }
        }

        public async Task<ActionResult> OnPostChoosedProfessionalAsync(string username) {

            Work work;
            work = JsonConvert.DeserializeObject<Work>(TempData["tempWork"].ToString());

            if (work == null) return Page();

            if (!ModelState.IsValid) {
                return Page();
            }

            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(username));
            work.Professional = professional;

            dbContext.Works.Add(work);

            await dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}