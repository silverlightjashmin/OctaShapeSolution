﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctaShape.Data;

namespace OctaShapeSolution.Areas.CardInformationSystem
{
    public class Card_StockDetailController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_StockDetail
        public ActionResult Index()
        {
            return View(db.Card_StockDetail.ToList());
        }

        // GET: CardInformationSystem/Card_StockDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_StockDetail card_StockDetail = db.Card_StockDetail.Find(id);
            if (card_StockDetail == null)
            {
                return HttpNotFound();
            }
            return View(card_StockDetail);
        }

        // GET: CardInformationSystem/Card_StockDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardInformationSystem/Card_StockDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Stock_Inward,Stock_Outward")] Card_StockDetail card_StockDetail)
        {
            if (ModelState.IsValid)
            {
                db.Card_StockDetail.Add(card_StockDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card_StockDetail);
        }

        // GET: CardInformationSystem/Card_StockDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_StockDetail card_StockDetail = db.Card_StockDetail.Find(id);
            if (card_StockDetail == null)
            {
                return HttpNotFound();
            }
            return View(card_StockDetail);
        }

        // POST: CardInformationSystem/Card_StockDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Stock_Inward,Stock_Outward")] Card_StockDetail card_StockDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card_StockDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card_StockDetail);
        }

        // GET: CardInformationSystem/Card_StockDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card_StockDetail card_StockDetail = db.Card_StockDetail.Find(id);
            if (card_StockDetail == null)
            {
                return HttpNotFound();
            }
            return View(card_StockDetail);
        }

        // POST: CardInformationSystem/Card_StockDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card_StockDetail card_StockDetail = db.Card_StockDetail.Find(id);
            db.Card_StockDetail.Remove(card_StockDetail);
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