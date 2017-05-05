using System.Web.Mvc;

namespace OctaShapeSolution.Areas.IdeaBank
{
    public class IdeaBankAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "IdeaBank";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "IdeaBank_default",
                "IdeaBank/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}