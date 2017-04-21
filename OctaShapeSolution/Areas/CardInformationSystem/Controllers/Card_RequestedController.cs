using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctaShape.Data;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    public class Card_RequestedController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_Requested
        public ActionResult Index()
        {
            return View(db.Card_Requested.ToList());
        }

        // GET: CardInformationSystem/Card_Requested/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Requested card_Requested = db.Card_Requested.Find(id);
            if (card_Requested == null)
            {
                return HttpNotFound();
            }
            return View(card_Requested);
        }

        // GET: CardInformationSystem/Card_Requested/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardInformationSystem/Card_Requested/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Request_Id,Card_RequestDate,Request_StardDate,Request_EndDate,Total_Cards,Request_By,Is_Received")] Card_Requested card_Requested)
        {
            if (ModelState.IsValid)
            {
                db.Card_Requested.Add(card_Requested);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card_Requested);
        }

        // GET: CardInformationSystem/Card_Requested/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Requested card_Requested = db.Card_Requested.Find(id);
            if (card_Requested == null)
            {
                return HttpNotFound();
            }
            return View(card_Requested);
        }

        // POST: CardInformationSystem/Card_Requested/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Request_Id,Card_RequestDate,Request_StardDate,Request_EndDate,Total_Cards,Request_By,Is_Received")] Card_Requested card_Requested)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_Requested).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card_Requested);
        }

        // GET: CardInformationSystem/Card_Requested/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Requested card_Requested = db.Card_Requested.Find(id);
            if (card_Requested == null)
            {
                return HttpNotFound();
            }
            return View(card_Requested);
        }

        // POST: CardInformationSystem/Card_Requested/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_Requested card_Requested = db.Card_Requested.Find(id);
            db.Card_Requested.Remove(card_Requested);
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
