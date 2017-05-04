using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShape.Common;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class AdminController : System.Web.Mvc.Controller
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();
     

        public ActionResult CreateTicket()

        {
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1");
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName");

            return View();
        }

        [HttpPost]
        public ActionResult CreateTicket(Ticket ticket)
        {
            ticket.CreatedBy = Convert.ToInt32(Session["User_Id"]);
            ticket.TicketStatusId = 1;
            ticket.EditEnable = true;
            ticket.BranchCode = Session["Branch_Code"].ToString();
            ticket.CreatedDate = DateTime.Now;

            string name = Session["User_Name"].ToString();


            if (ModelState.IsValid)
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();


                //send email to admin for new ticket raised
                string subject = "New Ticket Has Been Raised By : " + name;
                string Body = string.Format("Dear Admin,<BR/><br/>A New Ticket Has Been Raised By :{0}.<br/><br/> please click on the below link to View the ticket : <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>",name, Url.Action("GetTicket", "Comment", new { id = ticket.id }, Request.Url.Scheme));

                //try git
                var adminemaillist = db.AdminUserList().ToList();

                SendEmail se = new SendEmail();
                string messageto = "";

                foreach (var item in adminemaillist)
                {
                    messageto = item.Email;
                    
                    se.SendEmails(subject, Body, messageto);
                }
                

                //send email to client 
                 messageto = db.User.Find(ticket.CreatedBy).Email;
                 se.SendEmails(subject, Body, messageto);

               

                return RedirectToAction("ViewTicket","Admin");
            }

            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
           ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            return View(ticket);
        }

        


      

    


        public ActionResult ViewMyTicket()
        {
            //id for ticket closed in ticketstatus table is 4
            int ticketclosed = 4;

            int userid = Convert.ToInt32(Session["User_Id"]);
            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Where(x => x.TicketStatusId != ticketclosed && x.AssignedTo==userid);
            return View(ticket.ToList());
        }

        
        public ActionResult ViewTicket()
        {
            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Where(x=>x.TicketStatusId!=4);
            return View(ticket.ToList());
        }

        public ActionResult EditTicket(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ViewBag.AssignedBy = new SelectList(db.User, "id", "UserName", ticket.AssignedBy);

            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.AssignedTo = new SelectList(db.User, "id", "UserName", ticket.AssignedTo);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1", ticket.TicketStatusId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicket([Bind(Include = "id,Title,Description,TicketPriorityId,CreatedBy,CreatedDate,TicketStatusId,EditEnable,Categoryid,BranchCode,AssignedTo")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.LastEditedOn = DateTime.Now;

                ticket.AssignedBy = Convert.ToInt32(Session["User_Id"]);

                ticket.TicketStatusId = 2;

                ticket.AssignedTo = ticket.AssignedTo;

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();


                string createby = db.User.Find(ticket.CreatedBy).UserName;
                string Assigned = db.User.Find(ticket.AssignedTo).UserName;
               

                //send email to ticket issuer
                string subject = string.Format("Ticket Nr :{0} has been Assigned ", ticket.id);
                string Body = string.Format("Dear {0},<BR/><br/>Your Ticket has been Assigned To :{1}.<br/><br/> please click on the below link to View your ticket : <a href=\"{2}\" title=\"User Email Confirm\">{2}</a>",createby, Assigned, Url.Action("GetTicket", "Comment",new { id = ticket.id }, Request.Url.Scheme));
                string messageto = db.User.Find(ticket.CreatedBy).Email;
                SendEmail se = new SendEmail();
                se.SendEmails(subject, Body, messageto);

                //send notification to Assigned user
                Body = string.Format("Dear {0},<BR/><br/>New Ticket #{1} has been Assigned To You.<br/><br/> please click on the below link to View the ticket : <a href=\"{2}\" title=\"User Email Confirm\">{2}</a>",  Assigned, ticket.id, Url.Action("GetTicket", "Comment", new { id = ticket.id }, Request.Url.Scheme));
                messageto = db.User.Find(ticket.AssignedTo).Email;
              
                se.SendEmails(subject, Body, messageto);



                return RedirectToAction("ViewTicket");
            }

            ViewBag.AssignedBy = new SelectList(db.User, "id", "UserName", ticket.AssignedBy);
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.AssignedTo = new SelectList(db.User, "id", "UserName", ticket.AssignedTo);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "id", "TicketStatus1", ticket.TicketStatusId);
            return View(ticket);
        }


        public ActionResult GetTicketList()
        {
            int ticketnew = 1;
            //int ticketopen = 2;

            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Where(x => x.TicketStatusId== ticketnew && x.AssignedTo==null);
            return View(ticket.ToList());
            
        }

     
        public PartialViewResult CommonAdmin()
        {
            string UserName = Session["User_Name"].ToString();
            int UserId = Convert.ToInt32(Session["User_Id"]);

            var UserDetails = db.GetAllUserDetails(UserName).ToList();
            var pendingtickets = db.GetPendingTicketsByUser(UserId).ToList();

            ViewBag.UserDetails = UserDetails;
            ViewBag.PendingTickets = pendingtickets;
            ViewBag.Ticketcount = pendingtickets.Count();

            return PartialView();
        }
        public ActionResult DashBoard()
        {
            if (Session["User_Name"] != null)
            {
                string UserName = Session["User_Name"].ToString();
                int UserId = Convert.ToInt32(Session["User_Id"]);

                

            var UserDetails = db.GetAllUserDetails(UserName).ToList();

            var UserList = db.AdminUserList().ToList();

                //try to send in json format
               
               // var output = JsonConvert.SerializeObject(UserList);


                //try close

                var ticketlist = db.Ticket.OrderByDescending(x => x.TicketPriorityId).Where(x => x.TicketStatusId == 1).ToList();

            var cname = RegionInfo.CurrentRegion.DisplayName;

            var totaltickets = db.GetTicketTotalByStatus().ToList();

                var pendingtickets = db.GetPendingTicketsByUser(UserId).ToList();

                //Ticket count by user

                var openticket = 1;//string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Open").FirstOrDefault().TicketCount.ToString());

                var resolvedticket = 1;// string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Resolved").FirstOrDefault().TicketCount.ToString());

                var closedticket = 1;// string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Closed").FirstOrDefault().TicketCount.ToString());



                string hostname = Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);
                
                if (ipHostInfo.AddressList.Count()<=2)
                {
                    IPAddress hostip = ipHostInfo.AddressList[1];
                    ViewBag.HostIp = hostip.ToString();
                }
                else
                {
                    IPAddress hostip = ipHostInfo.AddressList[2];
                    ViewBag.HostIp = hostip.ToString();
                }
                

                ViewBag.HostName = hostname;
                //ViewBag.HostIp = hostip.ToString();

                ViewBag.UserDetails = UserDetails;
                ViewBag.UserList = UserList;
                ViewBag.TicketList = ticketlist;
                ViewBag.Date = DateTime.Now.Date.ToShortDateString();
                ViewBag.Time = DateTime.Now.ToString("HH:MM");
                ViewBag.Country = cname;
                ViewBag.TotalTickets = totaltickets;

                ViewBag.PendingTickets = pendingtickets;

                ViewBag.Ticketcount = pendingtickets.Count();

                ViewBag.OpenTicketCount =openticket ;
                ViewBag.ReslolveTicketCount =resolvedticket;
                ViewBag.ClosedTicketCount = closedticket;

            return View();

            }

            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

    }
}