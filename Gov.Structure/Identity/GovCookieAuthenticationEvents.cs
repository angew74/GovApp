
using Gov.Core.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class GovCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;



        public GovCookieAuthenticationEvents(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            // first remove the old claim
            var user = _userManager.FindByNameAsync(context.Principal.Identity.Name).Result;
            if (user != null && context.Principal.FindFirst("DateCreation") != null)
            {
                string v = context.Principal.FindFirst("DateCreation").Value;
                CultureInfo cultureInfo = new CultureInfo("it-IT");
                var dataCreation = DateTime.Parse(v,cultureInfo);
                if (user.LastModified > dataCreation)
                {
                    List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
                    foreach (string rr in roles)
                    {
                        var claim = context.Principal.FindFirst(rr);
                        ((ClaimsIdentity)context.Principal.Identity).RemoveClaim(claim);
                    }
                    // add the new claim

                    foreach (string r in roles)
                    {
                        Claim claimnew = new Claim(r, user.UserName);
                        ((ClaimsIdentity)context.Principal.Identity).AddClaim(claimnew);
                    }
                    Claim claimemail = new Claim(ClaimTypes.Email, user.Email);
                     ((ClaimsIdentity)context.Principal.Identity).AddClaim(claimemail);
                    // replace the claims
                    context.ReplacePrincipal(context.Principal);
                    context.ShouldRenew = true;
                }
            }
            return Task.CompletedTask;
        }
    }
}
