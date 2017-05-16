using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class ReportsController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        // GET: Reports
        public ActionResult Reports()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return View(transportationContext.Users.ToList());
        }

        [HttpGet]
        public ActionResult GetVehicles()
        {
            return View(transportationContext.Vehicles.ToList());
        }

        [HttpGet]
        public ActionResult GetGasCards()
        {
            return View(transportationContext.GasCards.ToList());
        }

        [HttpGet]
        public ActionResult GetSignedOut()
        {
            return View(transportationContext.Transportation_SignedOutView.ToList());
        }

        //[HttpGet]
        //public ActionResult GetSignOutLog()
        //{
        //    return View(transportationContext.Transportation_ViewAll.ToList());
        //}
        [HttpGet]
        public ActionResult GetSignOutLog(string D_start, string D_end)
        {


            return View(transportationContext.Transportation_ViewAll.ToList());

            //This View Returns first name in descending order of top 10
            //return View(transportationContext.Transportation_ViewAll.OrderByDescending(L => L.FirstName).Take(10).ToList());
        }
    }
}