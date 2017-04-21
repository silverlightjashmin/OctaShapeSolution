using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: CardInformationSystem/Admin
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}