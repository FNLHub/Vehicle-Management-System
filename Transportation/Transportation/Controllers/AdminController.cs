using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class AdminController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Reports()
        {
            return View();
        }
        // Create Statements for Users Table
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    transportationContext.Users.Add(user);
                    transportationContext.SaveChanges();
                    return RedirectToAction("index");
                }
                catch
                {
                    return RedirectToAction("index");
                }
            }
            return View();
        }

        // Create Statements for Departments Table
        [HttpGet]
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                transportationContext.Departments.Add(department);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for Vehicle Table
        [HttpGet]
        public ActionResult CreateVehicle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                transportationContext.Vehicles.Add(vehicle);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for Campus Table
        [HttpGet]
        public ActionResult CreateCampus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCampus(Campus campus)
        {
            if (ModelState.IsValid)
            {
                transportationContext.Campuses.Add(campus);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for VehicleStatus Table
        [HttpGet]
        public ActionResult CreateVehicleStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVehicleStatus(VehicleStatus vehicleStatus)
        {
            if (ModelState.IsValid)
            {
                transportationContext.VehicleStatuses.Add(vehicleStatus);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for Keys Table
        [HttpGet]
        public ActionResult CreateKey()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateKey(Key key)
        {
            if (ModelState.IsValid)
            {
                transportationContext.Keys.Add(key);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for GasCards Table
        [HttpGet]
        public ActionResult CreateGasCard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGasCard(GasCard gasCard)
        {
            if (ModelState.IsValid)
            {
                transportationContext.GasCards.Add(gasCard);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Create Statements for GasCardStatuses Table
        [HttpGet]
        public ActionResult CreateGasCardStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGasCardStatus(GasCardStatus gasCardStatus)
        {
            if (ModelState.IsValid)
            {
                transportationContext.GasCardStatuses.Add(gasCardStatus);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}