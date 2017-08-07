using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace VehicleManagementSystem.Controllers
{
    public class ReportsController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        //Vehicles
        [HttpGet]
        public ActionResult GetVehicles()
        {
            return View(transportationContext.Vehicles.ToList());
        }
        //Users
        [HttpGet]
        public ActionResult GetUsers()
        {
            return View(transportationContext.Users.ToList());
        }
        //Gas Cards
        [HttpGet]
        public ActionResult GetGasCards()
        {
            return View(transportationContext.GasCards.ToList());
        }
        //Currently Signed Out
        [HttpGet]
        public ActionResult GetSignedOut()
        {
            return View(transportationContext.Transportation_SignedOutView.ToList());
        }
        //Sign Out History
        [HttpGet]
        public ActionResult GetSignOutHistory(Transportation_ViewAll log)
        {
            if (log.CheckOutTime != null)
            {
                if (log.CheckInTime >= DateTime.Today + new TimeSpan(23, 59, 59))
                {
                    return View(transportationContext.Transportation_ViewAll.Where(m => m.CheckOutTime >= log.CheckOutTime && m.CheckInTime <= log.CheckInTime || m.CheckInTime == null).ToList());
                }
                return View(transportationContext.Transportation_ViewAll.Where(m => m.CheckOutTime >= log.CheckOutTime && m.CheckInTime <= log.CheckInTime).ToList());
            }
            return View(transportationContext.Transportation_ViewAll.ToList());
        }
        //Sign Out History By Date
        [HttpGet]
        public ActionResult GetSignOutHistoryByDate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSignOutHistoryByDate(Transportation_ViewAll log)
        {
            log.CheckInTime += new TimeSpan(23, 59, 59);
            return RedirectToAction("GetSignOutHistory", log);
        }
    }
}