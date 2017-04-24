
using OctaShapeSolution.Models;
using OctaShapeSolution.Common;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShape.Common;

namespace OctaShapeSolution.Controllers
{
    public class AccountController : Controller
    {
        private OctaShapeSolutionEntities db = new OctaShapeSolutionEntities();

        //Set UserStatus as per DB
        private class GetStatus
        {
            public int Registerd = 1;            
            public int Verified = 2;
            public int Locked = 3;
            public int Blocked = 4;

        }

        // GET: Account
      public ActionResult RegisterUser()
        {
          
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");
            return View();
                       
           
        }

        // POST:User
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisterUserViewModel RVM)
        {
            User u = new User();
            u.FirstName = RVM.FirstName;
            u.LastName = RVM.LastName;
            u.UserName = RVM.UserName;
            u.Email = RVM.Email;
            u.BranchCode = RVM.BranchCode;
            u.UserStatusId = new GetStatus().Registerd;
            u.EmailConfirmed = false;

            //call encryption
            var encyptedpassword =Encryption.Encrypt(RVM.UserName + RVM.Password, true);
            u.HashPassword = encyptedpassword;

            try
            {



                if (ModelState.IsValid)
                {

                    
                    db.User.Add(u);
                    db.SaveChanges();

                   
                    string Subject = "Please Confirm Your Email Id for eTicketSystem Application";
                    string Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", u.UserName, Url.Action("VerifyEmail", "Account", new { id = u.id, Email = u.Email }, Request.Url.Scheme));

                    SendEmail se = new SendEmail();

                    string messageto = u.Email;

                    se.SendEmails(Subject, Body, messageto);

                    return RedirectToAction("Login", "Home");
                }
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", u.BranchCode);
                return View(RVM);
            }
            catch (Exception ex)
            {
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", u.BranchCode);

                ModelState.AddModelError("", ex.Message);
                return View(RVM);
            }





               

            
            
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        //POST: Forgot Password
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel FPVM)
        {

            if (ModelState.IsValid)
            {
                var finduser = db.SearchUser(FPVM.Email).FirstOrDefault();

                if (finduser != null)
                {
                    //Reset the password to default password and send email
                    string commonpassword = "@SzWLO58$Rs";
                    string username = finduser.UserName;
                    string email = finduser.Email;
                    var encyptedpassword = Encryption.Encrypt(username + commonpassword, true);

                    //update password to default
                    db.ResetPassword(username, encyptedpassword);

                    //send default password to user email id
                  
                    string Subject = "Password Reset for eTicketSystem";
                    string Body = string.Format("Dear {0}<BR/>Your Password has been successfully reset. Your New Password is {1} <BR/> Please Change your Password after First Login.", username, commonpassword);

                    SendEmail se = new SendEmail();
                    string messageto = email;

                    se.SendEmails(Subject, Body, messageto);

                    return View("ForgotPasswordConfirmation");
                }
                else
                {
                    ModelState.AddModelError("", "We Couldnot Find Your EmailId or UserName In The System");
                    return View(FPVM);
                }


                    // Don't reveal that the user does not exist or is not confirmed

                }
            //error msg 

            return View(FPVM);
        }


        //GET Change password page
        public ActionResult ChangePassword()
        {
            string username = Session["User_Name"].ToString();

            if (username != null)
            { 
                return View();
            }
            else
            {

                ModelState.AddModelError("", "You are not logged in. Please Login First.");
                return RedirectToAction("Authenticate", "Login");
            }
                

            
        }
           
        


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel cpvm)
        {
        if (ModelState.IsValid)
        {
            string username = Session["User_Name"].ToString();

            if (username != null)
            {
                //Reset the password to default password and send email

                var encyptedpassword = Encryption.Encrypt(username + cpvm.Password, true);

                //update password to default
                db.ResetPassword(username, encyptedpassword);

                if (Session["User_Group"].ToString() == "Admin")
                {
                    return RedirectToAction("DashBoard", "DashBoard");
                }
                else
                    return RedirectToAction("UserDashBoard", "UserDashBoard");

            }
            else
            {
                ModelState.AddModelError("", "You are not logged in. Please Login First.");
                return RedirectToAction("Login", "Home");
            }


        } // Don't reveal that the user does not exist or is not confirmed

            
            //error msg 

            return View(cpvm);
        }


        public ActionResult AssignRole()
        {
            ViewBag.UserId = new SelectList(db.User, "id", "UserName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole(AssignRole AR)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    db.AddUserRole(AR.UserId, AR.RoleId);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

                ViewBag.UserId = new SelectList(db.User, "id", "UserName");
                ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");

                return View();

            }

            ViewBag.UserId = new SelectList(db.User, "id", "UserName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");

            return View();
        }

        public ActionResult VerifyEmail(int? id, string Email)  

        {


            db.VerifyEmail(id, Email);
            db.SaveChanges();
            return View();


        }

        public ActionResult CallDayEnd()
        {
            ViewBag.Branch_Name = db.TicketBranch.Find(Session["Branch_Code"].ToString()).BranchName;

            


            return View();
        }

       
       
           

    }

        

    }

    
