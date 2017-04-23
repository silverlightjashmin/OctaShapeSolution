using OctaShapeSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    [AuthorizeChecker]
    public class UserController : Controller
    {
        // GET: CardInformationSystem/User
        public ActionResult DashBoard() 
        {
            return View();
        }
    }
}