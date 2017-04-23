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
      

        // GET: CardInformationSystem/Card_ReceivedDetails/Create
      
        // POST: CardInformationSystem/Card_ReceivedDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

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
            return View(card_ReceivedDetails);
        }

        // GET: CardInformationSystem/Card_ReceivedDetails/Delete/5
       

        // POST: CardInformationSystem/Card_ReceivedDetails/Delete/5
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UploadReceivedCards()
        {

            // code here for records upload


            return View();
        }
    }
}
