using System.Web.Mvc;

namespace OctaShapeSolution.Areas.eTicketSystem
{
    public class eTicketSystemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "eTicketSystem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "eTicketSystem_default",
                "eTicketSystem/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}