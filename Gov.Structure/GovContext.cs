using Gov.Core;
using Gov.Core.Entity;
using Gov.Core.Entity.Elezioni;
using Gov.Core.Identity;
using Gov.Core.Interfaces;
using Gov.Structure.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using NLog.Extensions.Logging;
using Org.BouncyCastle.Asn1.Mozilla;
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

        public static readonly LoggerFactory MyLoggerFactory
  = new LoggerFactory(new[] { new NLogLoggerProvider() });
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _user;

        public GovContext(DbContextOptions<GovContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
        {

            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User.Identity.Name;
        }

        public GovContext(IHttpContextAccessor httpContextAccessor)
        {

            _user = httpContextAccessor.HttpContext.User.Identity.Name;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                     .UseLoggerFactory(MyLoggerFactory)
                    .EnableSensitiveDataLogging(true)
                     .UseMySql("server=localhost;user id=TOMAHAWK;password=Roberta4@;database=GOV", x => x.ServerVersion("8.0.13-mysql"));
            }
        }




        public override int SaveChanges()
        {


            if (!string.IsNullOrEmpty(_user))
            {
                var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.Entity is IAuditableEntity
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

                foreach (var entry in modifiedEntries)
                {
                    IAuditableEntity entity = entry.Entity as IAuditableEntity;
                    if (entity != null)
                    {
                        string identityName = _user;
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

        public virtual DbSet<Affluenze> Affluenze { get; set; }
        public virtual DbSet<AffluenzeStorico> AffluenzeStorico { get; set; }
        public virtual DbSet<AggregazioneInterrogazioni> AggregazioneInterrogazioni { get; set; }
        public virtual DbSet<Candidati> Candidati { get; set; }
        public virtual DbSet<Raggruppamento> Raggruppamenti { get; set; }
        public virtual DbSet<Elegen> Elegen { get; set; }
        public virtual DbSet<FaseElezione> FaseElezione { get; set; }
        public virtual DbSet<Iscritti> Iscritti { get; set; }
        public virtual DbSet<Liste> Liste { get; set; }
        public virtual DbSet<Matrice> Matrice { get; set; }
        public virtual DbSet<Municipi> Municipi { get; set; }
        public virtual DbSet<Plessi> Plessi { get; set; }
        public virtual DbSet<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual DbSet<RicalcoliAffluenza> RicalcoliAffluenza { get; set; }
        public virtual DbSet<RicalcoloAperturaCostituzione> RicalcoliAperturaCostituzione { get; set; }
        public virtual DbSet<RicalcoloPreferenze> RicalcoloPreferenze { get; set; }
        public virtual DbSet<RicalcoloVotiRaggruppamento> RicalcoloVotiRaggruppamento { get; set; }
        public virtual DbSet<RicalcoloVotiLista> RicalcoloVotiLista { get; set; }
        public virtual DbSet<RicalcoloVotiSindaco> RicalcoloVotiSindaco { get; set; }

        public virtual DbSet<Sezioni> Sezioni { get; set; }
        public virtual DbSet<Sindaci> Sindaci { get; set; }
        public virtual DbSet<TipoInterrogazione> TipoInterrogazione { get; set; }
        public virtual DbSet<TipoRicalcolo> TipoRicalcolo { get; set; }
        public virtual DbSet<TipoRicalcoloAggregazione> TipoRicalcoloAggregazione { get; set; }
        public virtual DbSet<Tipoelezione> Tipoelezione { get; set; }
        public virtual DbSet<Tiposezione> Tiposezione { get; set; }
        public virtual DbSet<UsersSezioni> UsersSezioni { get; set; }
        public virtual DbSet<VotiGenerali> VotiGenerali { get; set; }
        public virtual DbSet<VotiGeneraliStorico> VotiGeneraliStorico { get; set; }
        public virtual DbSet<VotiLista> VotiLista { get; set; }
        public virtual DbSet<VotiListaStorico> VotiListaStorico { get; set; }
        public virtual DbSet<VotiPeferenzeStorico> VotiPeferenzeStorico { get; set; }
        public virtual DbSet<VotiPreferenze> VotiPreferenze { get; set; }
        public virtual DbSet<VotiSindaco> VotiSindaco { get; set; }
        public virtual DbSet<VotiSindacoStorico> VotiSindacoStorico { get; set; }


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
                entity.HasMany<UsersSezioni>().WithOne().HasForeignKey(ur => ur.UserId);
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

            modelBuilder.Entity<Affluenze>(entity =>
            {
                entity.ToTable("affluenze");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Iscrittiid)
                    .HasName("fk_affluenze_iscritti_idx");

                entity.HasIndex(e => e.Plessoid)
                    .HasName("fk_affluenze_plessi_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_affluenza_sezioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_affluenze_tipo_elezione_idx");



                entity.HasOne(d => d.Iscritti)
                    .WithMany(p => p.Affluenze)
                    .HasForeignKey(d => d.Iscrittiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_affluenze_iscritti");

                entity.HasOne(d => d.Plesso)
                    .WithMany(p => p.Affluenze)
                    .HasForeignKey(d => d.Plessoid)
                    .HasConstraintName("fk_affluenze_plessi");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.Affluenze)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_affluenza_sezioni");
            });

            modelBuilder.Entity<AffluenzeStorico>(entity =>
            {


                entity.ToTable("affluenze_storico");

                entity.HasIndex(e => e.Iscrittiid)
                    .HasName("fk_affluenze_storico_iscritti_idx");

                entity.HasIndex(e => e.Plessoid)
                    .HasName("fk_affluenze_storico_plessi_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_affluenze_storico_sezioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_affluenze_storico_tipo_elezione_idx");

                entity.HasOne(d => d.Iscritti)
                    .WithMany()
                    .HasForeignKey(d => d.Iscrittiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_affluenze_storico_iscritti");

                entity.HasOne(d => d.Plesso)
                    .WithMany()
                    .HasForeignKey(d => d.Plessoid)
                    .HasConstraintName("fk_affluenze_storico_plessi");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany()
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_affluenze_storico_tipo_elezione");
            });

            modelBuilder.Entity<AggregazioneInterrogazioni>(entity =>
            {
                entity.ToTable("aggregazione_interrogazioni");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_aggregazione_interrogazioni_tipo_elezione_idx");

                entity.HasIndex(e => new { e.Descrizione, e.Tipoelezioneid, e.Codice })
                    .HasName("unique_aggregazione_interrogazioni")
                    .IsUnique();

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.AggregazioneInterrogazioni)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_aggregazione_interrogazioni_tipo_elezione");
            });

            modelBuilder.Entity<Candidati>(entity =>
            {
                entity.ToTable("candidati");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_candidati_lista_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_candidati_tipo_elezione_idx");

                entity.HasIndex(e => new { e.Progressivo, e.Listaid, e.Tipoelezioneid })
                    .HasName("uk_candidato_lista_progressivo")
                    .IsUnique();

                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.Candidati)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidati_lista");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.Candidati)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidati_tipo_elezione");
            });

            modelBuilder.Entity<Raggruppamento>(entity =>
            {
                entity.ToTable("Raggruppamento");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_tipoelezioni_coalizioni_idx");

                entity.HasIndex(e => e.Sindacoid)
                    .HasName("fk_sindaci_coalizioni_idx");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Raggruppamenti)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipoelezioni_coalizioni");

                entity.HasOne(d => d.Sindaco)
                    .WithMany(p => p.Raggruppamenti)
                    .HasForeignKey(d => d.Sindacoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sindaci_coalizioni");
            });

            modelBuilder.Entity<Elegen>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("elegen");

                entity.HasIndex(e => e.Id)
                    .HasName("idelegencam_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_elegen_tipo_elezione_idx");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Elegen)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .HasConstraintName("fk_elegen_tipo_elezione");
            });

            modelBuilder.Entity<FaseElezione>(entity =>
            {
                entity.ToTable("fase_elezione");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_fase_elezione_tipo_elezione_idx");

                entity.HasIndex(e => new { e.Codice, e.Idtipoelezione })
                    .HasName("uq_fase_elezioni_elezioni")
                    .IsUnique();

            });

            modelBuilder.Entity<Iscritti>(entity =>
            {
                entity.ToTable("iscritti");

                entity.HasIndex(e => e.Idsezione)
                    .HasName("idsezione_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_tipo_elezione_iscritti_idx");

                entity.HasIndex(e => e.Idtiposezione)
                    .HasName("fk_tiposezione_iscritti_idx");

                entity.HasOne(d => d.IdsezioneNavigation)
                    .WithOne(p => p.Iscritti)
                    .HasForeignKey<Iscritti>(d => d.Idsezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sezioni_iscritti");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Iscritti)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipo_elezione_iscritti");

                entity.HasOne(d => d.IdtiposezioneNavigation)
                    .WithMany(p => p.Iscritti)
                    .HasForeignKey(d => d.Idtiposezione)
                    .HasConstraintName("fk_tipo_sezione_iscritti");
            });

            modelBuilder.Entity<Liste>(entity =>
            {
                entity.ToTable("liste");

                entity.HasIndex(e => e.Coalizioneid)
                    .HasName("fk_liste_coalizioni_idx1");

                entity.HasIndex(e => e.ProgressivoCoalizione)
                    .HasName("fk_liste_coalizioni_idx");

                entity.HasIndex(e => e.Sindacoid)
                    .HasName("fk_liste_sindaci_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fl_liste_tipo_elezione_idx");


                entity.HasOne(d => d.Raggruppamenti)
                    .WithMany(p => p.Liste)
                    .HasForeignKey(d => d.Coalizioneid)
                    .HasConstraintName("fk_liste_coalizioni");

                entity.HasOne(d => d.Sindaco)
                    .WithMany(p => p.Liste)
                    .HasForeignKey(d => d.Sindacoid)
                    .HasConstraintName("fk_liste_sindaci");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.Liste)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fl_liste_tipo_elezione");
            });

            modelBuilder.Entity<Matrice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("matrice");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_matrice_tipo_elezione_idx");

                entity.HasIndex(e => e.Iscrittifemmine)
                    .HasName("iscrittifemmine_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Iscrittimaschi)
                    .HasName("iscrittimaschi_UNIQUE")
                    .IsUnique();

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Matrice)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_matrice_tipo_elezione");
            });

            modelBuilder.Entity<Municipi>(entity =>
            {
                entity.ToTable("municipi");

            });

            modelBuilder.Entity<Plessi>(entity =>
            {
                entity.ToTable("plessi");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_plessi_tipoelezione_idx");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Plessi)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .HasConstraintName("fk_plessi_tipoelezione");
            });

            modelBuilder.Entity<ProfiloVoti>(entity =>
            {
                entity.ToTable("profilo_voti");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_sezioni_idx");

                entity.HasIndex(e => e.Votigeneraliid)
                    .HasName("fk_voti_votigenerali_idx");

                entity.HasIndex(e => e.Votilistaid)
                    .HasName("fk_voti_votilista_idx");

                entity.HasIndex(e => e.Votisindacoid)
                    .HasName("fk_voti_votisindaco_idx");

                entity.HasIndex(e => new { e.Tipoelezioneid, e.Sezioneid })
                    .HasName("fk_voti_tipoelezione_idx");


                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.ProfiloVoti)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sezioni");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.ProfiloVoti)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_tipoelezione");

                entity.HasOne(d => d.Votigenerali)
                    .WithMany(p => p.ProfiloVoti)
                    .HasForeignKey(d => d.Votigeneraliid)
                    .HasConstraintName("fk_voti_votigenerali");

                entity.HasOne(d => d.Votilista)
                    .WithMany(p => p.ProfiloVoti)
                    .HasForeignKey(d => d.Votilistaid)
                    .HasConstraintName("fk_voti_votilista");

                entity.HasOne(d => d.Votisindaco)
                    .WithMany(p => p.ProfiloVoti)
                    .HasForeignKey(d => d.Votisindacoid)
                    .HasConstraintName("fk_voti_votisindaco");
            });

            modelBuilder.Entity<RicalcoliAffluenza>(entity =>
            {
                entity.ToTable("ricalcoli_affluenza");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_ricaloli_affluenza_tipo_elezione_idx");

                entity.HasIndex(e => e.Idtiporicalcolo)
                    .HasName("ricalcoli_affluenza_idx");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.RicalcoliAffluenza)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricaloli_affluenza_tipo_elezione");

                entity.HasOne(d => d.IdtiporicalcoloNavigation)
                    .WithMany(p => p.RicalcoliAffluenza)
                    .HasForeignKey(d => d.Idtiporicalcolo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcoli_affluenza_tipo_ricalcolo");
            });

            modelBuilder.Entity<RicalcoloAperturaCostituzione>(entity =>
            {
                entity.ToTable("ricalcoli_apertura_costituzione");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_ricalcoli_apertura_costituzione_tipo_elezione_idx");

                entity.HasIndex(e => e.Idtiporicalcolo)
                    .HasName("fk_ricalcoli_apertura_costituzione_tipo_ricalcolo_idx");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.RicalcoliAperturaCostituzione)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcoli_apertura_costituzione_tipo_elezione");

                entity.HasOne(d => d.IdtiporicalcoloNavigation)
                    .WithMany(p => p.RicalcoliAperturaCostituzione)
                    .HasForeignKey(d => d.Idtiporicalcolo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcoli_apertura_costituzione_tipo_ricalcolo");
            });

            modelBuilder.Entity<RicalcoloPreferenze>(entity =>
            {
                entity.ToTable("ricalcolo_preferenze");

                entity.HasIndex(e => e.Candidatoid)
                    .HasName("fk_ricalcolo_preferenze_candidato_idx");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_ricalcolo_preferenze_liste_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_ricalcolo_preferenze_tipo_elezione_idx");

                entity.HasIndex(e => e.Tiporicalcoloid)
                    .HasName("fk_ricalcolo_tipo_ricalcolo_idx");

                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.RicalcoloPreferenze)
                    .HasForeignKey(d => d.Candidatoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_preferenze_candidato");

                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.RicalcoloPreferenze)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_preferenze_liste");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.RicalcoloPreferenze)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_preferenze_tipo_elezione");

                entity.HasOne(d => d.Tiporicalcolo)
                    .WithMany(p => p.RicalcoloPreferenze)
                    .HasForeignKey(d => d.Tiporicalcoloid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_tipo_ricalcolo");
            });

            modelBuilder.Entity<RicalcoloVotiRaggruppamento>(entity =>
            {
                entity.ToTable("ricalcolo_voti_coalizioni");

                entity.HasIndex(e => e.Coalizioneid)
                    .HasName("fk_riacalcolo_coalizioni_coalizioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_ricalcolo_coalizioni_tipo_elezioni_idx");

                entity.HasIndex(e => e.Tiporicalcoloid)
                    .HasName("fk_ricalcolo_coalizioni_tipo_ricalcolo_idx");


                entity.HasOne(d => d.Raggruppamenti)
                    .WithMany(p => p.RicalcoloVotiCoalizioni)
                    .HasForeignKey(d => d.Coalizioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_riacalcolo_coalizioni_coalizioni");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.RicalcoloVotiCoalizioni)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_coalizioni_tipo_elezioni");

                entity.HasOne(d => d.Tiporicalcolo)
                    .WithMany(p => p.RicalcoloVotiCoalizioni)
                    .HasForeignKey(d => d.Tiporicalcoloid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_coalizioni_tipo_ricalcolo");
            });

            modelBuilder.Entity<RicalcoloVotiLista>(entity =>
            {
                entity.ToTable("ricalcolo_voti_lista");

                entity.HasIndex(e => e.Idlista)
                    .HasName("fk_ricalcolo_voti_lista_idx");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_ricalcolo_voti_tipo_elezione_idx");

                entity.HasIndex(e => e.Idtiporicalcolo)
                    .HasName("fk_ricalcolo_voti_tipo_ricalcolo_idx");

                entity.HasOne(d => d.IdlistaNavigation)
                    .WithMany(p => p.RicalcoloVotiLista)
                    .HasForeignKey(d => d.Idlista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_lista");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.RicalcoloVotiLista)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_tipo_elezione");

                entity.HasOne(d => d.IdtiporicalcoloNavigation)
                    .WithMany(p => p.RicalcoloVotiLista)
                    .HasForeignKey(d => d.Idtiporicalcolo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_tipo_ricalcolo");
            });

            modelBuilder.Entity<RicalcoloVotiSindaco>(entity =>
            {
                entity.ToTable("ricalcolo_voti_sindaco");

                entity.HasIndex(e => e.Sindacoid)
                    .HasName("fk_riacalcolo_sindaci_sindaci_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_ricalcolo_sindaci_tipo_elezioni_idx");

                entity.HasIndex(e => e.Tiporicalcoloid)
                    .HasName("fk_ricalcolo_sindaci_tipo_ricalcolo_idx");


                entity.HasOne(d => d.Sindaco)
                    .WithMany(p => p.RicalcoloVotiSindaco)
                    .HasForeignKey(d => d.Sindacoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_sindaco_sindaco");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.RicalcoloVotiSindaco)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_sindaco_tipo_elezione");

                entity.HasOne(d => d.Tiporicalcolo)
                    .WithMany(p => p.RicalcoloVotiSindaco)
                    .HasForeignKey(d => d.Tiporicalcoloid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ricalcolo_voti_sindaco_tipo_ricalcolo");
            });

            modelBuilder.Entity<Sezioni>(entity =>
            {
                entity.ToTable("sezioni");

                entity.HasIndex(e => e.Idplesso)
                    .HasName("fk_sezioni_plessi_idx");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_sezioni_tipoelezione_idx");

                entity.HasIndex(e => e.Idtiposezione)
                    .HasName("fk_sezioni_tiposezione_idx");

                entity.HasIndex(e => new { e.Numerosezione, e.Idtipoelezione })
                    .HasName("numerosezione_UNIQUE")
                    .IsUnique();

                entity.HasOne(d => d.IdplessoNavigation)
                    .WithMany(p => p.Sezioni)
                    .HasForeignKey(d => d.Idplesso)
                    .HasConstraintName("fk_sezioni_plessi");

                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.Sezioni)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .HasConstraintName("fk_sezioni_tipoelezione");

                entity.HasOne(d => d.IdtiposezioneNavigation)
                    .WithMany(p => p.Sezioni)
                    .HasForeignKey(d => d.Idtiposezione)
                    .HasConstraintName("fk_sezioni_tiposezione");
            });

            modelBuilder.Entity<Sindaci>(entity =>
            {
                entity.ToTable("sindaci");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_tipo_elezione_sindaci_idx");


                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.Sindaci)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipo_elezione_sindaci");
            });

            modelBuilder.Entity<TipoInterrogazione>(entity =>
            {
                entity.ToTable("tipo_interrogazione");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("tipo_interrogazione_tipo_elezione_idx");


                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.TipoInterrogazione)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_interrogazione_tipo_elezione");
            });

            modelBuilder.Entity<TipoRicalcolo>(entity =>
            {
                entity.ToTable("tipo_ricalcolo");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_tipo_ricalcolo_tipo_elezione_idx");


                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.TipoRicalcolo)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipo_ricalcolo_tipo_elezione");
            });

            modelBuilder.Entity<TipoRicalcoloAggregazione>(entity =>
            {
                entity.ToTable("tipo_ricalcolo_aggregazione");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("tipo_ricalcolo_aggregazione_tipo_elezione_idx");

                entity.HasIndex(e => new { e.Descrizione, e.Codice, e.Tipoelezioneid })
                    .HasName("tipo_ricalcolo_aggregazione_unique")
                    .IsUnique();



                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.TipoRicalcoloAggregazione)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_ricalcolo_aggregazione_tipo_elezione");
            });

            modelBuilder.Entity<Tipoelezione>(entity =>
            {
                entity.ToTable("tipoelezione");

                entity.HasIndex(e => e.Descrizione)
                    .HasName("descrizione_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("idtipoelezione_UNIQUE")
                    .IsUnique();


            });

            modelBuilder.Entity<Tiposezione>(entity =>
            {
                entity.ToTable("tiposezione");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

            });

            modelBuilder.Entity<UsersSezioni>(entity =>
            {
                entity.ToTable("users_sezioni");

                entity.HasIndex(e => e.Idtipoelezione)
                    .HasName("fk_users_sezione_tipoelezione_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_users_sezioni_sezioni_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("fk_users_sezioni_users_idx");


                entity.HasOne(d => d.IdtipoelezioneNavigation)
                    .WithMany(p => p.UsersSezioni)
                    .HasForeignKey(d => d.Idtipoelezione)
                    .HasConstraintName("fk_users_sezione_tipoelezione");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.UsersSezioni)
                    .HasForeignKey(d => d.Sezioneid)
                    .HasConstraintName("fk_users_sezioni_sezioni");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersSezioni)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_sezioni_users");
            });
            modelBuilder.Entity<VotiGenerali>(entity =>
            {
                entity.ToTable("voti_generali");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_sezione_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_tipoelezione_idx");


                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiGenerali)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_votigenerali_sezione");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.VotiGenerali)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_votigenerali_tipoelezione");
            });

            modelBuilder.Entity<VotiGeneraliStorico>(entity =>
            {

                entity.ToTable("voti_generali_storico");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_storico_sezione_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_storico_tipoelezione_idx");

                entity.HasOne(d => d.Sezione)
                    .WithMany()
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_storico_sezione");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany()
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_storico_tipoelezione");
            });

            modelBuilder.Entity<VotiLista>(entity =>
            {
                entity.ToTable("voti_lista");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_voti_lista_liste_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_lista_sezioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_lista_tipo_elezione_idx");

                entity.HasIndex(e => e.Votigeneraliid)
                    .HasName("fk_voti_lista_voti_generali_idx");


                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.VotiLista)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_lista_liste");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiLista)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_lista_sezioni");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.VotiLista)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_lista_tipo_elezione");

                entity.HasOne(d => d.Votigenerali)
                    .WithMany(p => p.VotiLista)
                    .HasForeignKey(d => d.Votigeneraliid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_lista_voti_generali");
            });

            modelBuilder.Entity<VotiListaStorico>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Listaid, e.Sezioneid, e.Tipoelezioneid, e.Voti, e.Dataoperazioneold, e.Utenteoperazioneold })
                    .HasName("PRIMARY");

                entity.ToTable("voti_lista_storico");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_liste_voti_lista_storico_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_sezioni_voti_lista_storico_idx");



                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.VotiListaStorico)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_liste_voti_lista_storico");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiListaStorico)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sezioni_voti_lista_storico");
            });

            modelBuilder.Entity<VotiPeferenzeStorico>(entity =>
            {
                entity.ToTable("voti_peferenze_storico");

                entity.HasIndex(e => e.Candidatoid)
                    .HasName("fk_voti_preferenze_storico_candidati_idx");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_voti_preferenze_storico_liste_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_preferenze_storico_sezioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_preferenze_storico_tipoelezione_id_idx");



                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.VotiPeferenzeStorico)
                    .HasForeignKey(d => d.Candidatoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_storico_candidati");

                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.VotiPeferenzeStorico)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_storico_liste");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiPeferenzeStorico)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_storico_sezioni");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.VotiPeferenzeStorico)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_storico_tipoelezione_id");
            });

            modelBuilder.Entity<VotiPreferenze>(entity =>
            {
                entity.ToTable("voti_preferenze");

                entity.HasIndex(e => e.Candidatoid)
                    .HasName("fk_voti_preferenze_candidati_idx");

                entity.HasIndex(e => e.Listaid)
                    .HasName("fk_voti_preferenze_liste_idx");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_preferenze_sezioni_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_preferenze_tipoelezione_id_idx");

                entity.HasIndex(e => new { e.Candidatoid, e.Sezioneid, e.Listaid, e.Tipoelezioneid })
                    .HasName("uk_voti_preferenze_lista_candidato_sezione")
                    .IsUnique();


                entity.HasOne(d => d.Candidato)
                    .WithMany(p => p.VotiPreferenze)
                    .HasForeignKey(d => d.Candidatoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_candidati");

                entity.HasOne(d => d.Lista)
                    .WithMany(p => p.VotiPreferenze)
                    .HasForeignKey(d => d.Listaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_liste");

                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiPreferenze)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_sezioni");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.VotiPreferenze)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_preferenze_tipoelezione_id");
            });

            modelBuilder.Entity<VotiSindaco>(entity =>
            {
                entity.ToTable("voti_sindaco");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_sindaco_sezioni_idx");

                entity.HasIndex(e => e.Sindacoid)
                    .HasName("fk_voti_sindaco_sindaco_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_sindaco_tipo_elezioni_idx");

                entity.HasIndex(e => e.Votigeneraliid)
                    .HasName("fk_voti_sindaco_voti_generali_idx");



                entity.HasOne(d => d.Sezione)
                    .WithMany(p => p.VotiSindaco)
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_sezioni");

                entity.HasOne(d => d.Sindaco)
                    .WithMany(p => p.VotiSindaco)
                    .HasForeignKey(d => d.Sindacoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_sindaco");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany(p => p.VotiSindaco)
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_tipo_elezioni");

                entity.HasOne(d => d.Votigenerali)
                    .WithMany(p => p.VotiSindaco)
                    .HasForeignKey(d => d.Votigeneraliid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_voti_generali");
            });

            modelBuilder.Entity<VotiSindacoStorico>(entity =>
            {

                entity.ToTable("voti_sindaco_storico");

                entity.HasIndex(e => e.Sezioneid)
                    .HasName("fk_voti_sindaco_old_sezioni_idx");

                entity.HasIndex(e => e.Sindacoid)
                    .HasName("fk_voti_sindaco_old_sindaco_idx");

                entity.HasIndex(e => e.Tipoelezioneid)
                    .HasName("fk_voti_sindaco_old_tipo_elezioni_idx");


                entity.HasOne(d => d.Sezione)
                    .WithMany()
                    .HasForeignKey(d => d.Sezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_old_sezioni");

                entity.HasOne(d => d.Sindaco)
                    .WithMany()
                    .HasForeignKey(d => d.Sindacoid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_old_sindaco");

                entity.HasOne(d => d.Tipoelezione)
                    .WithMany()
                    .HasForeignKey(d => d.Tipoelezioneid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_voti_sindaco_old_tipo_elezioni");
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
                ContentuoCard = "Da questa pagina è possibile registrare le affluenze",
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
                ContentuoCard = "/GovApp/affluenza/inserimento",
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
                ContentuoCard = "Affluenza Inserimento",
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
                ContentuoCard = "Da questa pagina è possibile modificare le affluenze",
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
                ContentuoCard = "/GovApp/affluenza/modifica",
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
                ContentuoCard = "Affluenze Modifica",
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
                ContentuoCard = "Da questa pagina è possibile visualizzare le affluenze",
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
                ContentuoCard = "/GovApp/affluenza/visualizza",
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
                ContentuoCard = "Affluenze Visualizzazione",
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
                ContentuoCard = "/GovApp/account/manage",
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
                ContentuoCard = "/GovApp/account/register",
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
                ContentuoCard = "/GovApp/account/changepassword",
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
                ContentuoCard = "Gestione Affluenze",
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
                ContentuoCard = "Gestione Interrogazioni",
                Tipo = "Titolo",
                TipoContenutoId = 6,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 14
            },
            new Contenuto
            {
                Id = 31,
                ContentuoCard = "Da questa pagina è possibile registrare i voti di coalizione",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 17
            },
            new Contenuto
            {
                Id = 32,
                ContentuoCard = "person-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 17
            },
            new Contenuto
            {
                Id = 33,
                ContentuoCard = "/GovApp/coalizioni/inserimento",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 17
            },
            new Contenuto
            {
                Id = 34,
                ContentuoCard = "Coalizioni Inserimento",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 17
            },
              new Contenuto
            {
                Id = 35,
                ContentuoCard = "Da questa pagina è possibile modificare i voti di coalizione",
                Tipo = "Testo",
                TipoContenutoId = 1,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 18
            },
            new Contenuto
            {
                Id = 36,
                ContentuoCard = "person-fill",
                Tipo = "Icona",
                TipoContenutoId = 2,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 18
            },
            new Contenuto
            {
                Id = 37,
                ContentuoCard = "/GovApp/coalizioni/modifica",
                Tipo = "Link",
                TipoContenutoId = 3,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 18
            },
            new Contenuto
            {
                Id = 39,
                ContentuoCard = "Coalizioni Modifica",
                Tipo = "Header",
                TipoContenutoId = 4,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                PaginaId = 18
            }
            };
            modelBuilder.Entity<Contenuto>().HasData(Contenuti);
            var Pagine = new Pagina[] {
            new Pagina
            {
                Id = 1,
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Inserimento Affluenze"
            },
            new Pagina
            {
                Id = 2,
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Modifica Affluenze"
            },
            new Pagina
            {
                Id = 3,
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Visualizzazione Affluenze"
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
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Inserimento Affluenze"
            },
            new Pagina
            {
                Id = 6,
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Modifica Affluenze"
            },
            new Pagina
            {
                Id = 7,
                Codice = "Affluenze",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Visualizzazione Affluenze"
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
            },new Pagina{
                Id = 16,
                Codice = "Sezione",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Gestione Sezione"
            },new Pagina{
                Id = 17,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Inserimento Coalizione"
            },new Pagina{
                Id = 18,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Modifica Coalizione"
            },new Pagina{
                Id = 19,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Inserimento Coalizione"
            },new Pagina{
                Id = 20,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Modifica Coalizione"
            },new Pagina{
                Id = 21,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "admin").Id,
                Denominazione = "Indice Coalizione"
            },new Pagina{
                Id = 22,
                Codice = "Voti",
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Denominazione = "Indice Coalizione"
            }};
            modelBuilder.Entity<Pagina>().HasData(Pagine);
            var Voci = new VoceMenu[] {
            new VoceMenu
            {
                Id = 1,
                Icona = "user-secret",
                Link = "/affluenze/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Affluenze"
            },
            new VoceMenu
            {
                Id = 2,
                Icona = "history",
                Link = "/GovApp/liste/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Voce = "Liste",
                RoleId = Roles.Single(i => i.Name == "user").Id
            },
            new VoceMenu
            {
                Id = 3,
                Icona = "receipt",
                Link = "/GovApp/sindaco/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Sindaco"
            },
            new VoceMenu
            {
                Id = 4,
                Icona = "university",
                Link = "/GovApp/interrogazioni/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId =Roles.Single(i => i.Name == "user").Id,
                Voce = "Interrogazioni"
            },
            new VoceMenu
            {
                Id = 5,
                Icona = "user",
                Link = "/GovApp/account/index",
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
                Link = "/GovApp/affluenze/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Affluenze"
            },
            new VoceMenu
            {
                Id = 7,
                Icona = "history",
                Link = "/GovApp/liste/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                Voce = "Liste",
                RoleId = Roles.Single(i => i.Name == "admin").Id
            },
            new VoceMenu
            {
                Id = 8,
                Icona = "receipt",
                Link = "/GovApp/sindaco/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Sindaco"
            },
            new VoceMenu
            {
                Id = 9,
                Icona = "university",
                Link = "/GovApp/interrogazioni/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Interrogazioni"
            },
            new VoceMenu
            {
                Id = 10,
                Icona = "user",
                Link = "/GovApp/account/index",
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
                Link = "/GovApp/rights/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Abilitazioni"
            }, new VoceMenu
            {
                Id = 12,
                Icona = "handshake",
                Link = "/GovApp/sezioni/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Sezioni"
            }, new VoceMenu
            {
                Id = 13,
                Icona = "user",
                Link = "/GovApp/Coalizione/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "admin").Id,
                Voce = "Voti Coalizione"
            }, new VoceMenu
            {
                Id = 14,
                Icona = "user",
                Link = "/GovApp/Coalizione/index",
                Active = true,
                CreatedBy = "Caricamento",
                CreatedDate = DateTime.Now,
                UpdatedBy = null,
                RoleId = Roles.Single(i => i.Name == "user").Id,
                Voce = "Voti Coalizione"
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
            if (!string.IsNullOrEmpty(_user))
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
