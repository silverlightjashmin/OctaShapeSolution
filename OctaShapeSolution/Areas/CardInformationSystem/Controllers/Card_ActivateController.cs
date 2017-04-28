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



            Card_Activate d = new Card_Activate();


            if (RequestStartDatevar == null)
            {
                d.Activate_Start = DateTime.Now.Date;
                d.Activate_End = DateTime.Now.Date;
            }
            else
            {
                d.Activate_Start = RequestStartDatevar.Activate_Start.Value.Date;
                d.Activate_End = DateTime.Now.Date;


            }
            d.Card_ReceivedDetails = new List<Card_ReceivedDetails>();


            ViewBag.sdate = d.Activate_Start;



            return View(d);
        }


        [HttpPost]
        public ActionResult DownloadActivationRequest(Card_Activate Card_Activate)
        {
          

            var ExportData = db.GetActivationData(Card_Activate.Activate_Start, Card_Activate.Activate_End, Session["User_Name"].ToString()).ToList();


            Card_Activate d = new Card_Activate();

            d.Activate_Start = Card_Activate.Activate_Start;
            d.Activate_End = Card_Activate.Activate_End;
            d.Card_ReceivedDetails = ExportData;



            


            return View(d);



        }

        [HttpPost]
        public ActionResult DumpData(Card_Activate Card_Activate)
        {

            var ExportData = db.GetActivationData(Card_Activate.Activate_Start, Card_Activate.Activate_End, Session["User_Name"].ToString()).ToList();

            //call notepad trf code
            ExportToText ett = new ExportToText();

            //ToString("yyyy-MM-dd")

            ett.Export<Card_ReceivedDetails>(ExportData, Card_Activate.Activate_Start.ToString());
            return RedirectToAction("DownloadActivationRequest");
        }
    }
}
