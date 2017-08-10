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
        //UserFill Information
        private static LoginController.AuthorizeObject _LoggedInUser;


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

            //Using Global Variables to set the ViewData Upon AppendDriver();
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

            //var User = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);
            _LoggedInUser = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);

            //Check if logged in
            if (_LoggedInUser == null)
            {
                //If no user is logged in then redirect to login page
                return RedirectToAction("../Login/Index");

            }

            //Empty User
            TransportationRequest_View_DemoForSymposium userFill = new TransportationRequest_View_DemoForSymposium();

            userFill.BannerId = _LoggedInUser.userInfo.EmployeeId.Substring(1);
            userFill.FirstName = _LoggedInUser.userInfo.FirstName;
            userFill.LastName = _LoggedInUser.userInfo.LastName;
            userFill.OfficePhoneNumber = _LoggedInUser.userInfo.OfficePhone;
            userFill.Email = _LoggedInUser.userInfo.Email;

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
                    //Note: This Lambda function is using the logged in user as the requester so if html is edited the validation is still correct
                    var RequesterId = transportationContext.Users.Where(u => u.BannerId == _LoggedInUser.userInfo.EmployeeId.Substring(1)).Select(i => i.UserId).FirstOrDefault();

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
            //Set User
            _LoggedInUser = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);

            //Check if logged in
            if (_LoggedInUser == null)
            {
                //If no user is logged in then redirect to login page
                return RedirectToAction("../Login/Index");

            }

            User userFill = new TransportationDB.User();

            userFill.FirstName = _LoggedInUser.userInfo.FirstName;
            userFill.LastName = _LoggedInUser.userInfo.LastName;
            userFill.BannerId = _LoggedInUser.userInfo.EmployeeId;
            userFill.Email = _LoggedInUser.userInfo.Email;
            userFill.OfficePhoneNumber = _LoggedInUser.userInfo.OfficePhone;

            return View(userFill);
        }
        [HttpPost]
        public ActionResult EditUserInfo(User user)
        {
            User _user = new TransportationDB.User();

            _user.FirstName = _LoggedInUser.userInfo.FirstName;
            _user.LastName = _LoggedInUser.userInfo.LastName;
            _user.BannerId = _LoggedInUser.userInfo.EmployeeId.Substring(1);
            _user.Email = _LoggedInUser.userInfo.Email;
            _user.OfficeAreaCode = "559";
            _user.OfficePhoneNumber = _LoggedInUser.userInfo.OfficePhone;
            _user.CellAreaCode = user.CellAreaCode;
            _user.CellPhoneNumber = user.CellPhoneNumber;
            _user.ApprovedDmv = false;
            _user.SupervisorEmail = null;
            _user.StatusId = 1;

            //Add new User to the User Table
            transportationContext.Users.Add(_user);
            //save user
            transportationContext.SaveChanges();

            return RedirectToAction("NewRequest");
        }

        [HttpGet]
        public ActionResult ViewHistory() {

            //Set global user logged in
            _LoggedInUser = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);

            //Check if logged in
            if (_LoggedInUser == null) {
                //If no user is logged in then redirect to login page
                return RedirectToAction("../Login/Index");

            }

            return View(transportationContext.TransportationRequest_View_DemoForSymposium.Where(m => m.BannerId == _LoggedInUser.userInfo.EmployeeId.Substring(1)).ToList());

        }

    }
}