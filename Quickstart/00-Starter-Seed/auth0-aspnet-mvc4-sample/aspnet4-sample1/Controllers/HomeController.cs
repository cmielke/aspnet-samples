using System.Configuration;
using System.Globalization;
using System.IdentityModel.Services;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace aspnet4_sample1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string name = ClaimsPrincipal.Current.FindFirst("name")?.Value;
            ViewBag.Name = name;
            ViewBag.ShadowSyncUserInfo = "";

            if (User.Identity.IsAuthenticated)
            {
                string shadowSyncSessionKey = this.Request.Cookies["ShadowSyncKey"]?.Value;
                string shadowSyncUrl = this.Request.Cookies["ShadowSyncUrl"]?.Value;
                if (!string.IsNullOrWhiteSpace(shadowSyncSessionKey) && !string.IsNullOrWhiteSpace(shadowSyncUrl))
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", $"EmdatSession {shadowSyncSessionKey}");
                        var response = await httpClient.GetAsync(shadowSyncUrl);
                        XDocument xdoc = XDocument.Load(await response.Content.ReadAsStreamAsync());
                        ViewBag.ShadowSyncUserInfo = xdoc.ToString(SaveOptions.OmitDuplicateNamespaces);
                    }
                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}