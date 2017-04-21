using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        // GET: eTicketSystem/Home
        public ActionResult Index()
        {
            if (Session["User_Id"] == null)
            {
                return Redirect("http://localhost");
            }
            else if (Convert.ToBoolean(Session["IsAdmin"]) == true)
            {
                return RedirectToAction("DashBoard", "Admin");
            }
            else
            {
                return RedirectToAction("DashBoard", "User");
            }
        }
    }
}