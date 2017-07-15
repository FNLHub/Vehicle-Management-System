using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace Voyager2._0.Controllers
{
    public class HomeController : Controller
    {
        FacilitiesDBEntities TransportationContext = new FacilitiesDBEntities();

        public ActionResult Index()
        {
            if (1==3) // if user is not authenticated
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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