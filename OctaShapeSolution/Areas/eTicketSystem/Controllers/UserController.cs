using OctaShape.Common;
using OctaShape.Data;
using OctaShapeSolution.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class UserController : Controller
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();
        // GET: User

        public PartialViewResult CommonUser()   
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
                
                var ticketlist = db.Ticket.OrderByDescending(x => x.TicketPriorityId).Where(x => x.TicketStatusId == 1 && x.CreatedBy==UserId).ToList();

                var cname = RegionInfo.CurrentRegion.DisplayName;

                var totaltickets = db.GetTicketTotalByStatus().ToList();

                var pendingtickets = db.GetPendingTicketsByUser(UserId).ToList();

                //Ticket count by user

                var openticket = 1;//string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Open").FirstOrDefault().TicketCount.ToString());

                var resolvedticket = 1;// string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Resolved").FirstOrDefault().TicketCount.ToString());

                var closedticket = 1;// string.IsNullOrEmpty(db.GetTicketCountByUser(UserId).Where(x => x.TicketStatus == "Closed").FirstOrDefault().TicketCount.ToString());



                string hostname = Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.GetHostEntry(hostname);

                if (ipHostInfo.AddressList.Count() <= 2)
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
             

                ViewBag.UserDetails = UserDetails;
                
                ViewBag.TicketList = ticketlist;
                ViewBag.Date = DateTime.Now.Date.ToShortDateString();
                ViewBag.Time = DateTime.Now.ToString("HH:MM");
                ViewBag.Country = cname;
                ViewBag.TotalTickets = totaltickets;

                ViewBag.PendingTickets = pendingtickets;

                ViewBag.Ticketcount = pendingtickets.Count();

                ViewBag.OpenTicketCount = openticket;
                ViewBag.ReslolveTicketCount = resolvedticket;
                ViewBag.ClosedTicketCount = closedticket; ;

                return View();

            }

            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
       

        public ActionResult ViewMyTicket()
        {
            //id for ticket closed in ticketstatus table is 4
            int ticketclosed = 4;

            int userid = Convert.ToInt32(Session["User_Id"]);
            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Where(x => x.TicketStatusId != ticketclosed && x.CreatedBy == userid ).OrderByDescending(x=>x.CreatedDate
           
            );
            return View(ticket.ToList());

        }

        public ActionResult ViewTickets()
        {
            //id for ticket closed in ticketstatus table is 4
            int ticketclosed = 4;
            string branchcode = Session["Branch_Code"].ToString();
            var ticket = db.Ticket.Include(t => t.TicketBranch).Include(t => t.TicketCategory).Include(t => t.User).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Where(x => x.TicketStatusId != ticketclosed && x.BranchCode==branchcode).OrderByDescending(x => x.CreatedDate);
            return View(ticket.ToList());
        }

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
                string subject = "New Ticket Has Been Raised By :" + Session["User_Name"].ToString();
                string Body = string.Format("Dear Admin,<BR/><br/>A New Ticket Has Been Raised By :{0}.<br/><br/> please click on the below link to View the ticket : <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", name, Url.Action("GetTicket", "Comment", new { id = ticket.id }, Request.Url.Scheme));

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

                ViewBag.Message = "Ticket Issued Successfully";

                ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
                ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);

                return RedirectToAction("ViewTickets");
            }

            ViewBag.Message = "Ticket Couldn't Be Issued";
            ViewBag.Categoryid = new SelectList(db.TicketCategory, "id", "CategoryName", ticket.Categoryid);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriority, "id", "TicketPriority1", ticket.TicketPriorityId);
            return View(ticket);
        }

       


    }
}