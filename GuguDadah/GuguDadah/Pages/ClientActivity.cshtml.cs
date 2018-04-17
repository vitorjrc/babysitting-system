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
    public class ClientActivity : PageModel {

        private readonly AppDbContext dbContext;

        public ClientActivity(AppDbContext context) {

            dbContext = context;
        }

        public List<Professional> ClientHistoryList = new List<Professional>();

        public void OnGet() {

        }
    }
}