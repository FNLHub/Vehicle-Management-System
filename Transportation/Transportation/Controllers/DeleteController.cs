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
        public ActionResult DeleteUser(int? id, Transportation_View transport)
        {
            try
            {
                Transportation_View trans = new Transportation_View();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(Response.StatusCode = 400);
                    }
                    trans = transportationContext.Transportation_View.Find(id);
                    if (trans == null)
                    {
                        return HttpNotFound();
                    }
                    transportationContext.Transportation_View.Remove(trans);
                    transportationContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(trans);
            }
            catch
            {
                return View();
            }
        }
    }
}