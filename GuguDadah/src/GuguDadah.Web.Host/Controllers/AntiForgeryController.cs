using Microsoft.AspNetCore.Antiforgery;
using GuguDadah.Controllers;

namespace GuguDadah.Web.Host.Controllers
{
    public class AntiForgeryController : GuguDadahControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
