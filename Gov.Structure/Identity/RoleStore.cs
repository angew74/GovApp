
using Gov.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Structure.Identity
{
    public class RoleStore : IQueryableRoleStore<ApplicationRole>
    {

        private readonly GovContext dbcontext = null;
        private readonly string Storage = string.Empty;

        public IQueryable<ApplicationRole> Roles => GetAll().AsQueryable();


        public List<ApplicationRole> GetAll()
        {
            List<ApplicationRole> r = dbcontext.Roles.ToList();
            List<ApplicationRole> roles = r.Select(u => new ApplicationRole()
            {
                //do your variable mapping here                    
                Id = u.Id,
                Name = u.Name

            }).ToList();
            return roles;
        }

        public RoleStore(GovContext context)
        {
            this.dbcontext = context;

        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new Exception("Nome gruppo richiesto");

            if (dbcontext.Roles.Any(x => x.Name.ToUpper() == role.Name.ToUpper()))
                throw new Exception("Ruolo \"" + role.Name + "\" già esistenten");
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            ApplicationRole struttura = new ApplicationRole()
            {
                Name = role.Name.ToUpper()
            };
            await dbcontext.AddAsync(struttura);
            int rows = dbcontext.SaveChanges();
            if (rows > 0)
            {
                role.Id = struttura.Id;
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Impossibile registrare il ruolo {role.Name}." });
        }

      
        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

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

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(roleId))
                throw new Exception("Id ruolo obbligatorio");

            int id = 0;
            int.TryParse(roleId, out id);
            if (id == 0)
            {
                throw new Exception("Id ruolo deve essere numerico");
            }
            ApplicationRole r = await Task.Run(() => dbcontext.Roles.Where(x => x.Id == id).FirstOrDefault());
            ApplicationRole role = new ApplicationRole
            {
                Id = r.Id,
                Name = r.Name

            };
            return role;
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
                throw new Exception("Nome gruppo obbligatorio");

            ApplicationRole role = await Task.Run(() => dbcontext.Roles.Where(x => x.Name.ToUpper() == normalizedRoleName.ToUpper()).Include(i=>i.VociMenu).FirstOrDefault());
            return role;
        }

        public async Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
