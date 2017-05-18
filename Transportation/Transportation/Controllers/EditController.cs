using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class EditController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        //=========================================     Edit User     ===================================================
        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            var user = transportationContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var departmentId = transportationContext.Departments.Where(d => d.DepartmentName == user.Department.DepartmentName).Select(i => i.DepartmentId).FirstOrDefault();
                    if (departmentId != 0)
                    {
                        user.DepartmentId = departmentId;
                        user.Department.DepartmentId = departmentId;
                        transportationContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        transportationContext.SaveChanges();
                    }

                    return RedirectToAction("Index", "Admin");
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }

        //=========================================    Edit Gas Card    ===================================================
        [HttpGet]
        public ActionResult EditGasCard(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            var gasCard = transportationContext.GasCards.Find(id);
            if (gasCard == null)
            {
                return HttpNotFound();
            }
            return View(gasCard);
        }
        [HttpPost]
        public ActionResult EditGasCard(GasCard gasCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Select the gas card id which matches GasCardName with the selected gas card
                    var gasCardId = transportationContext.GasCards.Where(G => G.GasCardName == gasCard.GasCardName).Select(i => i.GasCardId).FirstOrDefault();
                   
                    if (gasCardId != 0)
                    {
                        //Possible logic error on the two lines below
                        gasCard.StatusId = gasCardId;
                        gasCard.GasCardStatus.StatusId = gasCardId;
                        transportationContext.Entry(gasCard).State = System.Data.Entity.EntityState.Modified;
                        transportationContext.SaveChanges();
                    }

                    return RedirectToAction("Index", "Admin");
                }
                return View(gasCard);
            }
            catch
            {
                return View();
            }
        }

        //=========================================     Edit Vehicle     ===================================================
        [HttpGet]
        public ActionResult EditVehicle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            var vehicle = transportationContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }
        [HttpPost]
        public ActionResult EditVehicle(Vehicle vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Select the vehicle id where the names match
                    var vehicleId = transportationContext.Vehicles.Where( v => v.VehicleName == vehicle.VehicleName).Select(i => i.VehicleId).FirstOrDefault();
                 
                    if (vehicleId != 0)
                    {

                        //possible logic error on the 2 lines below
                        vehicle.VehicleId = vehicleId;
                        vehicle.VehicleStatus.StatusId = vehicleId;
                    
                        //Saving modified entity
                        transportationContext.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                        transportationContext.SaveChanges();
                    }

                    return RedirectToAction("Index", "Admin");
                }
                return View(vehicle);
            }
            catch
            {
                return View();
            }
        }

    }
}