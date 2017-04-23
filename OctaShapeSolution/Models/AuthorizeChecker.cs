using OctaShape.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctaShapeSolution.Models
{
    public class AuthorizeChecker :AuthorizeAttribute

    {
       
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var User_Id = httpContext.Session["User_Id"];

           
                bool authorize = false;

            if (User_Id == null)
            {
                    authorize = true; /* return true if Entity has current user(active) with specific role */
                }
            
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
    }
