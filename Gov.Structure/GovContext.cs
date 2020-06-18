using Gov.Core;
using Gov.Core.Entity;
using Gov.Core.Identity;
using Gov.Core.Interfaces;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Gov.Structure
{


    public class GovContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, Userclaims, ApplicationUserRole, Userlogins, Roleclaims, Usertokens>, IContext
    {

        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
      new DebugLoggerProvider()
});
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
                optionsBuilder.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging(true)
                     .UseMySql("server=localhost;user id=TOMAHAWK;password=Roberta4@;database=GOV", x => x.ServerVersion("8.0.13-mysql"));
            }
        }




        public override int SaveChanges()
        {
            if (Thread.CurrentPrincipal != null)
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
            else
            {
                return base.SaveChanges();
            }
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
        public DbSet<VoceMenu> VoceMenu { get; set; }
        public virtual DbSet<UserAudit> UserAudit { get; set; }
        public virtual DbSet<Roleclaims> Roleclaims { get; set; }
        public virtual new DbSet<ApplicationRole> Roles { get; set; }
        public virtual DbSet<Userclaims> Userclaims { get; set; }
        public virtual DbSet<Userlogins> Userlogins { get; set; }
        public new DbSet<ApplicationUserRole> UserRoles { get; set; }
        public new DbSet<ApplicationUser> Users { get; set; }

        public virtual DbSet<Usertokens> Usertokens { get; set; }
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
                entity.HasOne(d => d.Pagina)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.TipoConenuto)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
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
                entity.HasOne(d => d.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<VoceMenu>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Role)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany<UserAudit>().WithOne().HasForeignKey(uc => uc.IdUtente);
                entity.HasMany<Userclaims>().WithOne().HasForeignKey(uc => uc.UserId);
                entity.HasMany<Userlogins>().WithOne().HasForeignKey(ul => ul.UserId);
                entity.HasMany<Usertokens>().WithOne().HasForeignKey(ut => ut.UserId);
                entity.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId);
                //  entity.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId);
            });

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            modelBuilder.Entity<Userclaims>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<Userlogins>(b =>
            {

                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<Usertokens>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                b.Property(t => t.LoginProvider).HasMaxLength(256);
                b.Property(t => t.Name).HasMaxLength(256);

                // Maps to the AspNetUserTokens table
                b.ToTable("UserTokens");
            });
            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.HasOne(ur => ur.Role)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(ur => ur.RoleId)
             .IsRequired();
                b.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
                b.ToTable("UserRoles");
            });

            /*
            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
               b.HasKey(r => new { r.UserId, r.RoleId });
             /*   b.HasOne(ur => ur.Role)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

                b.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();*/
            /*    b.ToTable("UserRoles");
            });*/

            modelBuilder.Entity<Roleclaims>()
         .ToTable("RoleClaims");
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.HasMany(d => d.VociMenu)
                  .WithOne(d => d.Role)
                 .HasForeignKey(e => e.RoleId);
                entity.HasMany(d => d.Pagine)
                 .WithOne(d => d.Role)
                .HasForeignKey(e => e.RoleId);
                entity.HasMany(e => e.UserRoles)
               .WithOne(e => e.Role)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();
            });
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");

            var Roles = new ApplicationRole[] {
            new ApplicationRole
            {
                Id =1,
                Name = "admin",
                NormalizedName = "admin"
            },
            new ApplicationRole
            {
                Id =2,
                Name = "user",
                NormalizedName = "user"
            }};
            modelBuilder.Entity<ApplicationRole>().HasData(Roles);
            var hasher = new PasswordHasher<IdentityUser>();
            var passwordhash = hasher.HashPassword(null, "Robert4@");
            var Users = new ApplicationUser[]
       {
                 new ApplicationUser
            {

                Id =1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "agnew74@gmail.com",
                NormalizedEmail = "agnew74@gmail.com",
                EmailConfirmed = false,
                PasswordHash = passwordhash,
                SecurityStamp = string.Empty,
               CodiceFiscale = "RBRNCL74P16H501C",
               Cognome = "Admin",
               AccessFailedCount = 0,
               Nome= "Admin",
               LastModified = DateTime.Now,
               LockoutEnabled = false,
               PhoneNumber = null,
               PhoneNumberConfirmed = false,
               TwoFactorEnabled = false,
               Sesso = "Maschio"
            }
       };

            modelBuilder.Entity<ApplicationUser>().HasData(new
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "agnew74@gmail.com",
                NormalizedEmail = "agnew74@gmail.com",
                EmailConfirmed = false,
                PasswordHash = passwordhash,
                SecurityStamp = string.Empty,
                CodiceFiscale = "RBRNCL74P16H501C",
                Cognome = "Admin",
                Nome = "Admin",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PhoneNumber = "",
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = false,
                LastModified = DateTime.Now,
                Sesso = "Maschio"
            });
            var ApplicationUserRoles = new ApplicationUserRole[]
            {new ApplicationUserRole
            {
                UserId = Users.Single(x=>x.UserName== "admin").Id,
                RoleId = Roles.Single(x=>x.Name =="admin").Id
            },
            new ApplicationUserRole
            {
                UserId = 1,
                RoleId = 2
            }};
            modelBuilder.Entity<ApplicationUserRole>().HasData(ApplicationUserRoles);
            var Contenuti = new Contenuto[]{
            new Contenuto
            {
                Id = 1,
                ContentuoCard = "Da questa pagina è possibile registrare un nuovo Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            },
            new Contenuto
            {
                Id = 2,
                ContentuoCard = "person-plus-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            },
            new Contenuto
            {
                Id = 3,
                ContentuoCard = "/premier/inserimento",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            },
            new Contenuto
            {
                Id = 4,
                ContentuoCard = "Premier Inserimento",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 1
            },
            new Contenuto
            {
                Id = 5,
                ContentuoCard = "Da questa pagina è possibile modificare un Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            },
            new Contenuto
            {
                Id = 6,
                ContentuoCard = "person-check-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            },
            new Contenuto
            {
                Id = 7,
                ContentuoCard = "/premier/modifica",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            },
            new Contenuto
            {
                Id = 8,
                ContentuoCard = "Premier Modifica",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 2
            },
            new Contenuto
            {
                Id = 9,
                ContentuoCard = "Da questa pagina è possibile visualizzare i Premier",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            },
            new Contenuto
            {
                Id = 10,
                ContentuoCard = "people-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            },
            new Contenuto
            {
                Id = 11,
                ContentuoCard = "/premier/visualizza",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            },
            new Contenuto
            {
                Id = 12,
                ContentuoCard = "Premier Visualizzazione",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 3
            },
            new Contenuto
            {
                Id = 13,
                ContentuoCard = "Da questa pagina è possibile gestire gli Utenti",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 8
            },
            new Contenuto
            {
                Id = 14,
                ContentuoCard = "people-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 8
            },
            new Contenuto
            {
                Id = 15,
                ContentuoCard = "/account/manage",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 8
            },
            new Contenuto
            {
                Id = 16,
                ContentuoCard = "Gestione Utenti",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 8
            }, new Contenuto
            {
                Id = 17,
                ContentuoCard = "Da questa pagina è possibile registrare nuovi utenti",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 9
            },
            new Contenuto
            {
                Id = 18,
                ContentuoCard = "person-plus-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 9
            },
            new Contenuto
            {
                Id = 19,
                ContentuoCard = "/account/register",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 9
            },
            new Contenuto
            {
                Id = 20,
                ContentuoCard = "Registrazione Utente",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 9
            },new Contenuto
            {
                Id = 21,
                ContentuoCard = "Da questa pagina è possibile cambiare la password",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 13
            },
            new Contenuto
            {
                Id = 22,
                ContentuoCard = "gear-wide-connected",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 13
            },
            new Contenuto
            {
                Id = 23,
                ContentuoCard = "/account/changepassword",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 13
            },
            new Contenuto
            {
                Id = 24,
                ContentuoCard = "Cambio password",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 13
            },new Contenuto
            {
                Id = 25,
                ContentuoCard = "https://www.panoramasanita.it/wp-content/uploads/2019/05/roma.jpg",
                Tipo = "Image",
                TipoContenutoId = 5,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },new Contenuto
            {
                Id = 26,
                ContentuoCard = "Gestione Utenti",
                Tipo = "Titolo",
                TipoContenutoId = 6,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },new Contenuto{
                Id = 27,
                ContentuoCard = "https://res.cloudinary.com/hzekpb1cg/image/upload/c_fill,h_581,w_1185,f_auto/s3/public/prod/s3fs-public/Quartieri-di-Roma.jpg",
                Tipo = "Image",
                TipoContenutoId = 5,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },new Contenuto
            {
                Id = 28,
                ContentuoCard = "Gestione Premier",
                Tipo = "Titolo",
                TipoContenutoId = 6,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },new Contenuto{
                Id = 29,
                ContentuoCard = "https://roma.unicatt.it/ingresso-roma-992x560.jpg",
                Tipo = "Image",
                TipoContenutoId = 5,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },new Contenuto
            {
                Id = 30,
                ContentuoCard = "Gestione Governo",
                Tipo = "Titolo",
                TipoContenutoId = 6,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            }};
            modelBuilder.Entity<Contenuto>().HasData(Contenuti);
            var Pagine = new Pagina[] {
            new Pagina
            {
                Id = 1,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Inserimento Premier"
            },
            new Pagina
            {
                Id = 2,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Modifica Premier"
            },
            new Pagina
            {
                Id = 3,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Visualizzazione Premier"
            },
            new Pagina
            {
                Id = 4,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Indice Gestione Utenti"
            },
            new Pagina
            {
                Id = 5,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Inserimento Premier"
            },
            new Pagina
            {
                Id = 6,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Modifica Premier"
            },
            new Pagina
            {
                Id = 7,
                Codice = "Premier",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Visualizzazione Premier"
            },
            new Pagina
            {
                Id = 8,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Gestione Utenti"
            },
            new Pagina
            {
                Id = 9,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Registrazione Utenti"
            },
            new Pagina
            {
                Id = 10,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Mio Profilo"
            },
            new Pagina
            {
                Id = 11,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Mio Profilo"
            }, new Pagina
            {
                Id = 12,
                Codice = "Rights",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Gestione Abilitazioni"
            }, new Pagina{
                Id = 13,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Cambio Password"
            },new Pagina{
                Id = 14,
                Codice = "Home",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Home page"
            },new Pagina{
                Id = 15,
                Codice = "User",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Cambio Password"
            }};
            modelBuilder.Entity<Pagina>().HasData(Pagine);
            var Voci = new VoceMenu[] {
            new VoceMenu
            {
                Id = 1,
                Icona = "user-secret",
                Link = "/premier/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Premier"
            },
            new VoceMenu
            {
                Id = 2,
                Icona = "history",
                Link = "/governo/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Voce = "Governo",
                RoleId = Roles.Single(i => i.Name == "user").Id
            },
            new VoceMenu
            {
                Id = 3,
                Icona = "receipt",
                Link = "/dicastero/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Dicastero"
            },
            new VoceMenu
            {
                Id = 4,
                Icona = "university",
                Link = "/partito/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Voce = "Partito"
            },
            new VoceMenu
            {
                Id = 5,
                Icona = "user",
                Link = "/account/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Partito"
            },
            new VoceMenu
            {
                Id = 6,
                Icona = "user-secret",
                Link = "/premier/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Premier"
            },
            new VoceMenu
            {
                Id = 7,
                Icona = "history",
                Link = "/governo/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Voce = "Governo",
                RoleId = Roles.Single(i => i.Name == "admin").Id
            },
            new VoceMenu
            {
                Id = 8,
                Icona = "receipt",
                Link = "/dicastero/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Dicastero"
            },
            new VoceMenu
            {
                Id = 9,
                Icona = "university",
                Link = "/partito/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Partito"
            },
            new VoceMenu
            {
                Id = 10,
                Icona = "user",
                Link = "/account/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Account"
            },  new VoceMenu
            {
                Id = 11,
                Icona = "handshake",
                Link = "/rights/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Abilitazioni"
            }};
            modelBuilder.Entity<VoceMenu>().HasData(Voci);
            var TipiContenuto = new TipoContenuto[]
        {
            new TipoContenuto
            {
                Id = 1,
                Codice = "Testo",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null
            },
            new TipoContenuto
            {
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
            },
            new TipoContenuto
            {
                Id = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Codice = "Header"
            }, new TipoContenuto
            {
                Id = 5,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Codice = "Image"
            },new TipoContenuto
             {
                Id = 6,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Codice = "Titolo"
            }
        };
            modelBuilder.Entity<TipoContenuto>().HasData(TipiContenuto);
        }

        public IEnumerable<ValidationResult> GetValidationErrors()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            if (Thread.CurrentPrincipal != null)
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
                return base.SaveChangesAsync();
            }
            else
            {
                return base.SaveChangesAsync();
            }
        }
    }
}
