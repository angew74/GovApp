
using Gov.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class ApplicationUserManager : Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>
    {


        private readonly GovContext dbcontext = null;
        public ApplicationUserManager(Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>> logger)
              : base(
                  store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
                  logger)
        {
            
        }

               

        

        public async Task<int> GetUsersCounts()
        {
            return await new Task<int>(() => Users.Count());

        }
        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> utentis = dbcontext.Users.Include(i=>i.Roles).AsParallel().ToList();
            return utentis;
        }
        public async Task<ApplicationUser> FindByIdAsync(int id)
        {
            CancellationToken cancellationToken = new CancellationToken();
            return await this.Store.FindByIdAsync(id.ToString(), cancellationToken);
        }

        public List<ApplicationUser> GetAllPaging(int page, int pagesize)
        {
            List<ApplicationUser> utentis = dbcontext.Users.Skip(page * pagesize).Take(pagesize).AsParallel().ToList();           
            return utentis;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();           
            PasswordVerificationResult verificationResult = base.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {               
                return await Task.FromResult(IdentityResult.Failed(new IdentityError {Description = "Password attutale errata" }));               
            }
            user.Password = newPassword;
            return await base.UpdateAsync(user);                   
        }
    }
}
