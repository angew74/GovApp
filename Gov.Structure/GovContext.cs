using Gov.Core;
using Gov.Core.Entity;
using Gov.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<Pagina> Pagina { get; set; }
        public DbSet<Contenuto> Contenuto { get; set; }
        public DbSet<TipoContenuto> TipoContenuto { get; set; }      

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

            modelBuilder.Entity<Contenuto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ContentuoCard).IsRequired();              
            });
            modelBuilder.Entity<TipoContenuto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Codice).IsRequired();
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Codice).IsRequired();
                entity.Property(e => e.Denominazione).IsRequired();
                entity.HasMany(d => d.Contenuti);
            });

            modelBuilder.Entity<TipoContenuto>().HasData(new TipoContenuto
            {
                Id = 1,
                Codice = "Testo",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null
            },
            new TipoContenuto {
                Id = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Codice = "Icona"
            },
             new TipoContenuto
             {
                 Id = 3,
                 CreatedBy = "Caricamento",
                 CreatedDate = DateTime.Now,
                 UpdatedBy = null,
                 Codice = "Link"
             }, new TipoContenuto
             {
                 Id = 4,
                 CreatedBy = "Caricamento",
                 CreatedDate = DateTime.Now,
                 UpdatedBy = null,
                 Codice = "Header"
             });

            modelBuilder.Entity<Pagina>().HasData(new Pagina
            {
              Id=1,
              Codice = "Premier",  
              CreatedBy = "Caricamento",
              CreatedDate = DateTime.Now,
              UpdatedBy = null,           
              Denominazione = "Inserimento Premier"
            },
            new Pagina{
              Id = 2,
              Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Denominazione = "Modifica Premier"
            }, new Pagina
            {
                Id = 3,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Denominazione = "Visualizzazione Premier"
            });
            modelBuilder.Entity<Contenuto>().HasData(new Contenuto
            {
                Id=1,
                ContentuoCard = "Da questa pagina è possibile registrare un nuovo Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            }, new Contenuto
            {
                Id = 2,
                ContentuoCard = "user-secret",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            }, new Contenuto
            {
                Id = 3,
                ContentuoCard = "/premier/inserimento",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            }, new Contenuto
            {
                Id = 4,
                ContentuoCard = "Premier Inserimento",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            }, new Contenuto
            {
                Id = 5,
                ContentuoCard = "Da questa pagina è possibile modificare un Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            }, new Contenuto
            {
                Id = 6,
                ContentuoCard = "user-secret",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            }, new Contenuto
            {
                Id = 7,
                ContentuoCard = "/premier/modifica",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            }, new Contenuto{
                Id = 8,
                ContentuoCard = "Premier Modifica",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            }, new Contenuto
            {
                Id = 9,
                ContentuoCard = "Da questa pagina è possibile visualizzare i Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            }, new Contenuto
            {
                Id = 10,
                ContentuoCard = "user-secret",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            }, new Contenuto
            {
                Id = 11,
                ContentuoCard = "/premier/visualizza",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            }, new Contenuto
            {
                Id = 12,
                ContentuoCard = "Premier Visualizzazione",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            });
        }

        public IEnumerable<ValidationResult> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
