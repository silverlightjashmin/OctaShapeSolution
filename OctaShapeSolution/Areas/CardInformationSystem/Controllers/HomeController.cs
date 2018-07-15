using OctaShapeSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    [AuthorizeChecker]
    public class HomeController : Controller
    {
        // GET: CardInformationSystem/Home
        public ActionResult Index()
        {
            if (Session["User_Id"] == null)
            {
                return RedirectToAction("NoAuthorize", "Home");
            }
            else
            {

                if (Convert.ToBoolean(Session["IsAdmin"]) == true)
                {
                    return RedirectToAction("DashBoard", "Admin");
                }

                else
                {
                    return RedirectToAction("DashBoard", "User");
                }
            }
           
        }
        public ActionResult NoAuthorize()
        {
            return View();
        }
    }
}