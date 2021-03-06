﻿
using OctaShapeSolution.Models;
using OctaShape.Common;
using System;
using System.Linq;
using System.Web.Mvc;
using OctaShape.Data;


namespace OctaShapeSolution.Controllers
{
    
    public class AccountController : Controller
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();

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

        [AuthorizeChecker]
        public ActionResult RegisterUserByAdmin()
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
            var encyptedpassword = Encryption.Encrypt(RVM.UserName + RVM.Password, true);
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
                      
                    ViewBag.Result = "Your User Has Been Created Successfully, Please Check Your Email";
                    ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");

                    return View();
                }
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");
                ViewBag.Result = "Error";
                return View(RVM);
            }
            catch (Exception ex)
            {
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");

                ModelState.AddModelError("", ex.Message);
                return View(RVM);
            }









        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUserByAdmin(RegisterUserViewModel RVM)  
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
            var encyptedpassword = Encryption.Encrypt(RVM.UserName + RVM.Password, true);
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

                    ViewBag.Result = "Your User Has Been Created Successfully, Please Check Your Email";
                    ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");

                    return View();
                }
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");
                ViewBag.Result = "Error";
                return View(RVM);
            }
            catch (Exception ex)
            {
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");

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
        [AuthorizeChecker]
        public ActionResult ChangePassword()
        {
            string username = Session["User_Name"].ToString();

            if (username != null)
            {
                return View();
            }
            else
            {

                
                return RedirectToAction("Authenticate", "Login");
            }



        }



        [AuthorizeChecker]

        [HttpPost]
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

                    db.SaveChanges();

                    ViewBag.Message = "Password Changed Successfully";
                    return View();

                }
                else
                {
                    ModelState.AddModelError("", "You are not logged in. Please Login First.");
                    return RedirectToAction("Authenticate", "Login");
                }


            } // Don't reveal that the user does not exist or is not confirmed


            //error msg 

            return View(cpvm);
        }


        [AuthorizeChecker]
        public ActionResult AssignRole()
        {
            ViewBag.UserId = new SelectList(db.User, "id", "UserName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");

            return View();
        }

        [AuthorizeChecker]
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
                ViewBag.Message = "Role Assigned To User Successfully";
                return View();

            }

            ViewBag.UserId = new SelectList(db.User, "id", "UserName");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName");
            ViewBag.Message = "Role Couldnot Be Assigned To User";
            return View();
        }

        public ActionResult VerifyEmail(int? id, string Email)

        {


            db.VerifyEmail(id, Email);
            db.SaveChanges();
            return View();


        }

        [AuthorizeChecker]
        public ActionResult CallDayEnd()
        {


            ViewBag.Branch_Code = Session["Branch_Code"].ToString();

            ViewBag.Branch_Name = db.TicketBranch.Find(Session["Branch_Code"].ToString()).BranchName;

            DateTime date = DateTime.Now.Date;

            var exportdata = db.GetDayEndStatus().ToList();

            bool status = false;

            if (exportdata.Count()>=1)
            {
                status = Convert.ToBoolean(exportdata.Where(x => x.Branch_Code == Session["Branch_Code"].ToString()).FirstOrDefault().Is_DayEnd);

            }

            //pendin day end confirmation branch count
            ViewBag.pendingbranch = db.GetPendingBranch().Count();

            ViewBag.pendingbranchdetails = db.GetPendingBranch().ToList();

            //data for line chart

            var totallist = db.Day_EndDetail.ToList();

            var rdate = (from temp in totallist
                         where temp.Branch_Code == Session["Branch_Code"].ToString()
                         orderby temp.Request_Date descending
                         select temp.Request_Date.Value.ToShortDateString()).Take(7).ToList();

                var rtime    = (from temp in totallist
                                where temp.Branch_Code == Session["Branch_Code"].ToString()
                                orderby temp.Request_Date descending
                                select temp.Request_Time.Value.TotalHours).Take(7).ToList();

                string UserName1 = string.Join(" \",\"", rdate);

                string WorkLoad1 = string.Join(",", rtime);
                string fl = UserName1.Insert(0, "\"");
                string ll = fl.Insert(fl.Length, "\"");
                ViewBag.M = ll.Trim();
                ViewBag.P = WorkLoad1.Trim();


             



            if (status == true)

            {
                ViewBag.Status = "ON";
                ViewBag.message = "Day End  Flag";
            }
            else
            {
                ViewBag.Status = "OFF";
                ViewBag.message = "Day End  Flag";
            }

            ViewBag.Data = exportdata;


           
            

            return View();
        }


        [AuthorizeChecker]
        public ActionResult AddDayEndStat()
        {
            var TodayDate = DateTime.Now.Date;
            var bc = Session["Branch_Code"].ToString();
            var checkIsDayEnd = db.Day_EndDetail.Where(x => x.Branch_Code == bc && x.Request_Date == TodayDate).ToList();

            if(checkIsDayEnd.Count()>=1)
            {
                //update old data
                db.DayEndStatOn(Session["Branch_Code"].ToString());
                db.SaveChanges();
            }
            else
            {
                db.CallForDayEnd(Session["User_Name"].ToString(), Session["Branch_Code"].ToString());
                db.SaveChanges();
            }





            ViewBag.Message1 = "Day End Notification Send";

            return RedirectToAction("CallDayEnd");
        }

        [AuthorizeChecker]
        public ActionResult DayEndStatOff()
        {

            db.DayEndStatOff(Session["Branch_Code"].ToString());
            db.SaveChanges();
            return RedirectToAction("CallDayEnd");
        }

        
    }



}


