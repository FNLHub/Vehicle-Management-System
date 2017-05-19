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
        public ActionResult EditGasCard(int? id)
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
                    transportationContext.Entry(gasCard).State = System.Data.Entity.EntityState.Modified;
                    transportationContext.SaveChanges();
                    

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
                        transportationContext.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                        transportationContext.SaveChanges();

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