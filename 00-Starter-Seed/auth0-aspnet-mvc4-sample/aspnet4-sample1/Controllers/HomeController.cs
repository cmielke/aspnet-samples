﻿using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace aspnet4_sample1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var principal = ClaimsPrincipal.Current;

            if (principal.Identity.IsAuthenticated)
            {
                ViewBag.Email = principal.FindFirst("email").Value;
                ViewBag.Name = principal.Identity.Name;
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

        public ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            return RedirectToAction("Index");
        }
    }
}