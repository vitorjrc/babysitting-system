using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GuguDadah.Data;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Authentication;

namespace GuguDadah.Pages {

    public class List : PageModel {

        private readonly AppDbContext dbContext;

        public List(AppDbContext context) {

            dbContext = context;
        }

        public List<Client> lista = new List<Client>();

        public void OnGet() {

            var query = (from p in dbContext.Clients
                         orderby p.userName
                         select p).ToList();

            foreach (var item in query) //retrieve each item and assign to model
            {
                lista.Add(new Client() {
                    userName = item.userName,
                    contact = item.contact,
                    eMail = item.eMail,
                    avatar = item.avatar,
                });
            }
        }


    }
}