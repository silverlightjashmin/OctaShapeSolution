using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    [AuthorizeChecker]
    public class Card_ReceivedController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_Received
        public ActionResult Index()
        {
            var card_Received = db.Card_Received.Include(c => c.Card_ReceivedDetails);
            return View(card_Received.ToList());
        }

        // GET: CardInformationSystem/Card_Received/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Received card_Received = db.Card_Received.Find(id);
            if (card_Received == null)
            {
                return HttpNotFound();
            }
            return View(card_Received);
        }

        // GET: CardInformationSystem/Card_Received/Create
        public ActionResult Create()
        {
            ViewBag.Received_Id = new SelectList(db.Card_ReceivedDetails, "Received_Id", "Branch_Code");
            return View();
        }

        // POST: CardInformationSystem/Card_Received/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Received_Id,Received_Date,Total_CardsReceived,Received_By,IsPin_Received")] Card_Received card_Received)
        {
            if (ModelState.IsValid)
            {
                db.Card_Received.Add(card_Received);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Received_Id = new SelectList(db.Card_ReceivedDetails, "Received_Id", "Branch_Code", card_Received.Received_Id);
            return View(card_Received);
        }

        // GET: CardInformationSystem/Card_Received/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Received card_Received = db.Card_Received.Find(id);
            if (card_Received == null)
            {
                return HttpNotFound();
            }
            ViewBag.Received_Id = new SelectList(db.Card_ReceivedDetails, "Received_Id", "Branch_Code", card_Received.Received_Id);
            return View(card_Received);
        }

        // POST: CardInformationSystem/Card_Received/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Received_Id,Received_Date,Total_CardsReceived,Received_By,IsPin_Received")] Card_Received card_Received)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_Received).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Received_Id = new SelectList(db.Card_ReceivedDetails, "Received_Id", "Branch_Code", card_Received.Received_Id);
            return View(card_Received);
        }

        // GET: CardInformationSystem/Card_Received/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_Received card_Received = db.Card_Received.Find(id);
            if (card_Received == null)
            {
                return HttpNotFound();
            }
            return View(card_Received);
        }

        // POST: CardInformationSystem/Card_Received/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_Received card_Received = db.Card_Received.Find(id);
            db.Card_Received.Remove(card_Received);
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
