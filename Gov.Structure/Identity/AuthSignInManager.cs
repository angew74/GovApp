
using Gov.Core.Enumerators;
using Gov.Core.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class AuthSignInManager : SignInManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GovContext _db;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthSignInManager(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            GovContext dbContext,
            IAuthenticationSchemeProvider schemeProvider,
            IUserConfirmation<ApplicationUser> confirmation
            )
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, confirmation)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public override async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {


            //  var result = await _userPassword.AuthenticateAsync(user, password, );             
            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);          

            if (user != null) // We can only log an audit record if we can access the user object and it's ID
            {
                var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                UserAudit auditRecord = null;

                switch (result.ToString())
                {
                    case "Succeeded":
                        auditRecord = UserAudit.CreateAuditEvent(user.Id, UserAuditEventType.Login, ip);
                        break;

                    case "Failed":
                        auditRecord = UserAudit.CreateAuditEvent(user.Id, UserAuditEventType.FailedLogin, ip);
                        break;
                }

                if (auditRecord != null)
                {
                    _db.UserAudit.Add(auditRecord);
                    await _db.SaveChangesAsync();
                }
            }

            return result;
        }

        public override async Task SignOutAsync()
        {
            await base.SignOutAsync();
            if (!string.IsNullOrEmpty(_contextAccessor.HttpContext.User.Identity.Name))
            {
                var user = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name) as ApplicationUser;

                if (user != null)
                {
                    var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    var auditRecord = UserAudit.CreateAuditEvent(user.Id, UserAuditEventType.LogOut, ip);
                    _db.UserAudit.Add(auditRecord);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
