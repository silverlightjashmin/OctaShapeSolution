using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class TicketBranchesController : Controller
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();

        // GET: TicketBranches
        public ActionResult Index()
        {
            return View(db.TicketBranch.ToList());
        }

        // GET: TicketBranches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketBranch ticketBranch = db.TicketBranch.Find(id);
            if (ticketBranch == null)
            {
                return HttpNotFound();
            }
            return View(ticketBranch);
        }

        // GET: TicketBranches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchCode,BranchName,IsActive")] TicketBranch ticketBranch)
        {
            if (ModelState.IsValid)
            {
                db.TicketBranch.Add(ticketBranch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketBranch);
        }

        // GET: TicketBranches/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketBranch ticketBranch = db.TicketBranch.Find(id);
            if (ticketBranch == null)
            {
                return HttpNotFound();
            }
            return View(ticketBranch);
        }

        // POST: TicketBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchCode,BranchName,IsActive")] TicketBranch ticketBranch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketBranch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketBranch);
        }

        // GET: TicketBranches/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketBranch ticketBranch = db.TicketBranch.Find(id);
            if (ticketBranch == null)
            {
                return HttpNotFound();
            }
            return View(ticketBranch);
        }

        // POST: TicketBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TicketBranch ticketBranch = db.TicketBranch.Find(id);
            db.TicketBranch.Remove(ticketBranch);
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
