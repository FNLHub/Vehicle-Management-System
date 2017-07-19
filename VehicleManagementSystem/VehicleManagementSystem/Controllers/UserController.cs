using System;
using System.Collections.Generic;
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

            ViewData["UserDropdown"] = new SelectList(PopulateUsers(), "Text", "Value");

            //List<SelectListItem> listItem = new List<SelectListItem>();

            //for (int i = 1; i <= transportationContext.Users.Count(); i++)
            //{
            //    listItem.Add(new SelectListItem() { Value = transportationContext.Users.Where(v => v.UserId == i).Select(v => v.FirstName +" " + v.LastName).FirstOrDefault(), Text = transportationContext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault().ToString() });
            //}

            ////ViewData["VehicleDropDown"] = populateDropDown(transportationContext.Vehicles);
            ////ViewData["KeyDropDown"] = populateDropDown(transportationContext.Keys);
            //ViewData["UserDropdown"] = new SelectList(listItem, "Text", "Value");

            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(TransporationRequest transRequest)
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

        public static List<SelectListItem> PopulateUsers(int TableId)
        {
            FacilitiesDBEntities transcontext = new FacilitiesDBEntities();

            DropDownModel drop = new DropDownModel();
            List<SelectListItem> listItem = new List<SelectListItem>();

            for (int i = 1; i <= transcontext.Users.Count(); i++)
            {

                drop.Id = transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
                drop.Value = transcontext.Users.Where(v => v.UserId == i).Select(v => v.FirstName + " " + v.LastName).FirstOrDefault();

                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
            }

            switch (TableId)
            {
                // Users
                case 1:
                    for (int i = 1; i <= transcontext.Users.Count(); i++)
                    {

                        drop.Id = transcontext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
                        drop.Value = transcontext.Users.Where(v => v.UserId == i).Select(v => v.BannerId + " | " + v.FirstName + " | " + v.LastName).FirstOrDefault();

                        listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
                    }
                    break;
                // Vehicles
                case 2:
                    for (int i = 1; i <= transcontext.Vehicles.Count(); i++)
                    {

                        drop.Id = transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleId).FirstOrDefault();
                        drop.Value = transcontext.Vehicles.Where(v => v.VehicleId == i).Select(v => v.VehicleName).FirstOrDefault();

                        listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
                    }
                    break;
                // VehicleAddons
                case 3:
                    for (int i = 1; i <= transcontext.VehicleAddons.Count(); i++)
                    {

                        drop.Id = transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.VehicleAddonId).FirstOrDefault();
                        drop.Value = transcontext.VehicleAddons.Where(v => v.VehicleAddonId == i).Select(v => v.AddonName).FirstOrDefault();

                        listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
                    }
                    break;
                // Gas Cards
                case 4:
                    for (int i = 1; i <= transcontext.GasCards.Count(); i++)
                    {

                        drop.Id = transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardId).FirstOrDefault();
                        drop.Value = transcontext.GasCards.Where(v => v.GasCardId == i).Select(v => v.GasCardName).FirstOrDefault();

                        listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
                    }
                    break;



            }
            return listItem;
        }

    }
}