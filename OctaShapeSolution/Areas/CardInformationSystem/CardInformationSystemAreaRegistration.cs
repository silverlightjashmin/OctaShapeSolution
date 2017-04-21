using System.Web.Mvc;

namespace OctaShapeSolution.Areas.CardInformationSystem
{
    public class CardInformationSystemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CardInformationSystem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CardInformationSystem_default",
                "CardInformationSystem/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}