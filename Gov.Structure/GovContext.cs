using Gov.Core;
using Gov.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace Gov.Structure
{
   
    public class GovContext : DbContext, IContext
    {
        public GovContext(DbContextOptions<GovContext> options)
           : base(options)
        {
        }

        public GovContext()
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging(true)
                     .UseMySql("server=localhost;user id=TOMAHAWK;password=Roberta4@;database=GOV", x => x.ServerVersion("8.0.13-mysql"));
            }
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Governo> Governo { get; set; }
        public DbSet<Premier> Premier { get; set; }
        public DbSet<Ministero> Ministero { get; set; }
        public DbSet<Ministro> Ministro { get; set; }
        public DbSet<Partito> Partito { get; set; }
        public DbSet<Legislatura> Legislatura { get; set; }
        public DbSet<Camera> Camera { get; set; }
        public DbSet<Senato> Senato { get; set; }
        public DbSet<Coalizione> Coalizione { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Governo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroMinisteri).IsRequired();
                entity.HasOne(d => d.Premier);
                entity.HasOne(d => d.Legislatura);
            });
            modelBuilder.Entity<Premier>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.HasMany(d => d.Militanze);
            });
            modelBuilder.Entity<Legislatura>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Numero).IsRequired();
                entity.HasOne(d => d.Camera);
                entity.HasOne(d => d.Senato);
            });
            modelBuilder.Entity<Ministero>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Denominazione).IsRequired();               
            });
            modelBuilder.Entity<Coalizione>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Denominazione).IsRequired();
            });
            modelBuilder.Entity<Ministro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Cognome).IsRequired();
                entity.HasMany(d => d.Militanze);              
            });
            modelBuilder.Entity<Dicastero>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataInizio).IsRequired();
                entity.Property(e => e.DataFine).IsRequired();
                entity.HasOne(d => d.Ministro);
                entity.HasOne(d => d.Ministero);
            });
            modelBuilder.Entity<Militanza>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataInizio).IsRequired();
                entity.Property(e => e.DataFine).IsRequired();
                entity.HasOne(d => d.Partito);               
            });
            modelBuilder.Entity<Camera>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataInizio).IsRequired();
                entity.Property(e => e.DataFine).IsRequired();
                entity.HasMany(d => d.Partiti);
            });
            modelBuilder.Entity<Senato>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataInizio).IsRequired();
                entity.Property(e => e.DataFine).IsRequired();
                entity.HasMany(d => d.Partiti);
            });
        }
    }
}
