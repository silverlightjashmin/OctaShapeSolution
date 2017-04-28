using OctaShape.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace OctaShapeSolution.Areas.eTicketSystem.Models
{
    public class LayoutData
    {
        private OctaShape_eTicket_Entities db = new OctaShape_eTicket_Entities();
        public List<OctaShape.Data.GetPendingTicketsByUser_Result> GetData()
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            var Binding = db.GetPendingTicketsByUser(UserId).ToList();
            return Binding;

        }
       
    }
}