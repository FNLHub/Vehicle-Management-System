using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class DeleteController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            User user = transportationContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult DeleteUser(int? id, User Trash)
        {
            try
            {
                User user = new User();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(Response.StatusCode = 400);
                    }
                    user = transportationContext.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    transportationContext.Users.Remove(user);
                    transportationContext.SaveChanges();
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }
    }
}