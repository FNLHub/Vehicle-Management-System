﻿using System;
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

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            //Currently Broken due to new Api token***********************************

            ////Empty User
            //TransporationRequest userFill = new TransporationRequest() { User = new Transportation.User() };

            ////Fill Empty User from db
            //userFill.User.FirstName = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.FirstName;
            //userFill.User.LastName = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.LastName;
            //userFill.User.BannerId = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.EmployeeId.Substring(1);
            //userFill.User.OfficePhoneNumber = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.OfficePhone;
            //userFill.User.Email = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]).userInfo.Email;
            //return View(userFill);
            
            UniversalDropDownClass _drop = new UniversalDropDownClass();
            ViewData["UserDropdown"] = new SelectList(_drop.PopulateUsers(), "Text", "Value");
            ViewData["VehicleAddonsDropdown"] = new SelectList(_drop.PopulateVehiclesAddons(), "Text", "Value");
            ViewData["VehicleTypesDropdown"] = new SelectList(_drop.PopulateVehicles(), "Text", "Value");
            ViewData["GasCardsDropdown"] = new SelectList(_drop.PopulateGasCards(), "Text", "Value");
            ViewData["ApprovedDriversDropdown"] = new SelectList(_drop.PopulateApprovedDrivers(), "Text", "Value");
           

            return View();
        }
        [HttpPost]
        public ActionResult NewRequest(TransportationRequest_View transRequest)
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

                        /* Testing default values */
                        newRequest.RequesterUserId = 1;
                        newRequest.LeaveDate = DateTime.Parse("7/1/2017");
                        newRequest.LeaveTime = TimeSpan.Parse("5:15");
                        newRequest.ReturnDate = DateTime.Parse("7/5/2017");
                        newRequest.ReturnTime = TimeSpan.Parse("18:35");
                        newRequest.Destination = "LA";
                        newRequest.TripPurpose = "Pancakes";
                        newRequest.NumOfStudents = 4;

                        //Create Empty Int Object
                        var transReqId = new ObjectParameter("TranReqId", typeof(int));
                        //Call Precedure and give transReqId a value
                        transportationContext.p_TransReq_Add(newRequest.RequesterUserId, newRequest.LeaveDate, newRequest.LeaveTime, newRequest.ReturnDate, newRequest.ReturnTime, newRequest.Destination, newRequest.TripPurpose, newRequest.NumOfStudents, transReqId);

                        Drivers _Drivers = new Drivers();
                        _Drivers.DriverUserId = 1;
                        _Drivers.NeedGasCard = false;
                        _Drivers.TransReqId = Convert.ToInt16(transReqId.Value);
                        _Drivers.VehicleAddonId = 1;
                        _Drivers.VehicleTypeId = 1;


                        if (transReqId != null)
                        {
                            //foreach (driver in array)
                            //{
                            //    insert into db
                            //}
                        }

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

        //public static List<SelectListItem> PopulateUsers(int TableId)
        //{
        //    FacilitiesDBEntities transcontext = new FacilitiesDBEntities();

        //    DropDownModel drop = new DropDownModel();
        //    List<SelectListItem> listItem = new List<SelectListItem>();

        //    for (int i = 1; i <= transcontext.Users.Count(); i++)
        //    {

        //        drop.Id = transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
        //        drop.Value = transcontext.Users.Where(v => v.UserId == i).Select(v => v.FirstName + " " + v.LastName).FirstOrDefault();

        //        listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
        //    }

        //    switch (TableId)
        //    {
        //        // Users
        //        case 1:
        //            for (int i = 1; i <= transcontext.Users.Count(); i++)
        //            {

        //                drop.Id = transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
        //                drop.Value = transcontext.Users.Where(v => v.UserId == i).Select(v => v.BannerId + " | " + v.FirstName + " | " + v.LastName).FirstOrDefault();

        //                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
        //            }
        //            break;
        //        // Vehicles
        //        case 2:
        //            for (int i = 1; i <= transcontext.Vehicles.Count(); i++)
        //            {

        //                drop.Id = transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleId).FirstOrDefault();
        //                drop.Value = transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleName).FirstOrDefault();

        //                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
        //            }
        //            break;
        //        // VehicleAddons
        //        case 3:
        //            for (int i = 1; i <= transcontext.VehicleAddons.Count(); i++)
        //            {

        //                drop.Id = transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.VehicleAddonId).FirstOrDefault();
        //                drop.Value = transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.AddonName).FirstOrDefault();

        //                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
        //            }
        //            break;
        //        // Gas Cards
        //        case 4:
        //            for (int i = 1; i <= transcontext.GasCards.Count(); i++)
        //            {

        //                drop.Id = transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardId).FirstOrDefault();
        //                drop.Value = transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardName).FirstOrDefault();

        //                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
        //            }
        //            break;



        //    }
        //    return listItem;
        //}

    }
}