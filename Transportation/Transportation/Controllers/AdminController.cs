using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    //[Authorize(Roles ="Facilities, Admin")]
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
        public ActionResult CreateUser(Admin_Create_User CUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentId = transportationContext.Departments.Where(d => d.DepartmentName == CUser.DepartmentName).Select(i => i.DepartmentId).FirstOrDefault();
                    var statusId = transportationContext.UserStatuses.Where(s => s.StatusName == CUser.StatusName).Select(i => i.StatusId).FirstOrDefault();
                    if (departmentId != 0 && statusId != 0)
                    {
                        User user = new User();
                        user.BannerId = CUser.BannerId;
                        user.FirstName = CUser.FirstName;
                        user.LastName = CUser.LastName;
                        user.DepartmentId = departmentId;
                        user.StatusId = statusId;
                        transportationContext.Users.Add(user);
                        transportationContext.SaveChanges();

                        return RedirectToAction("index");
                    }

                    return new HttpStatusCodeResult(Response.StatusCode = 400);

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
        public ActionResult CreateVehicle(Admin_Create_Vehicle CVehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var statusId = transportationContext.VehicleStatuses.Where(Vs => Vs.StatusName == CVehicle.StatusName).Select(i => i.StatusId).FirstOrDefault();
                    var campusId = transportationContext.Campuses.Where(C => C.CampusCode == CVehicle.CampusCode).Select(i => i.CampusId).FirstOrDefault();
                    if (statusId != 0 && campusId != 0)
                    {
                        //create vehicle table object
                        Vehicle vehicle = new Vehicle();
                        //set input from form
                        vehicle.VehiclePlate = CVehicle.VehiclePlate;
                        vehicle.VehicleName = CVehicle.VehicleName;
                        //set validated input from form
                        vehicle.StatusId = statusId;
                        vehicle.CampusId = campusId;
                        //add this information into the Admin Create Vehicle
                        transportationContext.Vehicles.Add(vehicle);
                        //Save your changes
                        transportationContext.SaveChanges();
                        //retur page
                        return RedirectToAction("Index");
                    }
                    //error if bad input
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
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
        public ActionResult CreateGasCard(Admin_Create_GasCard CGasCard)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var statusId = transportationContext.GasCardStatuses.Where(Gs => Gs.StatusName == CGasCard.StatusName).Select(i => i.StatusId).FirstOrDefault();
                    if (statusId != 0)
                    {
                        //instantiate object
                        GasCard gasCard = new GasCard();
                        //set
                        gasCard.GasCardName = CGasCard.GasCardName;
                        gasCard.GasCardNum = CGasCard.GasCardNum;
                        gasCard.GasCardPin = CGasCard.GasCardPin;
                        gasCard.StatusId = statusId;
                        //add
                        transportationContext.GasCards.Add(gasCard);
                        //save
                        transportationContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
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
        public ActionResult GetVehicleStatuses()
        {
            return View(transportationContext.VehicleStatuses.ToList());
        }

        [HttpGet]
        public ActionResult GetCampus()
        {
            return View(transportationContext.Campuses.ToList());
        }

        [HttpGet]
        public ActionResult GetKeys()
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