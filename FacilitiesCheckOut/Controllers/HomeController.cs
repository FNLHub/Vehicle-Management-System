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

        public ActionResult Reports()
        {
            ViewBag.Message = "Your application reports page.";

            return View();
        }

        public ActionResult VehicleInOutChoose()//-vvvvvv-Vehicle Pages-vvvvvv-\\
        {
            ViewBag.Message = "Your application Vehicle Sign In/Out page.";

            return View();
        }

        public ActionResult VehicleSignOut()
        {
            ViewBag.Message = "Your Vehicle Sign Out page.";

            return View();
        }

        public ActionResult VehicleSignIn()
        {
            ViewBag.Message = "Your Vehicle Sign In Page.";

            return View();
        }

        public ActionResult GasCardInOutChoose()//-vvvvvv-Gas Card Pages-vvvvvv-\\
        {
            ViewBag.Message = "Your application Gas Card Sign In/Out page.";

            return View();
        }

        public ActionResult GasCardSignOut()
        {
            ViewBag.Message = "Your Gas Card Sign In Page.";

            return View();
        }

        public ActionResult GasCardSignIn()
        {
            ViewBag.Message = "Your Gas Card Sign In Page.";

            return View();
        }


    }
}