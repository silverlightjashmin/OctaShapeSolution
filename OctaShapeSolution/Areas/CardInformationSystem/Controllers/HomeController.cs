using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: CardInformationSystem/Home
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
            else if (Session["User_Group"].ToString() == "A")
            {
                return RedirectToAction("DashBoard", "User");
            }
            else if (Session["User_Group"].ToString() == "B")
            {
                return RedirectToAction("DashBoard", "User");
            }
            else if (Session["User_Group"].ToString() == "c")
            {
                return RedirectToAction("DashBoard", "User");
            }
            else if (Session["User_Group"].ToString() == "D")
            {
                return RedirectToAction("DashBoard", "User");
            }
            else
            {
                return RedirectToAction("NoAuthorize", "Home");
            }
        }
        public ActionResult NoAuthorize()
        {
            return View();
        }
    }
}