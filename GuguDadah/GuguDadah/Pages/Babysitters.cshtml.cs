using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.ComponentModel.DataAnnotations;

namespace GuguDadah.Pages {
    public class Babysitters : PageModel {

        [BindProperty]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [BindProperty]
        [Display(Name = "Email")]
        public string eMail { get; set; }

        [BindProperty]
        [Display(Name = "Contacto Telefónico")]
        public string contact { get; set; }

        [BindProperty]
        public byte[] Avatar { get; set; }

        [BindProperty]
        [Display(Name = "Turno")]
        public string shift { get; set; }

        [BindProperty]
        [Display(Name = "Rating")]
        public double rating { get; set; }

        private readonly AppDbContext dbContext;

        public Babysitters(AppDbContext context) {

            dbContext = context;
            onGet();
        }

        public List<Professional> lista = new List<Professional>();

        public void onGet() {

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
    }
}