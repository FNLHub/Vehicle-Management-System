using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace VehicleManagementSystem.Controllers
{
    public class AdminController : Controller
    {

        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //----------------------------------------------- Create DB Entry Views  ----------------------------------------------------------
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
        //----------------------------------------------- Create DB Entry Views End ----------------------------------------------------------

    }
}