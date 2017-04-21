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
    public class Card_RequestTypeController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_RequestType
        public ActionResult Index()
        {
            return View(db.Card_RequestType.ToList());
        }

        // GET: CardInformationSystem/Card_RequestType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestType card_RequestType = db.Card_RequestType.Find(id);
            if (card_RequestType == null)
            {
                return HttpNotFound();
            }
            return View(card_RequestType);
        }

        // GET: CardInformationSystem/Card_RequestType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardInformationSystem/Card_RequestType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Request_Id,Request_Name,Request_Charge")] Card_RequestType card_RequestType)
        {
            if (ModelState.IsValid)
            {
                db.Card_RequestType.Add(card_RequestType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card_RequestType);
        }

        // GET: CardInformationSystem/Card_RequestType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestType card_RequestType = db.Card_RequestType.Find(id);
            if (card_RequestType == null)
            {
                return HttpNotFound();
            }
            return View(card_RequestType);
        }

        // POST: CardInformationSystem/Card_RequestType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Request_Id,Request_Name,Request_Charge")] Card_RequestType card_RequestType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_RequestType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card_RequestType);
        }

        // GET: CardInformationSystem/Card_RequestType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestType card_RequestType = db.Card_RequestType.Find(id);
            if (card_RequestType == null)
            {
                return HttpNotFound();
            }
            return View(card_RequestType);
        }

        // POST: CardInformationSystem/Card_RequestType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_RequestType card_RequestType = db.Card_RequestType.Find(id);
            db.Card_RequestType.Remove(card_RequestType);
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
