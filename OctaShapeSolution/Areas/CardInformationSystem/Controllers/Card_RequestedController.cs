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
            var card_Requested = db.Card_Requested.Include(c => c.Card_Received);
            return View(card_Requested.ToList());
        }

        // GET: CardInformationSystem/Card_Requested/Details/5
       

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
            ViewBag.Card_ReceivedId = new SelectList(db.Card_Received, "Received_Id", "Received_By", card_Requested.Card_ReceivedId);
            return View(card_Requested);
        }

        // POST: CardInformationSystem/Card_Requested/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Request_Id,Card_RequestDate,Request_StardDate,Request_EndDate,Total_CardsRequested,Request_By,Card_ReceivedId")] Card_Requested card_Requested)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_Requested).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Card_ReceivedId = new SelectList(db.Card_Received, "Received_Id", "Received_By", card_Requested.Card_ReceivedId);
            return View(card_Requested);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DownloadCardRequest()
        {
            return View();
        }
    }
}
