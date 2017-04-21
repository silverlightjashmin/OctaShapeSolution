using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult DashBoard()
        {
            if (Session["User_Id"]==null)
            {
                return RedirectToAction("Authenticate", "Login");
            }
            else
            {
                return View();
            }
           
        }
    }
}