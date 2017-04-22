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
    public class Card_ReceivedDetailsController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_ReceivedDetails
        public ActionResult Index()
        {
            var card_ReceivedDetails = db.Card_ReceivedDetails.Include(c => c.Card_Received);
            return View(card_ReceivedDetails.ToList());
        }

        // GET: CardInformationSystem/Card_ReceivedDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_ReceivedDetails card_ReceivedDetails = db.Card_ReceivedDetails.Find(id);
            if (card_ReceivedDetails == null)
            {
                return HttpNotFound();
            }
            return View(card_ReceivedDetails);
        }

        // GET: CardInformationSystem/Card_ReceivedDetails/Create
        public ActionResult Create()
        {
            ViewBag.Received_Id = new SelectList(db.Card_Received, "Received_Id", "Received_By");
            return View();
        }

        // POST: CardInformationSystem/Card_ReceivedDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Received_Id,Branch_Code,Account_No,Card_No,Expiry_Date,Delivered_Date")] Card_ReceivedDetails card_ReceivedDetails)
        {
            if (ModelState.IsValid)
            {
                db.Card_ReceivedDetails.Add(card_ReceivedDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Received_Id = new SelectList(db.Card_Received, "Received_Id", "Received_By", card_ReceivedDetails.Received_Id);
            return View(card_ReceivedDetails);
        }

        // GET: CardInformationSystem/Card_ReceivedDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_ReceivedDetails card_ReceivedDetails = db.Card_ReceivedDetails.Find(id);
            if (card_ReceivedDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Received_Id = new SelectList(db.Card_Received, "Received_Id", "Received_By", card_ReceivedDetails.Received_Id);
            return View(card_ReceivedDetails);
        }

        // POST: CardInformationSystem/Card_ReceivedDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Received_Id,Branch_Code,Account_No,Card_No,Expiry_Date,Delivered_Date")] Card_ReceivedDetails card_ReceivedDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_ReceivedDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Received_Id = new SelectList(db.Card_Received, "Received_Id", "Received_By", card_ReceivedDetails.Received_Id);
            return View(card_ReceivedDetails);
        }

        // GET: CardInformationSystem/Card_ReceivedDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_ReceivedDetails card_ReceivedDetails = db.Card_ReceivedDetails.Find(id);
            if (card_ReceivedDetails == null)
            {
                return HttpNotFound();
            }
            return View(card_ReceivedDetails);
        }

        // POST: CardInformationSystem/Card_ReceivedDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_ReceivedDetails card_ReceivedDetails = db.Card_ReceivedDetails.Find(id);
            db.Card_ReceivedDetails.Remove(card_ReceivedDetails);
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
