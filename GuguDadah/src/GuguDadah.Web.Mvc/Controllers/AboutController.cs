using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GuguDadah.Controllers;

namespace GuguDadah.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : GuguDadahControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
