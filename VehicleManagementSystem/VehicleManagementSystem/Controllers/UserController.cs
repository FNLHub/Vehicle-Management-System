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

            DropDownModel drop = new DropDownModel();
            List<SelectListItem> listItem = new List<SelectListItem>();

            for (int i = 1; i <= transportationContext.Users.Count(); i++)
            {

                drop.Id = transportationContext.Users.Where(v => v.UserId == i).Select(v => v.UserId).FirstOrDefault();
                drop.Value = transportationContext.Users.Where(v => v.UserId == i).Select(v => v.FirstName + " " + v.LastName).FirstOrDefault();

                listItem.Add(new SelectListItem() { Value = drop.Value, Text = drop.Id.ToString() });
            }
            ViewData["UserDropdown"] = new SelectList(listItem, "Text", "Value");

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
    }
}