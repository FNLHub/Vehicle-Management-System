using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacilitiesCheckOut.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            ViewBag.Message = "Your SignOut page.";

            return View();
        }

        public ActionResult SignIn()
        {
            ViewBag.Message = "Your SignIn Page.";

            return View();
        }

        public ActionResult Reports()
        {
            ViewBag.Message = "Your application reports page.";

            return View();
        }
        
        public ActionResult Vehicles()//Vehicles Page
        {
            ViewBag.Message = "Your Vehicles Page.";

            return View();
        }

        public ActionResult GasCards()//Gas Cards Page
        {
            ViewBag.Message = "Your Gas Cards Page.";

            return View();
        }
    }
}