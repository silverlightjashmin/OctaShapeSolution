using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class UserStatusController : Controller
    {
        private OctaShapeSolutionEntities db = new OctaShapeSolutionEntities();

        // GET: UserStatus
        public ActionResult Index()
        {
            return View(db.UserStatus.ToList());
        }

        // GET: UserStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStatus userStatus = db.UserStatus.Find(id);
            if (userStatus == null)
            {
                return HttpNotFound();
            }
            return View(userStatus);
        }

        // GET: UserStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] UserStatus userStatus)
        {
            if (ModelState.IsValid)
            {
                db.UserStatus.Add(userStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userStatus);
        }

        // GET: UserStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStatus userStatus = db.UserStatus.Find(id);
            if (userStatus == null)
            {
                return HttpNotFound();
            }
            return View(userStatus);
        }

        // POST: UserStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] UserStatus userStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userStatus);
        }

        // GET: UserStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStatus userStatus = db.UserStatus.Find(id);
            if (userStatus == null)
            {
                return HttpNotFound();
            }
            return View(userStatus);
        }

        // POST: UserStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserStatus userStatus = db.UserStatus.Find(id);
            db.UserStatus.Remove(userStatus);
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
