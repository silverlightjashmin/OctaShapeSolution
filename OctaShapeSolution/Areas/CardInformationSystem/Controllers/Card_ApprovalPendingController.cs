using OctaShape.Data;
using OctaShapeSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;


namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    [AuthorizeChecker]
    public class Card_ApprovalPendingController : Controller
    {

        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_ApprovalPending


        public ActionResult PendingApprovalList()
        {
            string branchcode = Session["Branch_Code"].ToString();
            var card_RequestDetail = db.Card_RequestDetail.Include(c => c.Card_Received).Include(c => c.Card_Requested).Include(c => c.Card_RequestType).Where(x => x.Approved_By == null && x.Branch_Code == branchcode);
            return View(card_RequestDetail.ToList());
            
         

        }

       [HttpGet]
        public ActionResult ApproveApplication(int? id,string Account_No )
        {
            db.ApproveCardApplication(id, Account_No, Session["User_Name"].ToString());
            db.SaveChanges();
                
            
             return RedirectToAction("PendingApprovalList");
        }
    }
}