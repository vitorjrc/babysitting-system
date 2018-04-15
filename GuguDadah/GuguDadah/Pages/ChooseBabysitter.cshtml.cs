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

        [BindProperty]
        public Work work { get; set; }

        private readonly AppDbContext dbContext;

        public ChooseBabysitter(AppDbContext context) {

            dbContext = context;
        }

        public List<Professional> lista = new List<Professional>();

        public void OnGet() {

            var query = (from p in dbContext.Professionals
                         orderby p.userName
                         select p).ToList();

            foreach (var item in query) //retrieve each item and assign to model
            {
                lista.Add(new Professional() {
                    userName = item.userName,
                    contact = item.contact,
                    eMail = item.eMail,
                    avatar = item.avatar,
                    shift = item.shift,
                    rating = item.rating
                });
            }
        }

        public async Task<ActionResult> OnPostChoosedProfessionalAsync(string username) {

            work = JsonConvert.DeserializeObject<Work>(TempData["tempWork"].ToString());

            if (work == null) return Page();

            if (!ModelState.IsValid) {
                return Page();
            }

            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.userName.Equals(username));
            work.professional = professional;

            dbContext.Works.Add(work);

            await dbContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}