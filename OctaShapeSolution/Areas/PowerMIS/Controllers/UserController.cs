using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.PowerMIS.Controllers
{
    public class UserController : Controller
    {
        // GET: PowerMIS/User
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}