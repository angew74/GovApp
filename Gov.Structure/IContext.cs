using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Structure
{
    public interface IContext
    {
        public interface IContext
        {
           
            IEnumerable<ValidationResult> GetValidationErrors();
            Int32 SaveChanges();
            Task<Int32> SaveChangesAsync();
            Task<Int32> SaveChangesAsync(CancellationToken cancellationToken);
            DbSet<TEntity> Set<TEntity>() where TEntity : class;
        } // IDbContext
    }
}
