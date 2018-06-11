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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace GuguDadah.Pages {

    [Authorize(Roles = "Client")]
    public class Schedule : PageModel {

        [Required]
        [BindProperty]
        [Display(Name = "Data Início")]
        public string StartDate { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Data Fim")]
        public string EndDate { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Hora Início")]
        public string StartTime { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Hora Fim")]
        public string EndTime { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [BindProperty]
        public Work Work { get; set; }

        private readonly AppDbContext dbContext;

        public Schedule(AppDbContext context) {

            dbContext = context;
        }


        // método que é acedido quando o cliente preencheu os campos da página
        public ActionResult OnPostReturningTempWork() {

            TryUpdateModelAsync(this);

            // define as variáveis como não required
            ModelState.Remove("Work.Cost");
            ModelState.Remove("Work.Date");
            ModelState.Remove("Work.Client");
            ModelState.Remove("Work.Payment");
            ModelState.Remove("Work.Status");
            ModelState.Remove("Work.Type");
            ModelState.Remove("Work.Duration");
            ModelState.Remove("Work.Professional");

            // retorna erros se algo foi mal preenchido
            if (!ModelState.IsValid) return Page();

            // define o estado como oferta
            Work.Status = "O";

            // calcula hora e dia do início do trabalho
            var parsedStartDate = DateTime.ParseExact(StartDate, "dd-MM-yyyy", null);
            var parsedStartTime = DateTime.ParseExact(StartTime, "HH:mm", null);

            DateTime FinalStartTime = new DateTime(parsedStartDate.Year, parsedStartDate.Month, parsedStartDate.Day, parsedStartTime.Hour, parsedStartTime.Minute, 0);

            // calcula hora e dia do fim do trabalho
            var parsedEndDate = DateTime.ParseExact(EndDate, "dd-MM-yyyy", null);
            var parsedEndTime = DateTime.ParseExact(EndTime, "HH:mm", null);

            DateTime FinalEndTime = new DateTime(parsedEndDate.Year, parsedEndDate.Month, parsedEndDate.Day, parsedEndTime.Hour, parsedEndTime.Minute, 0);

            var today = DateTime.Now;

            // retorna erro se as datas são anteriores ao dia e hora atual
            if (FinalEndTime < today || FinalStartTime < today ) {

                ModelState.AddModelError(string.Empty, "Datas inválidas.");

                return Page();
            }

            // calcula diferença entre as horas
            var diff = FinalEndTime.Subtract(FinalStartTime).TotalHours;
            var duration = (int)Math.Ceiling(diff);

            // retorna erro se exceder 8 horas
            if (duration > 8) {

                ModelState.AddModelError(string.Empty, "O intervalo de tempo excede as 8 horas.");

                return Page();
            }

            // popula a instância
            Work.Duration = duration;

            // chama o método final price que calcula o preço, passando-lhe o tipo e a duração do trabalho
            Work.Cost = FinalPrice(Type, (int) Work.Duration);

            Work.Date = FinalStartTime;

            // transforma as strings da view em carateres pra serem guardados na BD
            if (Type.Equals("exterior")) {
                Work.Type = "E";
            }

            if (Type.Equals("study")) {
                Work.Type = "S";
            }

            if (Type.Equals("normal")) {
                Work.Type = "N";
            }

            TimeSpan time = FinalStartTime.TimeOfDay;

            // passa as informações para a página seguinte (Babysitters e pagamento)

            TempData["tempWork"] = JsonConvert.SerializeObject(Work);

            TempData["time"] = JsonConvert.SerializeObject(time);

            TempData["cost"] = Work.Cost;

            return RedirectToPage("/Babysitters", "ChooseBS");

        }


        private decimal FinalPrice(string typeOfWork, int duration) {

            // preço à hora
            decimal hourPrice = 10;

            // multiplicador para o caso de ser ou não cliente golden
            decimal multiplier = 1;

            // obtém o username do utilizador logado
            var LoggedUser = User.Identity.Name;

            // vai à BD buscar o cliente logado
            var client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(LoggedUser));

            // faz desconto de 20% se o cliente for golden
            if (client.Status.Equals("G")) multiplier = 0.8m;
            
            // adiciona 5€ ao preço por hora se o tipo for exterior
            if (typeOfWork.Equals("exterior")) hourPrice += 5;

            // adiciona 10€ ao preço por hora se o tipo for study
            if (typeOfWork.Equals("study")) hourPrice += 10;

            // calcula o preço
            decimal price = (hourPrice * duration) * multiplier;

            return price;
        }

    }
}