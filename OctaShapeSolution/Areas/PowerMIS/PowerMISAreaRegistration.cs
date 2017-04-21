using System.Web.Mvc;

namespace OctaShapeSolution.Areas.PowerMIS
{
    public class PowerMISAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PowerMIS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PowerMIS_default",
                "PowerMIS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}