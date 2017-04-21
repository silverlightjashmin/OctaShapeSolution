using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Net.Mail;

using System.Configuration;
using OctaShapeSolution.Common;
using OctaShape.Data;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    public class UsersController : Controller
    {
        private OctaShapeSolutionEntities db = new OctaShapeSolutionEntities();

        // GET: Users

        public ActionResult Index()
        {
            var user = db.User.Include(u => u.TicketBranch).Include(u => u.UserStatus);
            return View(user.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Status");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FirstName,LastName,UserName,HashPassword,Email,UserStatusId,BranchCode,EmailConfirmed")] User user)
        {
            var encyptedpassword = Encryption.Encrypt(user.UserName + user.HashPassword, true);
            user.HashPassword = encyptedpassword;

            try
            {
                if (ModelState.IsValid)
                {
                    db.User.Add(user);
                    db.SaveChanges();


                    //Send Email for Confirmation

                    // Get the key from config file
                    AppSettingsReader settingsReader = new AppSettingsReader();


                    string emailid = (string)settingsReader.GetValue("EmailId", typeof(String));
                    string password = (string)settingsReader.GetValue("Password", typeof(String));
                    string getsmtp = (string)settingsReader.GetValue("Smtp", typeof(String));
                    int getport = (int)settingsReader.GetValue("port", typeof(int));




                    MailMessage m = new MailMessage(emailid, user.Email);
                    m.Subject = "Please Confirm Your Email Id for eTicketSystem Application";
                    m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("Index", "VerifyEmail", new { id = user.id, Email = user.Email }, Request.Url.Scheme));
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient(getsmtp, getport);
                    smtp.Credentials = new System.Net.NetworkCredential(emailid, password);
                    smtp.EnableSsl = true;

                    smtp.Send(m);
                    return View("Index");

                }
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", user.BranchCode);
                ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Status", user.UserStatusId);
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", user.BranchCode);
                ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Status", user.UserStatusId);
                return View(user);
            }


        }
        

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", user.BranchCode);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Status", user.UserStatusId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FirstName,LastName,UserName,HashPassword,Email,UserStatusId,BranchCode,EmailConfirmed")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", user.BranchCode);
            ViewBag.UserStatusId = new SelectList(db.UserStatus, "Id", "Status", user.UserStatusId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
