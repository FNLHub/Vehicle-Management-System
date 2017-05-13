using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportation.Controllers.Admin.Edit
{
    public class EditController : Controller
    {
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        // Partial Views for Edit/Delete Querys
        public ActionResult GetUsers()
        {
            return View(transportationContext.Users.ToList());
        }
    }
}