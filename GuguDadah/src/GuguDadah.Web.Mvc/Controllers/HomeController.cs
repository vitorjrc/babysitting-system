using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GuguDadah.Controllers;

namespace GuguDadah.Web.Controllers
{
    public class HomeController : GuguDadahControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        // chamando a autorização
        [AbpMvcAuthorize]
        public ActionResult Login()
        {
            return View("~/Views/_ViewStart.cshtml");
        }
    }
}
