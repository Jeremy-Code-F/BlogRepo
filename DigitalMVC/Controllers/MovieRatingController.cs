using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMVC.Controllers
{
    public class MovieRatingController : Controller
    {
    
        public IActionResult TrumpHome()
        {
            bool result = CheckAuthorization.CheckAuth(HttpContext.Session);
            if (result)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/login");
            }
            
        }
    }
}