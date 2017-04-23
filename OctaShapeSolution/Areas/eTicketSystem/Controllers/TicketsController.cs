using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class TicketsController : Controller
    {
        private OctaShapeSolutionEntities db = new OctaShapeSolutionEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus);
            return View(ticket.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName");
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName");
            ViewBag.AssignedTo = new SelectList(db.User, "id", "FirstName");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,Description,TicketPriorityId,CreatedBy,CreatedDate,TicketStatusId,EditEnable,LastEditedOn,AssignedBy,Categoryid,BranchCode,AssignedTo")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", ticket.BranchCode);
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.AssignedTo = new SelectList(db.User, "id", "FirstName", ticket.AssignedTo);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1", ticket.TicketStatusId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", ticket.BranchCode);
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.AssignedTo = new SelectList(db.User, "id", "FirstName", ticket.AssignedTo);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Description,TicketPriorityId,CreatedBy,CreatedDate,TicketStatusId,EditEnable,LastEditedOn,AssignedBy,Categoryid,BranchCode,AssignedTo")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchCode = new SelectList(db.TicketBranch, "BranchCode", "BranchName", ticket.BranchCode);
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.AssignedTo = new SelectList(db.User, "id", "FirstName", ticket.AssignedTo);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1", ticket.TicketStatusId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
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
