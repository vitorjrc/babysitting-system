using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GuguDadah.Controllers;

namespace GuguDadah.Web.Controllers
{
    // desativando a autorização inicial
    // [AbpMvcAuthorize]
    public class HomeController : GuguDadahControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
