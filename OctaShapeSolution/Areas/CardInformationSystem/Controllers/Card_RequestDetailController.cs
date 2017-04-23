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
    public class Card_RequestDetailController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();
        private OctaShapeSolutionEntities db1 = new OctaShapeSolutionEntities();

        // GET: CardInformationSystem/Card_RequestDetail
        public ActionResult Index()
        {
            var card_RequestDetail = db.Card_RequestDetail.Include(c => c.Card_Received).Include(c => c.Card_Requested).Include(c => c.Card_RequestType);
            return View(card_RequestDetail.ToList());
        }

        // GET: CardInformationSystem/Card_RequestDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestDetail card_RequestDetail = db.Card_RequestDetail.Find(id);
            if (card_RequestDetail == null)
            {
                return HttpNotFound();
            }
            return View(card_RequestDetail);
        }

        // GET: CardInformationSystem/Card_RequestDetail/Create
        public ActionResult Create()
        {
            ViewBag.BranchCode = new SelectList(db1.TicketBranch, "BranchCode", "BranchName");
            ViewBag.Request_Id = new SelectList(db.Card_RequestType, "Request_Id", "Request_Name");
            return View();
        }

        // POST: CardInformationSystem/Card_RequestDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Request_Date,Account_No,Customer_Name,EmbossName,Request_Id,Created_By,Approved_By,Card_RequestId,Card_ReceivedId,Branch_Code,Card_Number,Delivered_Date")] Card_RequestDetail card_RequestDetail)
        {
            if (ModelState.IsValid)
            {
                db.Card_RequestDetail.Add(card_RequestDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchCode = new SelectList(db1.TicketBranch, "BranchCode", "BranchName");

            ViewBag.Request_Id = new SelectList(db.Card_RequestType, "Request_Id", "Request_Name", card_RequestDetail.Request_Id);
            return View(card_RequestDetail);
        }

        // GET: CardInformationSystem/Card_RequestDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestDetail card_RequestDetail = db.Card_RequestDetail.Find(id);
            if (card_RequestDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Request_Id = new SelectList(db.Card_RequestType, "Request_Id", "Request_Name", card_RequestDetail.Request_Id);
            return View(card_RequestDetail);
        }

        // POST: CardInformationSystem/Card_RequestDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Request_Date,Account_No,Customer_Name,EmbossName,Request_Id,Created_By,Approved_By,Card_RequestId,Card_ReceivedId,Branch_Code,Card_Number,Delivered_Date")] Card_RequestDetail card_RequestDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_RequestDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          ViewBag.Request_Id = new SelectList(db.Card_RequestType, "Request_Id", "Request_Name", card_RequestDetail.Request_Id);
            return View(card_RequestDetail);
        }

        // GET: CardInformationSystem/Card_RequestDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_RequestDetail card_RequestDetail = db.Card_RequestDetail.Find(id);
            if (card_RequestDetail == null)
            {
                return HttpNotFound();
            }
            return View(card_RequestDetail);
        }

        // POST: CardInformationSystem/Card_RequestDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_RequestDetail card_RequestDetail = db.Card_RequestDetail.Find(id);
            db.Card_RequestDetail.Remove(card_RequestDetail);
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
