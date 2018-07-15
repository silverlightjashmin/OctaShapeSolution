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
using OctaShape.Common;
using OctaShapeSolution.Areas.CardInformationSystem.Models;

namespace OctaShapeSolution.Areas.CardInformationSystem.Controllers
{
    [AuthorizeChecker]
        
    public class Card_ActivateController : Controller
    {
        private OctaShape_Card_Entities db = new OctaShape_Card_Entities();

        // GET: CardInformationSystem/Card_Activate
        public ActionResult Index()
        {
            return View(db.Card_Activate.ToList());
        }

        // GET: CardInformationSystem/Card_Activate/Details/5
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

        // GET: CardInformationSystem/Card_Activate/Create
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       public ActionResult DownloadActivationRequest()
        {
            DateTime RequestStartDate = DateTime.Now.Date;
            DateTime RequestEndDate = DateTime.Now.Date;

            var RequestStartDatevar = db.Card_Activate.OrderByDescending(x => x.Activate_Start).FirstOrDefault();



            CardActivateDate d = new CardActivateDate();


            if (RequestStartDatevar == null)
            {
                d.StartDate = DateTime.Now.Date;
                d.EndDate = DateTime.Now.Date;
            }
            else
            {
                d.StartDate = RequestStartDatevar.Activate_End.Value.Date;
                d.EndDate = DateTime.Now.Date;


            }

            d.Card_ReceivedDetail = new List<Card_ReceivedDetails>();


            ViewBag.sdate = d.StartDate;



            return View(d);
        }


        [HttpPost]
        public ActionResult DownloadActivationRequest(CardActivateDate CardActivateDate)    
        {


            // var ExportData = db.GetActivationData(CardActivateDate.StartDate, CardActivateDate.EndDate, Session["User_Name"].ToString()).ToList();
            var exportdata = db.Card_ReceivedDetails.Where(x => x.Delivered_Date >= CardActivateDate.StartDate && x.Delivered_Date <= CardActivateDate.EndDate).ToList();



            CardActivateDate d = new CardActivateDate();

            d.StartDate = CardActivateDate.StartDate;
            d.EndDate = CardActivateDate.EndDate;
            d.Card_ReceivedDetail = exportdata;



            


            return View(d);



        }

        [HttpPost]
        public ActionResult DumpData(CardActivateDate Card_Activate)
        {

            var ExportData = db.GetActivationData(Card_Activate.StartDate, Card_Activate.EndDate, Session["User_Name"].ToString()).ToList();

            //call notepad trf code
            ExportToText ett = new ExportToText();

            //ToString("yyyy-MM-dd")

            ett.Export<Card_ReceivedDetails>(ExportData, Card_Activate.StartDate.ToString());
            return RedirectToAction("DownloadActivationRequest");
        }
    }
}
