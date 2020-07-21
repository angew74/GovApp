using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp
{
    public class GovCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GovCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            // first remove the old claim
            var user = _httpContextAccessor.HttpContext.User;
            if (user != null)
            {      // replace the claims
                context.ReplacePrincipal(context.Principal);
                context.ShouldRenew = true;
            }
            else
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(
                    IdentityConstants.ApplicationScheme);
            }
            await Task.CompletedTask;
        }

        public override Task SigningIn(CookieSigningInContext context)
        {
            return base.SigningIn(context);
        }

        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);
        }
    }
}
