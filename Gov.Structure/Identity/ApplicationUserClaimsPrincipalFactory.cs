
using Gov.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class ApplicationUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>
 where TUser : ApplicationUser
 where TRole : ApplicationRole
    {
        private readonly UserManager<TUser> _userManager;
        public ApplicationUserClaimsPrincipalFactory(UserManager<TUser> userManager, RoleManager<TRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var id = await GenerateClaimsAsync(user);
            if (user != null)
            {
               List<string> roles = _userManager.GetRolesAsync(user).Result.ToList();
                foreach (string r in roles)
                {
                    id.AddClaim(new Claim(r, user.UserName));
                }
                id.AddClaim(new Claim("DateCreation", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            }
            return new ClaimsPrincipal(id);
        }
    }
}

