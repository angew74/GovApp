using Gov.Core;
using Gov.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        IEnumerable<ValidationResult> GetValidationErrors();
        Int32 SaveChanges();
        Task<Int32> SaveChangesAsync();
        Task<Int32> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<Premier> Premier { get; set; }
        DbSet<Pagina> Pagina { get; set; }
        DbSet<Contenuto> Contenuto { get; set; }
    } // IDbContext

}
