using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.WebUI.Models
{
    public class AdminAuthentication : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["oturum"] != null)
            {
                User gelen = (User)httpContext.Session["oturum"];
                if (gelen.isAdministrator)
                {
                    return true;
                }
                else
                {
                    httpContext.Response.Redirect("/Home/Index");
                    return false;
                }
            }
            else
            {
                httpContext.Response.Redirect("/Login/login");
                return false;
            }
        }
    }
}