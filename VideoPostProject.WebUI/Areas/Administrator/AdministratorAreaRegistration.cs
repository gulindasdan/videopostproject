using System.Web.Mvc;

namespace VideoPostProject.WebUI.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
//            context.MapRoute(
//                "Login",
//                "Login/{action}/{id}",
//                  new { controller = "Login", action = "Logout", id = UrlParameter.Optional },
//                new string[] { "VideoPostProject.WebUI.Controllers" }
//);
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "VideoPostProject.WebUI.Areas.Administrator.Controllers" }

            );


        }

    }
}