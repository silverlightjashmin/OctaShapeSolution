using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OctaShape.Data;
using OctaShape.Common;
using OctaShapeSolution.Models;

namespace OctaShapeSolution.Areas.eTicketSystem.Controllers
{
    [AuthorizeChecker]
    public class CommentController : System.Web.Mvc.Controller
    {

        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();
        string FileName = "";
        // GET: Comment

        [HttpPost]
        public ActionResult AddComment(TicketComment ticketcomment, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                ticketcomment.TicketId = ticketcomment.id;
                ticketcomment.id = 0;
                ticketcomment.CommentBy = Session["User_Name"].ToString();
                ticketcomment.CommentDate =  DateTime.Now;

                db.TicketComment.Add(ticketcomment);
                db.SaveChanges();

                //for image
       
                var path = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        //for checking

                        if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                           || Path.GetExtension(file.FileName).ToLower() == ".png"
                            || Path.GetExtension(file.FileName).ToLower() == ".gif"
                            || Path.GetExtension(file.FileName).ToLower() == ".jpeg")

                        {

                            FileName = "ImageId_" + ticketcomment.TicketId.ToString() + ticketcomment.id.ToString() + Path.GetExtension(file.FileName).ToLower();

                            path = Path.Combine(Server.MapPath("~/img/Comment_image"), FileName);
                            file.SaveAs(path);
                            

                        }


                    }


                }
                else
                {
                    path = "";
                }

                TicketImage tc = new TicketImage();
                tc.TicketId = ticketcomment.TicketId;
                tc.TicketCommentId = ticketcomment.id;
                tc.Url = path;

                if(file!=null)
                {
                    tc.ImageOn = FileName;
                    
                }
                else
                {
                    tc.ImageOn = null;
                }
              


                db.TicketImage.Add(tc);
                db.SaveChanges();

                //send email to admin for new Comments

                string subject = "New Comments Has Been Posted By :" + Session["User_Name"].ToString();
                string Body = string.Format("Dear Admin,<BR/><br/>A New Comment on Ticket #{0} Has Been Posted By :{1}.<br/><br/> please click on the below link to View the Comments : <a href=\"{2}\" title=\"User Email Confirm\">{2}</a>", ticketcomment.TicketId, Session["User_Name"].ToString(), Url.Action("GetTicket", "Comment", new { id = ticketcomment.TicketId }, Request.Url.Scheme));

                var adminemaillist = db.AdminUserList().ToList();

                SendEmail se = new SendEmail();
                string messageto = "";

                foreach (var item in adminemaillist)
                {
                    messageto = item.Email;

                    se.SendEmails(subject, Body, messageto);
                }







                return RedirectToAction("GetTicket",new { id = ticketcomment.TicketId });

            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(); ;
            }
            

        }


        //get ticket
                public ActionResult GetTicket(int? id)
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

            
            var Comments = db.GetCommentImage().OrderByDescending(x => x.CommentDate).Where(x => x.TicketId == id).ToList();
            

            


           

            ViewBag.Comments = Comments;
       
            ViewBag.TicketDetails = ticket;
            ViewBag.AssignedBy = db.User.Find(ticket.AssignedBy).UserName;

            return View(); 
        }



    }
}