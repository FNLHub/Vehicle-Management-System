using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

//For Dropdown Box Model
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class UserController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();
        static int count = 0;

        //Global List Variables used to speedup partial view populate
        static SelectList _Users;
        static SelectList _VehicleAddons;
        static SelectList _VehicleTypes;
        static SelectList _Vehicles;
        static SelectList _GasCards;
        static SelectList _ApprovedDrivers;


        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //Append Driver to driver table
        public ActionResult AppendDriver()
        {
            //Used for deleting rows
            ViewBag.count = count++;

            //Used for populating dropdowns
            ViewData["UserDropdown"] = _Users;
            ViewData["VehicleAddonsDropdown"] = _VehicleAddons;
            ViewData["VehicleTypesDropdown"] = _VehicleTypes;
            ViewData["GasCardsDropdown"] = _GasCards;
            ViewData["Vehicles"] = _Vehicles;
            ViewData["ApprovedDriversDropdown"] = _ApprovedDrivers;

            return PartialView("~/Views/Partial/AppendDriver.cshtml", new Drivers());
        }


        [HttpGet]
        public ActionResult NewRequest()
        {


            //Empty User
            TransportationRequest_View_DemoForSymposium userFill = new TransportationRequest_View_DemoForSymposium();

            var User = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);


            userFill.BannerId = User.userInfo.EmployeeId.Substring(1);
            userFill.FirstName = User.userInfo.FirstName;
            userFill.LastName = User.userInfo.LastName;
            userFill.OfficePhoneNumber = User.userInfo.OfficePhone;
            userFill.Email = User.userInfo.Email;

            UniversalDropDownClass _drop = new UniversalDropDownClass();
            //Populate Global Variables
            _Users = new SelectList(_drop.PopulateUsers(), "Text", "Value");
            _VehicleAddons = new SelectList(_drop.PopulateVehiclesAddons(), "Text", "Value");
            _Vehicles = new SelectList(_drop.PopulateVehicles(), "Text", "Value");
            _GasCards = new SelectList(_drop.PopulateGasCards(), "Text", "Value"); ;
            _ApprovedDrivers = new SelectList(_drop.PopulateApprovedDrivers(), "Text", "Value");
            _VehicleTypes = new SelectList(_drop.PopulateVehicleTypes(), "Text", "Value");

            //Populate ViewData's
            ViewData["UserDropdown"] = _Users;
            ViewData["VehicleAddonsDropdown"] = _VehicleAddons;
            ViewData["VehicleTypesDropdown"] = _VehicleTypes;
            ViewData["VehiclesDropdown"] = _Vehicles;
            ViewData["GasCardsDropdown"] = _GasCards;
            ViewData["ApprovedDriversDropdown"] = _ApprovedDrivers;

            return View(userFill);
        }
        [HttpPost]
        public ActionResult NewRequest(TransportationRequest_View_DemoForSymposium transRequest)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Grabs current logged on user ID as they will be the requester
                    var RequesterId = transportationContext.Users.Where(u => u.BannerId == transRequest.BannerId).Select(i => i.UserId).FirstOrDefault();

                    if (RequesterId != null)
                    {
                        TransportationRequest newRequest = new TransportationRequest();
                        newRequest.RequesterUserId = RequesterId;
                        newRequest.LeaveDate = transRequest.LeaveDate.GetValueOrDefault();
                        newRequest.LeaveTime = transRequest.LeaveTime.GetValueOrDefault();
                        newRequest.ReturnDate = transRequest.ReturnDate.GetValueOrDefault();
                        newRequest.ReturnTime = transRequest.ReturnTime.GetValueOrDefault();
                        newRequest.Destination = transRequest.Destination;
                        newRequest.TripPurpose = transRequest.TripPurpose;
                        newRequest.NumOfStudents = transRequest.NumOfStudents.GetValueOrDefault();

                        //Create Empty Int Object
                        var transReqId = new ObjectParameter("TranReqId", typeof(int));
                        //Save Request and Get transportation request Id
                        transportationContext.p_TransReq_Add(newRequest.RequesterUserId, newRequest.LeaveDate, newRequest.LeaveTime, newRequest.ReturnDate, newRequest.ReturnTime, newRequest.Destination, newRequest.TripPurpose, newRequest.NumOfStudents, transReqId);
                        
                        //Save to Driver Group Table
                        DriverGroup driverGroup = new DriverGroup();
                        driverGroup.NeedGasCard = transRequest.NeedGasCard.GetValueOrDefault();
                        driverGroup.TranRequestId = Convert.ToInt16(transReqId.Value);
                        driverGroup.UserId = transRequest.UserId;
                        driverGroup.VehicleAddOnId = transRequest.VehicleAddOnId;
                        driverGroup.VehicleTypeId = transRequest.VehicleTypeId;

                        //Add new driver associated with request
                        transportationContext.DriverGroups.Add(driverGroup);
                        //save
                        transportationContext.SaveChanges();

                        return RedirectToAction("RequestConfirmation");
                    }


                    // Will require a foreach loop to grab all drivers listed on request
                    //var DriverId = transportationContext.Users.Where(u => u.UserId == transRequest.UserId).Select(i => i.UserId).FirstOrDefault();

                    // Will require a foreach loop to grab all passangers listed on the request
                    //var PassangerId = transportationContext.Users.Where(u => u.UserId == transRequest.UserId).Select(i => i.UserId).FirstOrDefault();

                    // Add all drivers to DriverGroup Table
                    //foreach ( /* Driver listed on form (may need to make an array) */)
                    //{
                    //    DriverGroup Driver = new DriverGroup();
                    //    Driver.RequestId = 0/*Transportation Request primary key*/;
                    //    Driver.UserId = transportationContext.Users.Where(u => u.UserId == 0/*Current Driver Id*/).Select(i => i.UserId).FirstOrDefault();
                    //    if (true /*Driver requires a vehicle*/)
                    //    {
                    //        //Driver.VehicleType
                    //    }
                    //    transportationContext.DriverGroups.Add(Driver);
                    //}

                }
                catch
                {
                    return new HttpStatusCodeResult(Response.StatusCode = 400);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult RequestConfirmation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestConfirmation(int? x)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUserInfo()
        {
            var userAuth = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);

            User userFill = new TransportationDB.User();

            userFill.FirstName = userAuth.userInfo.FirstName;
            userFill.LastName = userAuth.userInfo.LastName;
            userFill.BannerId = userAuth.userInfo.EmployeeId;
            userFill.Email = userAuth.userInfo.Email;
            userFill.OfficePhoneNumber = userAuth.userInfo.OfficePhone;

            return View(userFill);
        }
        [HttpPost]
        public ActionResult EditUserInfo(int? x)
        {
            return RedirectToAction("NewRequest");
        }

    }
}