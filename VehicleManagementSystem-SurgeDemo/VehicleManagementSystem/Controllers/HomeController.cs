using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportationDB;

namespace VehicleManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        public ActionResult Index()
        {
            if (LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]) == null) // if user is not authenticated
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                var _LoggedInUser = LoginController.GetAuthorize(Request.Cookies[LoginController.userToken]);
                return View(transportationContext.TransportationRequest_View_DemoForSymposium.Where(u => u.BannerId == _LoggedInUser.userInfo.EmployeeId.Substring(1)).ToList());
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
    }
}