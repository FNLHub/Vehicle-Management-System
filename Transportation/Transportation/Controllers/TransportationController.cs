using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

//populate dropdown test
using Transportation.Models;

namespace Transportation.Controllers
{
    public class TransportationController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();
        static int count = 0;
        
        public ActionResult CreateDriver()
        {
            ViewBag.count = count++;
            return PartialView("~/Views/Injections/AppendingToDriverTable.cshtml");
        }

        [HttpGet] // GET: Transportation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TransportationRequest()
        {
            //Empty User
            TransporationRequest userFill = new TransporationRequest() { User = new Transportation.User() };
            
            //Fill Empty User from db
            userFill.User.FirstName = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.FirstName;
            userFill.User.LastName = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.LastName;
            userFill.User.BannerId = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.EmployeeId.Substring(1);
            userFill.User.OfficePhoneNumber = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.OfficePhone;
            userFill.User.Email = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.Email;
            return View(userFill);
        }

        [HttpPost]
        public ActionResult TransportationRequest(TransporationRequest transRequest)
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

        //SelectList populateDropDown(System.Data.Entity.DbSet transContext)
        //{
        //    List<SelectListItem> listItem = new List<SelectListItem>();
            
        //    for (int i = 1; i <= transContext.Count(); i++)
        //    {
        //        listItem.Add(new SelectListItem() { Value = transContext.Where(v => v.VehicleId == i).Select(v => v.VehicleName).FirstOrDefault(), Text = transportationContext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleId).FirstOrDefault().ToString() });
        //    }

        //    SelectList dropDown = new SelectList(listItem, "Text", "Value");
        //    return dropDown;
        //}
           
        [HttpGet]
        public ActionResult CheckOut()
        {
            //List<SelectListItem> listItem = new List<SelectListItem>();

            //for (int i = 1; i <= transportationContext.Vehicles.Count(); i++ )
            //{
            //    listItem.Add(new SelectListItem() { Value = transportationContext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleName).FirstOrDefault(), Text = transportationContext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleId).FirstOrDefault().ToString() });
            //}

            ////ViewData["VehicleDropDown"] = populateDropDown(transportationContext.Vehicles);
            ////ViewData["KeyDropDown"] = populateDropDown(transportationContext.Keys);
            //ViewData["VehicleDropdown"] = new SelectList(listItem, "Text", "Value");

            //Dropdown Test
            
            //----- Dropdown Test

            return View();
        }

        //Dropdown Test

        //private List<vehicleDropdown> _vehicleDropdown;
        //public IEnumerable<SelectListItem> vehicleItems
        //{
        //    get
        //    {
        //        IEnumerable<SelectListItem> selectVehicle = new List<SelectListItem>();
        //        selectVehicle = _vehicleDropdown.Select(x => new SelectListItem
        //        {
        //            Value = x.vehicleId.ToString(),
        //            Text = x.vehicleName
        //        });
        //        return DefaultItem.Concat(selectVehicle);
        //    }
        //}

        //public IEnumerable<SelectListItem> DefaultItem
        //{
        //    get
        //    {
        //        return Enumerable.Repeat(new SelectListItem
        //        {
        //            Value = "",
        //            Text = "- SELECT -"
        //        }, count: 1);
        //    }
        //}

        //public class GenericViewModel
        //{
        //    [Display(Name = "Vehicle")]
        //    public Guid SelectedCountryId { get; set; }
        //    public List<SelectListItem> vehicleItems { get; set; }

        //    public GenericViewModel()
        //    {
        //        var ddlHelper = new DropDownListHelper();
        //        this.vehicleItems = ddlHelper.CountryItems.ToList();
        //    }
        //}

        //------Dropdown Test End
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