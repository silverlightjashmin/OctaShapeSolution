using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    public class ProfileController : System.Web.Mvc.Controller
    {
        // GET: Profile
      public ActionResult MYProfile()
        {
            return View();
        }
    }
}