using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class TransportationController : Controller
    {
        TransportationEntities transportationContext = new TransportationEntities();
        
        
        [HttpGet] // GET: Transportation
        public ActionResult Index()
        {
            return View(transportationContext.Transportation_View.ToList());
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignOut(SignOut trans)
        {
            if (ModelState.IsValid)
            {
                trans.CheckOutTime = DateTime.Now;
                trans.ActivityTime = DateTime.Now;
                transportationContext.SignOuts.Add(trans);
                transportationContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignIn(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            SignOut trans = transportationContext.SignOuts.Find(id);
            if (trans == null)
            {
                return HttpNotFound();
            }
            return View(trans);
        }
        [HttpPost]
        public ActionResult SignIn(SignOut trans)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    trans.CheckOutTime = trans.CheckOutTime;
                    trans.CheckInTime = DateTime.Now;
                    trans.ActivityTime = DateTime.Now;
                    transportationContext.Entry(trans).State = System.Data.Entity.EntityState.Modified;
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

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            Transportation_View trans = transportationContext.Transportation_View.Find(id);
            if (trans == null)
            {
                return HttpNotFound();
            }
            return View(trans);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(Response.StatusCode = 400);
            }
            Transportation_View trans = transportationContext.Transportation_View.Find(id);
            if (trans == null)
            {
                return HttpNotFound();
            }
            return View(trans);
        }
        [HttpPost]
        public ActionResult Delete(int? id, Transportation_View transport)
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