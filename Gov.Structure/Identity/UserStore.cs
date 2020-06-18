using Gov.Core.Enumerators;
using Gov.Core.Identity;
using Gov.Structure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class UserStore : IQueryableUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, ISecurityStampValidator, IUserRoleStore<ApplicationUser>

    {

        private readonly GovContext dbcontext = null;
        public IQueryable<ApplicationUser> Users => GetAll().AsQueryable();


        public UserStore(GovContext context)
        {
            this.dbcontext = context;

        }

        #region userstore 

        public List<ApplicationUser> GetAll()
        {
            List<ApplicationUser> utentis = dbcontext.Users.Include(i=>i.UserRoles).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
            return utentis;
        }

        public List<ApplicationUser> GetUsersBy(int take,int skip)
        {
            List<ApplicationUser> utentis = dbcontext.Users.Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
            return utentis;
        }

        public List<ApplicationUser> GetUsersByUsernameLike(string usernamepartial, int take,int skip)
        {
            List<ApplicationUser> utentis = dbcontext.Users.Where(x=>x.UserName.ToLower().Contains(usernamepartial)).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
            return utentis;
        }

        public List<ApplicationUser> GetUsersByMailLike(string mailpartial, int take, int skip)
        {
            List<ApplicationUser> utentis = dbcontext.Users.Where(x => x.NormalizedEmail.ToLower().Contains(mailpartial)).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
            return utentis;
        }

        public List<ApplicationUser> GetUsersByCognomeLike(string cognomepartial, int take, int skip)
        {
            List<ApplicationUser> utentis = dbcontext.Users.Where(x => x.Cognome.ToLower().Contains(cognomepartial)).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
            return utentis;
        }

        public int GetUsersCount()
        {
           return dbcontext.Users.Count();
        }

        public List<ApplicationUser> GetUsersSortingBy(int take, int skip, string sortBy, bool sortDesc, string filter, string[] types)
        {
            List<ApplicationUser> utentis = new List<ApplicationUser>();
            string ordining = "";
            if (string.IsNullOrEmpty(sortBy))
            { ordining = "UserName "; }
            else { ordining = sortBy; }
            if (sortDesc)
            { ordining += " DESC"; }
            else { ordining += " ASC"; }
            if (string.IsNullOrEmpty(filter))
            { utentis = dbcontext.Users.OrderBy(ordining).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList(); }
            else
            {
                foreach (string t in types)
                {
                    switch (t.ToLower())
                    {
                        case "username":
                            utentis = dbcontext.Users.Where(x => x.UserName.ToLower().Contains(filter)).OrderBy(ordining).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
                            break;
                        case "email":
                            utentis = dbcontext.Users.Where(x => x.Email.ToLower().Contains(filter)).OrderBy(ordining).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
                            break;
                        case "cognome":
                            utentis = dbcontext.Users.Where(x => x.Email.ToLower().Contains(filter)).OrderBy(ordining).Skip(skip).Take(take).Include(i => i.UserRoles).ThenInclude(i => i.Role).AsParallel().ToList();
                            break;

                    }
                }
            }
            return utentis;
        }

        public int GetUsersCountLike(string filter, string[] types)
        {
            int count = 0;
            if (types.Length > 0)
            {
                foreach (string t in types)
                {
                    switch (t.ToLower())
                    {
                        case "username":
                            count += dbcontext.Users.Where(x => x.UserName.ToLower().Contains(filter)).Count();
                            break;
                        case "email":
                            count += dbcontext.Users.Where(x => x.Email.ToLower().Contains(filter)).Count();
                            break;
                        case "cognome":
                            count += dbcontext.Users.Where(x => x.Cognome.ToLower().Contains(filter)).Count();
                            break;
                        default:                           
                            break;
                    }
                }
            }
            else
            {
                count += dbcontext.Users.Where(x => x.UserName.ToLower().Contains(filter) || x.Email.ToLower().Contains(filter) || x.Cognome.ToLower().Contains(filter)).Distinct().Count();
              
            }
            return count;
        }

        internal async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            SignInResultMock signInResult = new SignInResultMock();
            ApplicationUser u = await Task.Run(() => dbcontext.Users.Where(x => x.UserName.ToUpper() == user.UserName.ToUpper()).First());
            var hasher = new PasswordHasher<IdentityUser>();
            if (hasher.VerifyHashedPassword(null, u.PasswordHash, password) == PasswordVerificationResult.Success)
            { signInResult.Succeeded = true; }
            else { signInResult.Succeeded = false; }
            return await System.Threading.Tasks.Task.FromResult(signInResult);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (dbcontext.Users.Any(x => x.UserName.ToUpper() == user.UserName.ToUpper()))
                throw new Exception("Username \"" + user.UserName + "\" is already taken");
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var hasher = new PasswordHasher<IdentityUser>();
            string password = PasswordGenerator.Generate();
            string passwordhash = hasher.HashPassword(null, password);     
            await dbcontext.AddAsync(user);
            int rows = dbcontext.SaveChanges();
            if (rows > 0)
            {
                user.Password = password;
                user.PasswordHash = passwordhash;               
                ApplicationUserRole userRoles = new ApplicationUserRole()
                {
                    RoleId = (int)RolesTypes.User,
                    UserId = user.Id
                };
                await dbcontext.UserRoles.AddAsync(userRoles);
                int rowsu = dbcontext.SaveChanges();
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Impossibile registrare l'utente {user.Email}." });

        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            try
            {
                ApplicationUser s = await Task.Run(() => dbcontext.Users.Include(i => i.UserRoles).ThenInclude(i => i.Role).Where(x => x.Id == user.Id).FirstOrDefault());
                if(s.UserRoles.Where(x=>x.RoleId ==(int) RolesTypes.Administrator).Count() > 0)
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Impossibile cancellare l'utente {user.UserName}. Utente amministratore" });
                }
                dbcontext.Users.Remove(s);
                int rows = dbcontext.SaveChanges();
                if (rows > 0)
                {
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Impossibile cancellare l'utente {user.UserName}." });
                }
            }
            catch(Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Impossibile cancellare l'utente {user.UserName}." });
            }

        }

        public async Task<ApplicationUser> FindByIdAsync(String userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new Exception("User Id obbligatorio");

            int id = 0;
            int.TryParse(userId, out id);
            if (id == 0)
            {
                throw new Exception("User Id deve essere numerico");
            }
            ApplicationUser u = await Task.Run(() => dbcontext.Users.Include(i => i.UserRoles).ThenInclude(i => i.Role).Where(x => x.Id == id).FirstOrDefault());
            return u;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedUserName))
                throw new Exception("UserName  obbligatorio");

            var u = await Task.Run(() => dbcontext.Users.Include(i => i.UserRoles).ThenInclude(i => i.Role).Where(x => x.UserName == normalizedUserName).FirstOrDefault());           
            if (u == null)
            { return null; }             
            return u;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new Exception("user  obbligatorio");

            ApplicationUser u = await Task.Run(() => dbcontext.Users.Include(i => i.UserRoles).ThenInclude(i => i.Role).Where(x => x.Id == user.Id).First());
            if (u == null)
            { return null; }
            return user.Id.ToString();
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new Exception("user  obbligatorio");

            ApplicationUser u = await Task.Run(() => dbcontext.Users.Where(x => x.Id == user.Id).Include(i => i.UserRoles).ThenInclude(i => i.Role).First());
            if (u == null)
            { return null; }
            return user.UserName;
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);            
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser utente, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();
            if (utente == null) throw new ArgumentNullException(nameof(utente));
            ApplicationUser found = await Task.Run(() => dbcontext.Users.Where(u=>u.Id == utente.Id).FirstOrDefault());
            if (found == null)
                throw new Exception("Utente non trovato");
            cancellationToken.ThrowIfCancellationRequested();
            found.Email = utente.Email;
            found.LastModified = System.DateTime.Now;
            var hasher = new PasswordHasher<IdentityUser>();          
            found.PasswordHash = hasher.HashPassword(null, utente.Password);
            found.EmailConfirmed = utente.EmailConfirmed;
            found.LockoutEnabled = utente.LockoutEnabled;
            found.NormalizedEmail = utente.Email.ToLower();
            found.CodiceFiscale = utente.CodiceFiscale;
            found.Nome = utente.Nome;
            found.Cognome = utente.Cognome;
            found.Sesso = utente.Sesso.ToUpper();       
            dbcontext.Update(found);
            int rows = dbcontext.SaveChanges();
            if (rows > 0)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Impossibile aggiornare l'utente {utente.Email}." });
        }


        #endregion

        #region dispose 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbcontext?.Dispose();
            }
        }

        #endregion

        #region passwordstore
        public async Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            ApplicationUser found = await Task.Run(() => dbcontext.Users.Where(x => x.UserName.ToUpper() == user.UserName.ToUpper()).First());
            if (found == null)
                throw new Exception("Utente non trovato");
            return found.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            bool ok = ((await Task.Run(() => dbcontext.Users.Where(x => x.Id == user.Id))).FirstOrDefault().PasswordHash != null);
           return ok;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentNullException(nameof(passwordHash));
            ApplicationUser found = dbcontext.Users.Find(user.Id);
            if (found == null)
                throw new Exception("Utente non trovato");
            found.PasswordHash = passwordHash;
            found.LastModified = DateTime.Now;
            dbcontext.Users.Update(found);
            int rows = dbcontext.SaveChanges();
            if (rows == 1) { return new Task<bool>(() => true); }
            else { return new Task<bool>(() => false); }
        }
        #endregion


        #region usersrolesstore 
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var roles = (from ep in dbcontext.Roles
                         join e in dbcontext.UserRoles on ep.Id equals e.RoleId
                         join t in dbcontext.Users on e.UserId equals t.Id
                         where e.UserId == user.Id
                         select ep.NormalizedName);

            return await roles.ToListAsync();
        }

        private DateTime LastChangeUser(string user)
        {          
            if (user == null) throw new ArgumentNullException(nameof(user));
            var l = (from u in dbcontext.Users                        
                         where u.UserName.ToLower() == user.ToLower()
                         select u.LastModified);
            return l.FirstOrDefault();         
        }

        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            ApplicationUser found = dbcontext.Users.Find(user.Id);
            if (found == null)
                throw new Exception("Utente non trovato");
            var r = dbcontext.Roles.Where(a => a.Name.ToUpper() == roleName.ToUpper()).Single();
            if (r == null) throw new ArgumentNullException(nameof(r));
            ApplicationUserRole s = new ApplicationUserRole();
            s.UserId = user.Id;
            s.RoleId = r.Id;
            found.LastModified = System.DateTime.Now;
            dbcontext.UserRoles.AddAsync(s);
            dbcontext.Users.Update(found);
            return dbcontext.SaveChangesAsync();
        }

        public async Task<IList<string>> GetRolesAsync(string user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(user)) throw new ArgumentNullException(nameof(user));
            var id = dbcontext.Users.Where(x => x.UserName.ToUpper() == user.ToUpper()).FirstOrDefault().Id;
            var roles = (from ep in dbcontext.Roles
                         join e in dbcontext.UserRoles on ep.Id equals e.RoleId
                         join t in dbcontext.Users on e.UserId equals t.Id
                         where e.UserId== id
                         select ep.NormalizedName);

            return await roles.ToListAsync();
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (roleName == null) throw new ArgumentNullException(nameof(roleName));
            var users = (from ep in dbcontext.Users
                         join e in dbcontext.UserRoles on ep.Id equals e.UserId
                         join t in dbcontext.Roles on e.RoleId equals t.Id
                         where t.Name.ToUpper() == roleName.ToUpper()
                         select ep);
            return await users.ToListAsync();
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            ApplicationUser found = dbcontext.Users.Find(user.Id);
            if (found == null)
                throw new Exception("Utente non trovato");
            var r = dbcontext.Roles.Where(a => a.Name.ToUpper() == roleName.ToUpper()).Single();
            if (r == null) throw new Exception("ruolo non trovato");
            var role = (from ep in dbcontext.Roles
                        join e in dbcontext.UserRoles on ep.Id equals e.RoleId
                        join t in dbcontext.Users on e.UserId equals t.Id
                        where e.UserId == user.Id && ep.Name.ToUpper() == roleName.ToUpper()
                        select t).SingleOrDefault();
            if (role != null) { return new Task<bool>(() => true); }
            else { return new Task<bool>(() => false); }
        }


        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            ApplicationUser found = dbcontext.Users.Find(user.Id);
            found.LastModified = System.DateTime.Now;
            if (found == null)
                throw new Exception("utente non trovato");
            var r = dbcontext.Roles.Where(a => a.Name.ToUpper() == roleName.ToUpper()).Single();
            if (r == null) throw new Exception("ruolo non trovato");
            var role = (from ep in dbcontext.Roles
                        join e in dbcontext.UserRoles on ep.Id equals e.RoleId
                        join t in dbcontext.Users on e.UserId equals t.Id
                        where e.UserId == user.Id
                        select e).Single();
            dbcontext.UserRoles.Remove(role);
            dbcontext.Users.Update(found);
            return dbcontext.SaveChangesAsync();
        }

        #endregion

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken = default(CancellationToken))
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (user.SecurityStamp == null)
            {
                return Task.FromResult("AspNet.Identity.SecurityStamp");
            }
            return Task.FromResult(user.SecurityStamp);
        }

        public async Task ValidateAsync(CookieValidatePrincipalContext context)
        {                         
            DateTime lastChange= LastChangeUser(context.Principal.Identity.Name);
            var userPrincipal = context.Principal;

            // Look for the last changed claim.          
            DateTime cookieDate = DateTime.Parse(context.Principal.FindFirst("UpdatedOn").Value);
            if (lastChange == null || lastChange < cookieDate)
            {
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.RejectPrincipal();       
            }
            else
            {
                return;
            }
        }
    }
}
