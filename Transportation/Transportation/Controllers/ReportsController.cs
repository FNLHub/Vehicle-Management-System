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
        public ActionResult GetSignOutLog(Transportation_ViewAll log)
        {   
            if (log.CheckOutTime != null)
            {
                if (log.CheckInTime == DateTime.Today + new TimeSpan(23, 59, 59))
                {
                    return View(transportationContext.Transportation_ViewAll.Where(m => m.CheckOutTime >= log.CheckOutTime && m.CheckInTime <= log.CheckInTime || m.CheckInTime == null).ToList());
                }
                return View(transportationContext.Transportation_ViewAll.Where(m => m.CheckOutTime >= log.CheckOutTime && m.CheckInTime <= log.CheckInTime).ToList());
            }
            return View(transportationContext.Transportation_ViewAll.ToList());
        }

        [HttpGet]
        public ActionResult GetByDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetByDate(Transportation_ViewAll log)
        {
            log.CheckInTime += new TimeSpan(23, 59, 59);
            return RedirectToAction("GetSignOutLog", log);
        }
    }
}