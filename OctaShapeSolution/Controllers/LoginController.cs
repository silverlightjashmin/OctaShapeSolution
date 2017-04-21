using System;
using System.Linq;
using System.Web.Mvc;
using OctaShapeSolution.Common;
using OctaShapeSolution.Models;
using OctaShape.Data;

namespace OctaShapeSolution.Controllers
{
    public class LoginController : Controller
    {
        private OctaShapeSolutionEntities db = new OctaShapeSolutionEntities();
        
        // GET: Login
        public ActionResult Authenticate()
        {
            {
                //Check Session Details for already checkin user

                if (Session["User_Id"] != null)
                {

                    return RedirectToAction("DashBoard", "DashBoard");

                }
                return View();

            }

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(AuthenticateViewModel user)
        {
            string Status_Name = "";
            string User_Name = "";
            int User_Id = 0;
            string Branch_Code      = "";
            bool IsAdmin = false;

            //encrypt the password by combining username and password
            var encryptedpassword = Encryption.Encrypt(user.UserName + user.Password, true);

            if (ModelState.IsValid)
            {

                var logininfo = db.Loginauthenticate(user.UserName, encryptedpassword).ToList();

                if (logininfo != null && logininfo.Count() > 0)
                {

                    foreach (var item in logininfo)
                    {
                        //user status
                        Status_Name = Convert.ToString(item.UserStatus);

                        //username
                        User_Name = Convert.ToString(item.UserName);

                        //userid
                        User_Id = Convert.ToInt32(item.id);

                        //Branch
                        Branch_Code  = item.BranchCode;

                        
                    }

                    //get role details for user
                    var groupinfo = db.FindUserRole(User_Id).ToList().FirstOrDefault();

                    //Check is admin 
                    IsAdmin = Convert.ToBoolean(groupinfo.IsAdmin);

                    //check user is registered only
                    if (Status_Name == "Registered")
                    {
                        ModelState.AddModelError("", "User Verification Pending. Please Check Your Email for Verification");
                    }

                    //check user is verified
                    else if (Status_Name == "Verified")
                    {
                        //check if user had verfired its email address
                        Session["User_Id"] = User_Id;
                        Session["User_Name"] = User_Name;
                        Session["User_Group"] = groupinfo.RoleName;
                        Session["Branch_Code"] = Branch_Code;
                        Session["IsAdmin"] = IsAdmin;

/*
                        //get host id and host name
                        string hostname = Dns.GetHostName();
                        IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);
                        string HostId= ipHostInfo.AddressList[2].ToString();

                        //save user login session 
                        db.AddUserLoginSession(User_Name, hostname, HostId, DateTime.Now);
                        db.SaveChanges();

    */
                        return RedirectToAction("DashBoard", "DashBoard");
                    }
                    
                        

            

            //check if user is blocked
            else if (Status_Name == "Locked")
            {
                ModelState.AddModelError("", " User is Locked");
            }


            //check if user is locked
            else if (Status_Name == "Blocked")
            {
                ModelState.AddModelError("", " User is Blocked. Please Contact System Administrator");
            }

            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

                }

            }

            // If we got this far, something failed, redisplay form
            return View();
        }


        public ActionResult Logoff()
        {
            Session["User_Id"] = null;
            Session["User_Name"] = null;
            Session["User_Group"] = null;
            Session["Branch_Code"] = null;
            Session["IsAdmin"] = null;

            return RedirectToAction("Authenticate", "Login");

        }

    }
}