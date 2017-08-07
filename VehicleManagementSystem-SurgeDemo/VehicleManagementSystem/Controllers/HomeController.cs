using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace VehicleManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        public ActionResult Index()
        {
            if (1 == 3) // if user is not authenticated
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                return View();
            }
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