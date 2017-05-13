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
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
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
                try
                {
                    transportationContext.Departments.Add(department);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.Vehicles.Add(vehicle);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.Campuses.Add(campus);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.VehicleStatuses.Add(vehicleStatus);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.Keys.Add(key);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.GasCards.Add(gasCard);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
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
                try
                {
                    transportationContext.GasCardStatuses.Add(gasCardStatus);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            return View(transportationContext.Users.ToList());
        }

        [HttpGet]
        public ActionResult GetDepartments()
        {
            return View(transportationContext.Departments.ToList());
        }

        [HttpGet]
        public ActionResult GetVehicles()
        {
            return View(transportationContext.Vehicles.ToList());
        }

        [HttpGet]
        public ActionResult getVehicleStatuses()
        {
            return View(transportationContext.VehicleStatuses.ToList());
        }

        [HttpGet]
        public ActionResult getCampus()
        {
            return View(transportationContext.Campuses.ToList());
        }

        [HttpGet]
        public ActionResult getKeys()
        {
            return View(transportationContext.Keys.ToList());
        }

        [HttpGet]
        public ActionResult GetGasCards()
        {
            return View(transportationContext.GasCards.ToList());
        }

        [HttpGet]
        public ActionResult GetGasCardStatuses()
        {
            return View(transportationContext.GasCardStatuses.ToList());
        }
        
    }
}