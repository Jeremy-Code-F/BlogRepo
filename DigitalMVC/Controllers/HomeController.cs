using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DigitalMVC.Models;

namespace DigitalMVC.Controllers
{
    public class HomeController : Controller
    {
        const string SessionKeyName = "_Name";
        const string SessionKeyFY = "_FY";
        const string SessionKeyDate = "_Date";

        public IActionResult Index()
        {
            HttpContext.Session.SetString(SessionKeyName, "Nam Wam");
            HttpContext.Session.SetInt32(SessionKeyFY, 2017);
            // Requires you add the Set extension method mentioned in the SessionExtensions static class.
           // HttpContext.Session.Set<DateTime>(SessionKeyDate, DateTime.Now);
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Name = HttpContext.Session.GetString(SessionKeyName);
            ViewBag.FY = HttpContext.Session.GetInt32(SessionKeyFY);
           // ViewBag.Date = HttpContext.Session.Get<DateTime>(SessionKeyDate);

            ViewData["Message"] = "Session State In Asp.Net Core 1.1";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Login()
        {
            ViewData["Message"] = "Your login page.";

            return View();
        }

        public IActionResult CreateAccount()
        {
            ViewData["Message"] = "Create Account Page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
