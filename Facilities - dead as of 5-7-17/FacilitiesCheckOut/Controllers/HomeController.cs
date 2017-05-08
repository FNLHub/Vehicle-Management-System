using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

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
            ViewBag.Title = "Checkout or Return Vehicle";
            ViewBag.Choice = "Vehicle";

            return View("CheckoutReturnChoose");
        }

        public ActionResult GasCardInOutChoose()//-vvvvvv-Gas Card Pages-vvvvvv-\\
        {
            ViewBag.Message = "Your application Gas Card Sign In/Out page.";
            ViewBag.Title = "Checkout or Return Gas Card";
            ViewBag.Choice = "Gas Card";

            return View("CheckoutReturnChoose");
        }

        public ActionResult Checkout()
        {
            switch ((string)Session["type"])
            {
                case "Vehicle":
                    ViewBag.Message = "Checkout a Vehicle";
                    break;
                case "Gas Card":
                    ViewBag.Message = "Checkout a Gas Card";
                    break;
            }

            return View("Reports");
        }

        public ActionResult Return()
        {
            switch ((string)Session["type"])
            {
                case "Vehicle":
                    ViewBag.Message = "Return a Vehicle";
                    break;
                case "Gas Card":
                    ViewBag.Message = "Return a Gas Card";
                    break;
            }

            return View("Reports");
        }
    }
}