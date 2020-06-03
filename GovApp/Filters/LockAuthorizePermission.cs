using Gov.Core.Identity;
using Gov.Structure;
using Gov.Structure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Filters
{
    public class LockAuthorizePermission : AuthorizeAttribute, IAuthorizationFilter
    {
              
       


        public void OnAuthorization(AuthorizationFilterContext context)
        {          

            var userName = context.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                GovContext DbContext = (GovContext)context.HttpContext.RequestServices.GetService(typeof(GovContext));

                var user = DbContext.Users.Where(m => m.UserName.ToLower() == userName).FirstOrDefault();
                if (user != null)
                {
                    if (user.EmailConfirmed == false)
                    {
                        context.Result = new RedirectToActionResult("accessdenied", "account", null);
                    }
                    if (user.LockoutEnabled)
                    {
                        context.Result = new RedirectToActionResult("accessdenied", "account", null);
                    }

                }
                else { context.Result = new RedirectToActionResult("accessdenied", "account", null); }
            }
            else
            {
               context.Result = new RedirectToActionResult("accessdenied", "account", null);
            }
            return;
        }
    }
}
