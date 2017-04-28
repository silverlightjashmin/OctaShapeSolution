using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class TicketCategoriesController : Controller
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();

        // GET: TicketCategories
        public ActionResult Index()
        {
            var ticketCategory = db.TicketCategory.Include(t => t.TicketCategory2);
            return View(ticketCategory.ToList());
        }

        // GET: TicketCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategory.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            return View(ticketCategory);
        }

        // GET: TicketCategories/Create
        public ActionResult Create()
        {
            ViewBag.ParentCategoryId = new SelectList(db.TicketCategory, "id", "CategoryName");
            return View();
        }

        // POST: TicketCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CategoryName,ParentCategoryId")] TicketCategory ticketCategory)
        {
            if (ModelState.IsValid)
            {
                db.TicketCategory.Add(ticketCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentCategoryId = new SelectList(db.TicketCategory, "id", "CategoryName", ticketCategory.ParentCategoryId);
            return View(ticketCategory);
        }

        // GET: TicketCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategory.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentCategoryId = new SelectList(db.TicketCategory, "id", "CategoryName", ticketCategory.ParentCategoryId);
            return View(ticketCategory);
        }

        // POST: TicketCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CategoryName,ParentCategoryId")] TicketCategory ticketCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategoryId = new SelectList(db.TicketCategory, "id", "CategoryName", ticketCategory.ParentCategoryId);
            return View(ticketCategory);
        }

        // GET: TicketCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategory.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            return View(ticketCategory);
        }

        // POST: TicketCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketCategory ticketCategory = db.TicketCategory.Find(id);
            db.TicketCategory.Remove(ticketCategory);
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
