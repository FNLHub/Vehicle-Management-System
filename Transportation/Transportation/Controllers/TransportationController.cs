using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers
{
    public class TransportationController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();
        static int count = 0;
        public ActionResult CreateDriver()
        {
            ViewBag.count = (count++);
            return PartialView("~/Views/Injections/AppendingToTable.cshtml");
        }

        [HttpGet] // GET: Transportation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TransporationRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TransporationRequest(TransporationRequest transRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {

                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(User_Transportation_Log trans)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = transportationContext.Users.Where(u => u.BannerId == trans.BannerId).Select(i => i.UserId).FirstOrDefault();
                    var vehicleId = transportationContext.Vehicles.Where(v => v.VehicleName == trans.VehicleName).Select(i => i.VehicleId).FirstOrDefault();
                    var keyId = transportationContext.Keys.Where(k => k.KeyName == trans.KeyName).Select(i => i.KeyId).FirstOrDefault();
                    var gasCardId = transportationContext.GasCards.Where(g => g.GasCardName == trans.GasCardName).Select(i => i.GasCardId).FirstOrDefault();

                    if (userId != 0 && vehicleId != 0 && keyId != 0 && gasCardId != 0)
                    {
                        SignOut signLog = new SignOut();
                        signLog.UserId = userId;
                        signLog.VehicleId = vehicleId;
                        signLog.KeyId = keyId;
                        signLog.Destination = trans.Destination;
                        signLog.GasCardId = gasCardId;
                        signLog.CheckOutTime = DateTime.Now;
                        signLog.ActivityTime = DateTime.Now;
                        transportationContext.SignOuts.Add(signLog);
                        transportationContext.SaveChanges();
                    }
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
        public ActionResult SignIn()
        {
            return View(transportationContext.Transportation_SignedOutView.ToList());
        }

        [HttpGet]
        public ActionResult ConfirmSignIn(int? id)
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
        public ActionResult ConfirmSignIn(SignOut trans)
        {
            try
            {
                if (ModelState.IsValid)
                {
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