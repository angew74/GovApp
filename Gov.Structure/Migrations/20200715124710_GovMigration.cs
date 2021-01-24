using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gov.Structure.Migrations
{
    public partial class GovMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camera",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DataInizio = table.Column<DateTime>(nullable: false),
                    DataFine = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coalizione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Denominazione = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coalizione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fase_elezione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Codice = table.Column<string>(nullable: false),
                    Descrizione = table.Column<string>(nullable: false),
                    Abilitata = table.Column<int>(nullable: false),
                    Idtipoelezione = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fase_elezione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Denominazione = table.Column<string>(maxLength: 256, nullable: false),
                    DataIstituzione = table.Column<DateTime>(nullable: false),
                    DataCessazione = table.Column<DateTime>(nullable: false),
                    IsSenzaPortafoglio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ministro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Cognome = table.Column<string>(maxLength: 256, nullable: false),
                    DataNascita = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "municipi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Municipio = table.Column<int>(nullable: false),
                    Descrizione = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Cognome = table.Column<string>(maxLength: 256, nullable: false),
                    DataNascita = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Senato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DataInizio = table.Column<DateTime>(nullable: false),
                    DataFine = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoContenuto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Codice = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContenuto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipoelezione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                   
                    Descrizione = table.Column<string>(nullable: false),
                    Dataelezione = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoelezione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tiposezione",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int(10) unsigned", nullable: false),                   
                    Descrizione = table.Column<string>(nullable: false),
                    Codicesezione = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tiposezione", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    CustomTag = table.Column<string>(nullable: true),
                    CodiceFiscale = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Cognome = table.Column<string>(nullable: true),
                    Sesso = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LockoutEnd = table.Column<DateTime>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Denominazione = table.Column<string>(maxLength: 256, nullable: false),
                    Codice = table.Column<string>(maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagina_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: false),
                    ClaimValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoceMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Link = table.Column<string>(maxLength: 256, nullable: false),
                    Icona = table.Column<string>(maxLength: 50, nullable: false),
                    Voce = table.Column<string>(maxLength: 100, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoceMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoceMenu_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Legislatura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    DataInizio = table.Column<DateTime>(nullable: false),
                    DataFine = table.Column<DateTime>(nullable: false),
                    SenatoId = table.Column<int>(nullable: true),
                    CameraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legislatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Legislatura_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Legislatura_Senato_SenatoId",
                        column: x => x.SenatoId,
                        principalTable: "Senato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partito",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Nome = table.Column<string>(maxLength: 256, nullable: true),
                    CoalizioneId = table.Column<int>(nullable: true),
                    CameraId = table.Column<int>(nullable: true),
                    SenatoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partito_Camera_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Camera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partito_Coalizione_CoalizioneId",
                        column: x => x.CoalizioneId,
                        principalTable: "Coalizione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partito_Senato_SenatoId",
                        column: x => x.SenatoId,
                        principalTable: "Senato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "aggregazione_interrogazioni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Codice = table.Column<string>(nullable: false),
                    Descrizione = table.Column<string>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aggregazione_interrogazioni", x => x.Id);
                    table.ForeignKey(
                        name: "fk_aggregazione_interrogazioni_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "elegen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Numerosezioni = table.Column<int>(nullable: false),
                    Numeroliste = table.Column<int>(nullable: true),
                    Annoelezione = table.Column<int>(nullable: false),
                    Giornocostituzione = table.Column<string>(nullable: false),
                    Giornovotazione1 = table.Column<string>(nullable: false),
                    Giornovotazione2 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_elegen_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                              
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: true),
                    Collegiocamera = table.Column<int>(nullable: true),
                    Collegiosenato = table.Column<int>(nullable: true),
                    Collegioprovinciale = table.Column<int>(nullable: true),
                    Numerosezioni = table.Column<int>(nullable: true),
                    Iscrittimaschi = table.Column<int>(nullable: true),
                    Iscrittifemmine = table.Column<int>(nullable: true),
                    Iscrittitotali = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "fk_matrice_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "plessi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Descrizione = table.Column<string>(nullable: false),
                    Ubicazione = table.Column<string>(nullable: false),
                    Municipio = table.Column<int>(nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plessi", x => x.Id);
                    table.ForeignKey(
                        name: "fk_plessi_tipoelezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sindaci",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                   
                    Nome = table.Column<string>(nullable: false),
                    Cognome = table.Column<string>(nullable: false),
                    Sesso = table.Column<string>(nullable: false),
                    Progressivo = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sindaci", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tipo_elezione_sindaci",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tipo_interrogazione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Descrizione = table.Column<string>(nullable: false),
                    CodiceFase = table.Column<string>(nullable: false),
                    Codice = table.Column<string>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_interrogazione", x => x.Id);
                    table.ForeignKey(
                        name: "tipo_interrogazione_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tipo_ricalcolo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                   
                    Descrizione = table.Column<string>(nullable: false),
                    CodiceFase = table.Column<string>(nullable: false),
                    Codice = table.Column<string>(nullable: false),
                    Idtipoelezione = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_ricalcolo", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tipo_ricalcolo_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tipo_ricalcolo_aggregazione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Descrizione = table.Column<string>(nullable: false),
                    Codice = table.Column<string>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_ricalcolo_aggregazione", x => x.Id);
                    table.ForeignKey(
                        name: "tipo_ricalcolo_aggregazione_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAudit",
                columns: table => new
                {
                    UserAuditId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdUtente = table.Column<int>(nullable: false),
                    AuditEvent = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudit", x => x.UserAuditId);
                    table.ForeignKey(
                        name: "FK_UserAudit_Users_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: false),
                    ClaimValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contenuto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ContentuoCard = table.Column<string>(maxLength: 256, nullable: false),
                    Tipo = table.Column<string>(maxLength: 10, nullable: true),
                    PaginaId = table.Column<int>(nullable: false),
                    TipoContenutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contenuto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contenuto_Pagina_PaginaId",
                        column: x => x.PaginaId,
                        principalTable: "Pagina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contenuto_TipoContenuto_TipoContenutoId",
                        column: x => x.TipoContenutoId,
                        principalTable: "TipoContenuto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Governo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    PremierId = table.Column<int>(nullable: true),
                    LegislaturaId = table.Column<int>(nullable: true),
                    NumeroMinisteri = table.Column<int>(nullable: false),
                    NumeroMinisteriSenzaPortafogio = table.Column<int>(nullable: false),
                    DataIncarico = table.Column<DateTime>(nullable: true),
                    NumeroVotiSenato = table.Column<int>(nullable: false),
                    NumeroVotiCamera = table.Column<int>(nullable: false),
                    DataFiducia = table.Column<DateTime>(nullable: false),
                    IsFiducia = table.Column<bool>(nullable: false),
                    DataRevoca = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Governo_Legislatura_LegislaturaId",
                        column: x => x.LegislaturaId,
                        principalTable: "Legislatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Governo_Premier_PremierId",
                        column: x => x.PremierId,
                        principalTable: "Premier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Militanza",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    DataInizio = table.Column<DateTime>(nullable: false),
                    DataFine = table.Column<DateTime>(nullable: false),
                    PartitoId = table.Column<int>(nullable: true),
                    MinistroId = table.Column<int>(nullable: true),
                    PremierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Militanza", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Militanza_Ministro_MinistroId",
                        column: x => x.MinistroId,
                        principalTable: "Ministro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Militanza_Partito_PartitoId",
                        column: x => x.PartitoId,
                        principalTable: "Partito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Militanza_Premier_PremierId",
                        column: x => x.PremierId,
                        principalTable: "Premier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sezioni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                 
                    Numerosezione = table.Column<int>(nullable: false),
                    Idtiposezione = table.Column<uint>(nullable: true),
                    Idplesso = table.Column<int>(nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: true),
                    Municipio = table.Column<int>(nullable: true),
                    Cabina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sezioni", x => x.Id);
                    table.ForeignKey(
                        name: "fk_sezioni_plessi",
                        column: x => x.Idplesso,
                        principalTable: "plessi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sezioni_tipoelezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sezioni_tiposezione",
                        column: x => x.Idtiposezione,
                        principalTable: "tiposezione",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Raggruppamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Denominazione = table.Column<string>(nullable: false),
                    DenominazioneBreve = table.Column<string>(nullable: false),
                    Sindacoid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raggruppamento", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tipoelezioni_coalizioni",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sindaci_coalizioni",
                        column: x => x.Sindacoid,
                        principalTable: "sindaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcoli_affluenza",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Idtiporicalcolo = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    AffluenzaTotale = table.Column<int>(nullable: false),
                    IscrittiTotale = table.Column<int>(nullable: true),
                    PercentualeTotale = table.Column<string>(nullable: false),
                    AffluenzaMaschi = table.Column<int>(nullable: false),
                    IscrittiMaschi = table.Column<int>(nullable: false),
                    PercentualeMaschi = table.Column<string>(nullable: false),
                    AffluenzaFemmine = table.Column<int>(nullable: false),
                    IscrittiFemmine = table.Column<int>(nullable: false),
                    PercentualeFemmine = table.Column<string>(nullable: false),
                    NumeroAffluenza = table.Column<int>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: true),
                    TotaleSezioni = table.Column<int>(nullable: false),
                    PercentualeSezioniPervenute = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcoli_affluenza", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ricaloli_affluenza_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcoli_affluenza_tipo_ricalcolo",
                        column: x => x.Idtiporicalcolo,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcoli_apertura_costituzione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Idtiporicalcolo = table.Column<int>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: false),
                    NumeroCostituite = table.Column<int>(nullable: false),
                    PercentualeCostituite = table.Column<string>(nullable: false),
                    NumeroAperte = table.Column<int>(nullable: false),
                    PercentualeAperte = table.Column<string>(nullable: false),
                    IscrittiTotali = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcoli_apertura_costituzione", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ricalcoli_apertura_costituzione_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcoli_apertura_costituzione_tipo_ricalcolo",
                        column: x => x.Idtiporicalcolo,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcolo_voti_sindaco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Tiporicalcoloid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Sindacoid = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    NumeroVotiSoloSindaco = table.Column<int>(nullable: false),
                    PercentualeVoti = table.Column<string>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: false),
                    TotaleSezioni = table.Column<int>(nullable: false),
                    PercentualeSezioniPervenute = table.Column<string>(nullable: false),
                    IscrittiPervenute = table.Column<int>(nullable: false),
                    IscrittiTotale = table.Column<int>(nullable: false),
                    VotantiPervenute = table.Column<int>(nullable: false),
                    VotantiTotale = table.Column<int>(nullable: false),
                    PercentualeVotantiPervenute = table.Column<string>(nullable: false),
                    PercentualeVotantiTotale = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcolo_voti_sindaco", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_sindaco_sindaco",
                        column: x => x.Sindacoid,
                        principalTable: "sindaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_sindaco_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_sindaco_tipo_ricalcolo",
                        column: x => x.Tiporicalcoloid,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dicastero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    MinisteroId = table.Column<int>(nullable: true),
                    MinistroId = table.Column<int>(nullable: true),
                    DataInizio = table.Column<DateTime>(nullable: false),
                    DataFine = table.Column<DateTime>(nullable: false),
                    DataGiuramento = table.Column<DateTime>(nullable: true),
                    DataSfiducia = table.Column<DateTime>(nullable: true),
                    GovernoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dicastero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dicastero_Governo_GovernoId",
                        column: x => x.GovernoId,
                        principalTable: "Governo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dicastero_Ministero_MinisteroId",
                        column: x => x.MinisteroId,
                        principalTable: "Ministero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dicastero_Ministro_MinistroId",
                        column: x => x.MinistroId,
                        principalTable: "Ministro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "iscritti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                    
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Idsezione = table.Column<int>(nullable: false),
                    Idtiposezione = table.Column<uint>(nullable: true),
                    Municipio = table.Column<int>(nullable: false),
                    Collegiocamera = table.Column<int>(nullable: false),
                    Collegiosenato = table.Column<int>(nullable: false),
                    Collegioprovinciale = table.Column<int>(nullable: false),
                    Cabina = table.Column<int>(nullable: false),
                    Iscrittimaschi = table.Column<int>(nullable: false),
                    Iscrittifemmine = table.Column<int>(nullable: false),
                    Iscrittitotali = table.Column<int>(nullable: false),
                    Iscrittimaschiue = table.Column<int>(nullable: false),
                    Iscrittifemmineue = table.Column<int>(nullable: false),
                    Iscrittitotaliue = table.Column<int>(nullable: false),
                    Iscrittimaschigen = table.Column<int>(nullable: false),
                    Iscrittifemminegen = table.Column<int>(nullable: false),
                    Iscrittitotaligen = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iscritti", x => x.Id);
                    table.ForeignKey(
                        name: "fk_sezioni_iscritti",
                        column: x => x.Idsezione,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tipo_elezione_iscritti",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_tipo_sezione_iscritti",
                        column: x => x.Idtiposezione,
                        principalTable: "tiposezione",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_sezioni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_sezioni", x => x.Id);
                    table.ForeignKey(
                        name: "fk_users_sezione_tipoelezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_users_sezioni_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_users_sezioni_users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_generali",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Sezioneid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Contestate = table.Column<int>(nullable: false),
                    Bianche = table.Column<int>(nullable: false),
                    Nulle = table.Column<int>(nullable: false),
                    TotaleValide = table.Column<int>(nullable: false),
                    SoloSindaco = table.Column<int>(nullable: false),
                    Totale = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_generali", x => x.Id);
                    table.ForeignKey(
                        name: "fk_votigenerali_sezione",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_votigenerali_tipoelezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_generali_storico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Sezioneid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Contestate = table.Column<int>(nullable: false),
                    Bianche = table.Column<int>(nullable: false),
                    Nulle = table.Column<int>(nullable: false),
                    TotaleValide = table.Column<int>(nullable: false),
                    SoloSindaco = table.Column<int>(nullable: false),
                    Totale = table.Column<int>(nullable: false),
                    DataOperazioneOld = table.Column<DateTime>(nullable: false),
                    UtenteOperazioneOld = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_generali_storico", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_storico_sezione",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_storico_tipoelezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_sindaco_storico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sindacoid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    NumeroVotiSoloSindaco = table.Column<int>(nullable: false),
                    Votigeneraliid = table.Column<int>(nullable: false),
                    UtenteOperazioneOld = table.Column<string>(nullable: false),
                    DataOperazioneOld = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_sindaco_storico", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_old_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_old_sindaco",
                        column: x => x.Sindacoid,
                        principalTable: "sindaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_old_tipo_elezioni",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "liste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Denominazione = table.Column<string>(nullable: false),
                    DenominazioneBreve = table.Column<string>(nullable: false),
                    ProgressivoManifesto = table.Column<int>(nullable: true),
                    Progressivo = table.Column<int>(nullable: true),
                    Coalizioneid = table.Column<int>(nullable: true),
                    ProgressivoCoalizione = table.Column<int>(nullable: true),
                    Sindacoid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_liste", x => x.Id);
                    table.ForeignKey(
                        name: "fk_liste_coalizioni",
                        column: x => x.Coalizioneid,
                        principalTable: "Raggruppamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_liste_sindaci",
                        column: x => x.Sindacoid,
                        principalTable: "sindaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fl_liste_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcolo_voti_coalizioni",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Tiporicalcoloid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Coalizioneid = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    PercentualeVoti = table.Column<string>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: false),
                    TotaleSezioni = table.Column<int>(nullable: false),
                    PercentualeSezioniPervenute = table.Column<string>(nullable: false),
                    IscrittiPervenute = table.Column<int>(nullable: false),
                    IscrittiTotale = table.Column<int>(nullable: false),
                    VotantiPervenute = table.Column<int>(nullable: false),
                    VotantiTotale = table.Column<int>(nullable: false),
                    PercentualeVotantiPervenute = table.Column<string>(nullable: false),
                    PercentualeVotantiTotale = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcolo_voti_coalizioni", x => x.Id);
                    table.ForeignKey(
                        name: "fk_riacalcolo_coalizioni_coalizioni",
                        column: x => x.Coalizioneid,
                        principalTable: "Raggruppamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_coalizioni_tipo_elezioni",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_coalizioni_tipo_ricalcolo",
                        column: x => x.Tiporicalcoloid,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "affluenze",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Plessoid = table.Column<int>(nullable: true),
                    Iscrittiid = table.Column<int>(nullable: false),
                    Costituzione1 = table.Column<int>(nullable: true),
                    Costituzione2 = table.Column<int>(nullable: true),
                    Apertura1 = table.Column<int>(nullable: true),
                    Apertura2 = table.Column<int>(nullable: true),
                    Apertura3 = table.Column<int>(nullable: true),
                    Affluenza1 = table.Column<int>(nullable: true),
                    Affluenza2 = table.Column<int>(nullable: true),
                    Affluenza3 = table.Column<int>(nullable: true),
                    Affluenza4 = table.Column<int>(nullable: true),
                    Affluenza5 = table.Column<int>(nullable: true),
                    Votantimaschi1 = table.Column<int>(nullable: true),
                    Votantimaschi2 = table.Column<int>(nullable: true),
                    Votantimaschi3 = table.Column<int>(nullable: true),
                    Votantimaschi4 = table.Column<int>(nullable: true),
                    Votantimaschi5 = table.Column<int>(nullable: true),
                    Votantifemmine1 = table.Column<int>(nullable: true),
                    Votantifemmine2 = table.Column<int>(nullable: true),
                    Votantifemmine3 = table.Column<int>(nullable: true),
                    Votantifemmine4 = table.Column<int>(nullable: true),
                    Votantifemmine5 = table.Column<int>(nullable: true),
                    Votantitotali1 = table.Column<int>(nullable: true),
                    Votantitotali2 = table.Column<int>(nullable: true),
                    Votantitotali3 = table.Column<int>(nullable: true),
                    Votantitotali4 = table.Column<int>(nullable: true),
                    Votantitotali5 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affluenze", x => x.Id);
                    table.ForeignKey(
                        name: "fk_affluenze_iscritti",
                        column: x => x.Iscrittiid,
                        principalTable: "iscritti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_affluenze_plessi",
                        column: x => x.Plessoid,
                        principalTable: "plessi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_affluenza_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "affluenze_storico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Plessoid = table.Column<int>(nullable: true),
                    Iscrittiid = table.Column<int>(nullable: false),
                    Costituzione1 = table.Column<int>(nullable: true),
                    Costituzione2 = table.Column<int>(nullable: true),
                    Apertura1 = table.Column<int>(nullable: true),
                    Apertura2 = table.Column<int>(nullable: true),
                    Apertura3 = table.Column<int>(nullable: true),
                    Affluenza1 = table.Column<int>(nullable: true),
                    Affluenza2 = table.Column<int>(nullable: true),
                    Affluenza3 = table.Column<int>(nullable: true),
                    Affluenza4 = table.Column<int>(nullable: true),
                    Affluenza5 = table.Column<int>(nullable: true),
                    Votantimaschi1 = table.Column<int>(nullable: true),
                    Votantimaschi2 = table.Column<int>(nullable: true),
                    Votantimaschi3 = table.Column<int>(nullable: true),
                    Votantimaschi4 = table.Column<int>(nullable: true),
                    Votantimaschi5 = table.Column<int>(nullable: true),
                    Votantifemmine1 = table.Column<int>(nullable: true),
                    Votantifemmine2 = table.Column<int>(nullable: true),
                    Votantifemmine3 = table.Column<int>(nullable: true),
                    Votantifemmine4 = table.Column<int>(nullable: true),
                    Votantifemmine5 = table.Column<int>(nullable: true),
                    Votantitotali1 = table.Column<int>(nullable: true),
                    Votantitotali2 = table.Column<int>(nullable: true),
                    Votantitotali3 = table.Column<int>(nullable: true),
                    Votantitotali4 = table.Column<int>(nullable: true),
                    Votantitotali5 = table.Column<int>(nullable: true),
                    DataOperazioneold = table.Column<DateTime>(nullable: false),
                    UtenteOperazioneold = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affluenze_storico", x => x.Id);
                    table.ForeignKey(
                        name: "fk_affluenze_storico_iscritti",
                        column: x => x.Iscrittiid,
                        principalTable: "iscritti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_affluenze_storico_plessi",
                        column: x => x.Plessoid,
                        principalTable: "plessi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_affluenze_storico_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_sindaco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sindacoid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    NumeroVotiSoloSindaco = table.Column<int>(nullable: false),
                    Votigeneraliid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_sindaco", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_sindaco",
                        column: x => x.Sindacoid,
                        principalTable: "sindaci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_tipo_elezioni",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_sindaco_voti_generali",
                        column: x => x.Votigeneraliid,
                        principalTable: "voti_generali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "candidati",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                   
                    NomeCandidato = table.Column<string>(nullable: false),
                    CognomeCandidato = table.Column<string>(nullable: false),
                    SessoCandidato = table.Column<string>(nullable: false),
                    Progressivo = table.Column<int>(nullable: false),
                    Listaid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidati", x => x.Id);
                    table.ForeignKey(
                        name: "fk_candidati_lista",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_candidati_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcolo_voti_lista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Idtipoelezione = table.Column<int>(nullable: false),
                    Idtiporicalcolo = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Idlista = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    PercentualeVoti = table.Column<string>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: false),
                    TotaleSezioni = table.Column<int>(nullable: false),
                    PercentualeSezioniPervenute = table.Column<string>(nullable: false),
                    IscrittiPervenute = table.Column<int>(nullable: false),
                    IscrittiTotale = table.Column<int>(nullable: false),
                    VotantiPervenute = table.Column<int>(nullable: false),
                    VotantiTotali = table.Column<int>(nullable: false),
                    PercentualeVotantiPervenute = table.Column<string>(nullable: false),
                    PercentualeVotantiTotale = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcolo_voti_lista", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_lista",
                        column: x => x.Idlista,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_tipo_elezione",
                        column: x => x.Idtipoelezione,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_voti_tipo_ricalcolo",
                        column: x => x.Idtiporicalcolo,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_lista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Listaid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: true),
                    Voti = table.Column<int>(nullable: false),
                    Votigeneraliid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_lista", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_lista_liste",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_lista_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_lista_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_lista_voti_generali",
                        column: x => x.Votigeneraliid,
                        principalTable: "voti_generali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_lista_storico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Listaid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Voti = table.Column<int>(nullable: false),
                    Dataoperazioneold = table.Column<DateTime>(nullable: false),
                    Utenteoperazioneold = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Municipio = table.Column<int>(nullable: true),
                    Votigeneraliid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id, x.Listaid, x.Sezioneid, x.Tipoelezioneid, x.Voti, x.Dataoperazioneold, x.Utenteoperazioneold });
                    table.ForeignKey(
                        name: "fk_liste_voti_lista_storico",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sezioni_voti_lista_storico",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ricalcolo_preferenze",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Tiporicalcoloid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Listaid = table.Column<int>(nullable: false),
                    Candidatoid = table.Column<int>(nullable: false),
                    NumeroVoti = table.Column<int>(nullable: false),
                    PercentualeVoti = table.Column<string>(nullable: false),
                    NumeroSezioni = table.Column<int>(nullable: false),
                    TotaleSezioni = table.Column<int>(nullable: false),
                    PercentualeSezioniPervenute = table.Column<string>(nullable: false),
                    IscrittiPervenute = table.Column<int>(nullable: false),
                    IscrittiTotale = table.Column<int>(nullable: false),
                    VotantiPervenute = table.Column<int>(nullable: false),
                    VotantiTotale = table.Column<int>(nullable: false),
                    PercentualeVotantiPervenute = table.Column<string>(nullable: false),
                    PercentualeVotantiTotale = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ricalcolo_preferenze", x => x.Id);
                    table.ForeignKey(
                        name: "fk_ricalcolo_preferenze_candidato",
                        column: x => x.Candidatoid,
                        principalTable: "candidati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_preferenze_liste",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_preferenze_tipo_elezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_ricalcolo_tipo_ricalcolo",
                        column: x => x.Tiporicalcoloid,
                        principalTable: "tipo_ricalcolo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_peferenze_storico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Candidatoid = table.Column<int>(nullable: false),
                    Listaid = table.Column<int>(nullable: false),
                    Numerovoti = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_peferenze_storico", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_storico_candidati",
                        column: x => x.Candidatoid,
                        principalTable: "candidati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_storico_liste",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_storico_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_storico_tipoelezione_id",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "voti_preferenze",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Tipoelezioneid = table.Column<int>(nullable: false),
                    Sezioneid = table.Column<int>(nullable: false),
                    Candidatoid = table.Column<int>(nullable: false),
                    Listaid = table.Column<int>(nullable: false),
                    Municipio = table.Column<int>(nullable: false),
                    Numerovoti = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voti_preferenze", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_candidati",
                        column: x => x.Candidatoid,
                        principalTable: "candidati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_liste",
                        column: x => x.Listaid,
                        principalTable: "liste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_preferenze_tipoelezione_id",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "profilo_voti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),                  
                    Votigeneraliid = table.Column<int>(nullable: true),
                    Votisindacoid = table.Column<int>(nullable: true),
                    Votilistaid = table.Column<int>(nullable: true),
                    Sezioneid = table.Column<int>(nullable: false),
                    Tipoelezioneid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profilo_voti", x => x.Id);
                    table.ForeignKey(
                        name: "fk_voti_sezioni",
                        column: x => x.Sezioneid,
                        principalTable: "sezioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_tipoelezione",
                        column: x => x.Tipoelezioneid,
                        principalTable: "tipoelezione",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_votigenerali",
                        column: x => x.Votigeneraliid,
                        principalTable: "voti_generali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_votilista",
                        column: x => x.Votilistaid,
                        principalTable: "voti_lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_voti_votisindaco",
                        column: x => x.Votisindacoid,
                        principalTable: "voti_sindaco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "4f823334-492d-45c5-abb2-d6e27a72b6e0", null, "admin", "admin" },
                    { 2, "c9efc755-3ee1-4265-b494-3bcd88c02a49", null, "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "TipoContenuto",
                columns: new[] { "Id", "Codice", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Testo", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9330), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Icona", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9395), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Link", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9420), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Header", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9428), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Image", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9435), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Titolo", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(9442), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CodiceFiscale", "Cognome", "ConcurrencyStamp", "CustomTag", "Email", "EmailConfirmed", "LastModified", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sesso", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "RBRNCL74P16H501C", "Admin", null, null, "agnew74@gmail.com", false, new DateTime(2020, 7, 15, 14, 47, 7, 357, DateTimeKind.Local).AddTicks(923), false, null, "Admin", "agnew74@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEBES17Aaql/nyQi+XEa4ENTXxflFpT9WchvI32qHDcJm5yEhU8WeP/NaeN4nTkbxjg==", "", false, "", "Maschio", false, "admin" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codice", "CreatedBy", "CreatedDate", "Denominazione", "RoleId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(4834), "Inserimento Affluenze", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(9763), "Modifica Affluenze", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(1403), "Visualizzazione Affluenze", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(3279), "Indice Gestione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(8084), "Gestione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(9194), "Registrazione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(275), "Mio Profilo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Rights", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(3252), "Gestione Abilitazioni", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(6637), "Cambio Password", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Home", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(5555), "Home page", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(4445), "Cambio Password", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "User", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 363, DateTimeKind.Local).AddTicks(1356), "Mio Profilo", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(6957), "Visualizzazione Affluenze", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(5792), "Modifica Affluenze", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 362, DateTimeKind.Local).AddTicks(4587), "Inserimento Affluenze", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "VoceMenu",
                columns: new[] { "Id", "Active", "CreatedBy", "CreatedDate", "Icona", "Link", "RoleId", "UpdatedBy", "UpdatedDate", "Voce" },
                values: new object[,]
                {
                    { 9, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(3177), "university", "/interrogazioni/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interrogazioni" },
                    { 8, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(2111), "receipt", "/sindaco/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sindaco" },
                    { 7, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(958), "history", "/liste/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liste" },
                    { 6, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(9881), "user-secret", "/affluenze/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Affluenze" },
                    { 1, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(2303), "user-secret", "/affluenze/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Affluenze" },
                    { 2, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(5034), "history", "/liste/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Liste" },
                    { 3, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(6334), "receipt", "/sindaco/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sindaco" },
                    { 4, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(7663), "university", "/iterrogazioni/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Interrogazioni" },
                    { 5, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 364, DateTimeKind.Local).AddTicks(8791), "user", "/account/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Partito" },
                    { 11, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(5305), "handshake", "/rights/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abilitazioni" },
                    { 10, true, "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 365, DateTimeKind.Local).AddTicks(4243), "user", "/account/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Account" }
                });

            migrationBuilder.InsertData(
                table: "Contenuto",
                columns: new[] { "Id", "ContentuoCard", "CreatedBy", "CreatedDate", "PaginaId", "Tipo", "TipoContenutoId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Da questa pagina è possibile registrare le affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(5167), 1, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "Gestione Affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(66), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "https://res.cloudinary.com/hzekpb1cg/image/upload/c_fill,h_581,w_1185,f_auto/s3/public/prod/s3fs-public/Quartieri-di-Roma.jpg", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(54), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "Gestione Utenti", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(43), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "https://www.panoramasanita.it/wp-content/uploads/2019/05/roma.jpg", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(32), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "Cambio password", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(21), 13, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "/account/changepassword", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(11), 13, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "gear-wide-connected", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local), 13, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "Da questa pagina è possibile cambiare la password", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9989), 13, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Registrazione Utente", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9978), 9, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "/account/register", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9966), 9, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "person-plus-fill", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9923), 9, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Da questa pagina è possibile registrare nuovi utenti", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9912), 9, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Gestione Utenti", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9899), 8, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "/account/manage", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9885), 8, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "people-fill", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9871), 8, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Da questa pagina è possibile gestire gli Utenti", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9858), 8, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Affluenze Visualizzazione", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9843), 3, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "/affluenza/visualizza", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9831), 3, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "people-fill", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9819), 3, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Da questa pagina è possibile visualizzare le affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9806), 3, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Affluenze Modifica", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9795), 2, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "/affluenza/modifica", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9783), 2, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "person-check-fill", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9772), 2, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Da questa pagina è possibile modificare le affluenze", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9759), 2, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Affluenza Inserimento", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9747), 1, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "/affluenza/inserimento", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9730), 1, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "person-plus-fill", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 360, DateTimeKind.Local).AddTicks(9600), 1, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "https://roma.unicatt.it/ingresso-roma-992x560.jpg", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(77), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "Gestione Interrogazioni", "Caricamento", new DateTime(2020, 7, 15, 14, 47, 7, 361, DateTimeKind.Local).AddTicks(88), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "affluenze",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_iscritti_idx",
                table: "affluenze",
                column: "Iscrittiid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_plessi_idx",
                table: "affluenze",
                column: "Plessoid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenza_sezioni_idx",
                table: "affluenze",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_tipo_elezione_idx",
                table: "affluenze",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_storico_iscritti_idx",
                table: "affluenze_storico",
                column: "Iscrittiid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_storico_plessi_idx",
                table: "affluenze_storico",
                column: "Plessoid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_storico_sezioni_idx",
                table: "affluenze_storico",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_affluenze_storico_tipo_elezione_idx",
                table: "affluenze_storico",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_aggregazione_interrogazioni_tipo_elezione_idx",
                table: "aggregazione_interrogazioni",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "unique_aggregazione_interrogazioni",
                table: "aggregazione_interrogazioni",
                columns: new[] { "Descrizione", "Tipoelezioneid", "Codice" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_candidati_lista_idx",
                table: "candidati",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_candidati_tipo_elezione_idx",
                table: "candidati",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "uk_candidato_lista_progressivo",
                table: "candidati",
                columns: new[] { "Progressivo", "Listaid", "Tipoelezioneid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contenuto_PaginaId",
                table: "Contenuto",
                column: "PaginaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contenuto_TipoContenutoId",
                table: "Contenuto",
                column: "TipoContenutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dicastero_GovernoId",
                table: "Dicastero",
                column: "GovernoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dicastero_MinisteroId",
                table: "Dicastero",
                column: "MinisteroId");

            migrationBuilder.CreateIndex(
                name: "IX_Dicastero_MinistroId",
                table: "Dicastero",
                column: "MinistroId");

            migrationBuilder.CreateIndex(
                name: "idelegencam_UNIQUE",
                table: "elegen",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_elegen_tipo_elezione_idx",
                table: "elegen",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_fase_elezione_tipo_elezione_idx",
                table: "fase_elezione",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "uq_fase_elezioni_elezioni",
                table: "fase_elezione",
                columns: new[] { "Codice", "Idtipoelezione" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Governo_LegislaturaId",
                table: "Governo",
                column: "LegislaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Governo_PremierId",
                table: "Governo",
                column: "PremierId");

            migrationBuilder.CreateIndex(
                name: "idsezione_UNIQUE",
                table: "iscritti",
                column: "Idsezione",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_tipo_elezione_iscritti_idx",
                table: "iscritti",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_tiposezione_iscritti_idx",
                table: "iscritti",
                column: "Idtiposezione");

            migrationBuilder.CreateIndex(
                name: "IX_Legislatura_CameraId",
                table: "Legislatura",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Legislatura_SenatoId",
                table: "Legislatura",
                column: "SenatoId");

            migrationBuilder.CreateIndex(
                name: "fk_liste_coalizioni_idx1",
                table: "liste",
                column: "Coalizioneid");

            migrationBuilder.CreateIndex(
                name: "fk_liste_coalizioni_idx",
                table: "liste",
                column: "ProgressivoCoalizione");

            migrationBuilder.CreateIndex(
                name: "fk_liste_sindaci_idx",
                table: "liste",
                column: "Sindacoid");

            migrationBuilder.CreateIndex(
                name: "fl_liste_tipo_elezione_idx",
                table: "liste",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_matrice_tipo_elezione_idx",
                table: "matrice",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "iscrittifemmine_UNIQUE",
                table: "matrice",
                column: "Iscrittifemmine",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iscrittimaschi_UNIQUE",
                table: "matrice",
                column: "Iscrittimaschi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Militanza_MinistroId",
                table: "Militanza",
                column: "MinistroId");

            migrationBuilder.CreateIndex(
                name: "IX_Militanza_PartitoId",
                table: "Militanza",
                column: "PartitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Militanza_PremierId",
                table: "Militanza",
                column: "PremierId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagina_RoleId",
                table: "Pagina",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Partito_CameraId",
                table: "Partito",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Partito_CoalizioneId",
                table: "Partito",
                column: "CoalizioneId");

            migrationBuilder.CreateIndex(
                name: "IX_Partito_SenatoId",
                table: "Partito",
                column: "SenatoId");

            migrationBuilder.CreateIndex(
                name: "fk_plessi_tipoelezione_idx",
                table: "plessi",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sezioni_idx",
                table: "profilo_voti",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_votigenerali_idx",
                table: "profilo_voti",
                column: "Votigeneraliid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_votilista_idx",
                table: "profilo_voti",
                column: "Votilistaid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_votisindaco_idx",
                table: "profilo_voti",
                column: "Votisindacoid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_tipoelezione_idx",
                table: "profilo_voti",
                columns: new[] { "Tipoelezioneid", "Sezioneid" });

            migrationBuilder.CreateIndex(
                name: "id_UNIQUE",
                table: "Raggruppamento",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_tipoelezioni_coalizioni_idx",
                table: "Raggruppamento",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_sindaci_coalizioni_idx",
                table: "Raggruppamento",
                column: "Sindacoid");

            migrationBuilder.CreateIndex(
                name: "fk_ricaloli_affluenza_tipo_elezione_idx",
                table: "ricalcoli_affluenza",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "ricalcoli_affluenza_idx",
                table: "ricalcoli_affluenza",
                column: "Idtiporicalcolo");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcoli_apertura_costituzione_tipo_elezione_idx",
                table: "ricalcoli_apertura_costituzione",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcoli_apertura_costituzione_tipo_ricalcolo_idx",
                table: "ricalcoli_apertura_costituzione",
                column: "Idtiporicalcolo");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_preferenze_candidato_idx",
                table: "ricalcolo_preferenze",
                column: "Candidatoid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_preferenze_liste_idx",
                table: "ricalcolo_preferenze",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_preferenze_tipo_elezione_idx",
                table: "ricalcolo_preferenze",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_tipo_ricalcolo_idx",
                table: "ricalcolo_preferenze",
                column: "Tiporicalcoloid");

            migrationBuilder.CreateIndex(
                name: "fk_riacalcolo_coalizioni_coalizioni_idx",
                table: "ricalcolo_voti_coalizioni",
                column: "Coalizioneid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_coalizioni_tipo_elezioni_idx",
                table: "ricalcolo_voti_coalizioni",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_coalizioni_tipo_ricalcolo_idx",
                table: "ricalcolo_voti_coalizioni",
                column: "Tiporicalcoloid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_voti_lista_idx",
                table: "ricalcolo_voti_lista",
                column: "Idlista");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_voti_tipo_elezione_idx",
                table: "ricalcolo_voti_lista",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_voti_tipo_ricalcolo_idx",
                table: "ricalcolo_voti_lista",
                column: "Idtiporicalcolo");

            migrationBuilder.CreateIndex(
                name: "fk_riacalcolo_sindaci_sindaci_idx",
                table: "ricalcolo_voti_sindaco",
                column: "Sindacoid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_sindaci_tipo_elezioni_idx",
                table: "ricalcolo_voti_sindaco",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_ricalcolo_sindaci_tipo_ricalcolo_idx",
                table: "ricalcolo_voti_sindaco",
                column: "Tiporicalcoloid");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_sezioni_plessi_idx",
                table: "sezioni",
                column: "Idplesso");

            migrationBuilder.CreateIndex(
                name: "fk_sezioni_tipoelezione_idx",
                table: "sezioni",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_sezioni_tiposezione_idx",
                table: "sezioni",
                column: "Idtiposezione");

            migrationBuilder.CreateIndex(
                name: "numerosezione_UNIQUE",
                table: "sezioni",
                columns: new[] { "Numerosezione", "Idtipoelezione" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_tipo_elezione_sindaci_idx",
                table: "sindaci",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "tipo_interrogazione_tipo_elezione_idx",
                table: "tipo_interrogazione",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_tipo_ricalcolo_tipo_elezione_idx",
                table: "tipo_ricalcolo",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "tipo_ricalcolo_aggregazione_tipo_elezione_idx",
                table: "tipo_ricalcolo_aggregazione",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "tipo_ricalcolo_aggregazione_unique",
                table: "tipo_ricalcolo_aggregazione",
                columns: new[] { "Descrizione", "Codice", "Tipoelezioneid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "descrizione_UNIQUE",
                table: "tipoelezione",
                column: "Descrizione",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idtipoelezione_UNIQUE",
                table: "tipoelezione",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAudit_IdUtente",
                table: "UserAudit",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_users_sezione_tipoelezione_idx",
                table: "users_sezioni",
                column: "Idtipoelezione");

            migrationBuilder.CreateIndex(
                name: "fk_users_sezioni_sezioni_idx",
                table: "users_sezioni",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_users_sezioni_users_idx",
                table: "users_sezioni",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoceMenu_RoleId",
                table: "VoceMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sezione_idx",
                table: "voti_generali",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_tipoelezione_idx",
                table: "voti_generali",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_storico_sezione_idx",
                table: "voti_generali_storico",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_storico_tipoelezione_idx",
                table: "voti_generali_storico",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_lista_liste_idx",
                table: "voti_lista",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_lista_sezioni_idx",
                table: "voti_lista",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_lista_tipo_elezione_idx",
                table: "voti_lista",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_lista_voti_generali_idx",
                table: "voti_lista",
                column: "Votigeneraliid");

            migrationBuilder.CreateIndex(
                name: "fk_liste_voti_lista_storico_idx",
                table: "voti_lista_storico",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_sezioni_voti_lista_storico_idx",
                table: "voti_lista_storico",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_storico_candidati_idx",
                table: "voti_peferenze_storico",
                column: "Candidatoid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_storico_liste_idx",
                table: "voti_peferenze_storico",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_storico_sezioni_idx",
                table: "voti_peferenze_storico",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_storico_tipoelezione_id_idx",
                table: "voti_peferenze_storico",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_candidati_idx",
                table: "voti_preferenze",
                column: "Candidatoid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_liste_idx",
                table: "voti_preferenze",
                column: "Listaid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_sezioni_idx",
                table: "voti_preferenze",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_preferenze_tipoelezione_id_idx",
                table: "voti_preferenze",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "uk_voti_preferenze_lista_candidato_sezione",
                table: "voti_preferenze",
                columns: new[] { "Candidatoid", "Sezioneid", "Listaid", "Tipoelezioneid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_sezioni_idx",
                table: "voti_sindaco",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_sindaco_idx",
                table: "voti_sindaco",
                column: "Sindacoid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_tipo_elezioni_idx",
                table: "voti_sindaco",
                column: "Tipoelezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_voti_generali_idx",
                table: "voti_sindaco",
                column: "Votigeneraliid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_old_sezioni_idx",
                table: "voti_sindaco_storico",
                column: "Sezioneid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_old_sindaco_idx",
                table: "voti_sindaco_storico",
                column: "Sindacoid");

            migrationBuilder.CreateIndex(
                name: "fk_voti_sindaco_old_tipo_elezioni_idx",
                table: "voti_sindaco_storico",
                column: "Tipoelezioneid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "affluenze");

            migrationBuilder.DropTable(
                name: "affluenze_storico");

            migrationBuilder.DropTable(
                name: "aggregazione_interrogazioni");

            migrationBuilder.DropTable(
                name: "Contenuto");

            migrationBuilder.DropTable(
                name: "Dicastero");

            migrationBuilder.DropTable(
                name: "elegen");

            migrationBuilder.DropTable(
                name: "fase_elezione");

            migrationBuilder.DropTable(
                name: "matrice");

            migrationBuilder.DropTable(
                name: "Militanza");

            migrationBuilder.DropTable(
                name: "municipi");

            migrationBuilder.DropTable(
                name: "profilo_voti");

            migrationBuilder.DropTable(
                name: "ricalcoli_affluenza");

            migrationBuilder.DropTable(
                name: "ricalcoli_apertura_costituzione");

            migrationBuilder.DropTable(
                name: "ricalcolo_preferenze");

            migrationBuilder.DropTable(
                name: "ricalcolo_voti_coalizioni");

            migrationBuilder.DropTable(
                name: "ricalcolo_voti_lista");

            migrationBuilder.DropTable(
                name: "ricalcolo_voti_sindaco");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "tipo_interrogazione");

            migrationBuilder.DropTable(
                name: "tipo_ricalcolo_aggregazione");

            migrationBuilder.DropTable(
                name: "UserAudit");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "users_sezioni");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VoceMenu");

            migrationBuilder.DropTable(
                name: "voti_generali_storico");

            migrationBuilder.DropTable(
                name: "voti_lista_storico");

            migrationBuilder.DropTable(
                name: "voti_peferenze_storico");

            migrationBuilder.DropTable(
                name: "voti_preferenze");

            migrationBuilder.DropTable(
                name: "voti_sindaco_storico");

            migrationBuilder.DropTable(
                name: "iscritti");

            migrationBuilder.DropTable(
                name: "Pagina");

            migrationBuilder.DropTable(
                name: "TipoContenuto");

            migrationBuilder.DropTable(
                name: "Governo");

            migrationBuilder.DropTable(
                name: "Ministero");

            migrationBuilder.DropTable(
                name: "Ministro");

            migrationBuilder.DropTable(
                name: "Partito");

            migrationBuilder.DropTable(
                name: "voti_lista");

            migrationBuilder.DropTable(
                name: "voti_sindaco");

            migrationBuilder.DropTable(
                name: "tipo_ricalcolo");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "candidati");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Legislatura");

            migrationBuilder.DropTable(
                name: "Premier");

            migrationBuilder.DropTable(
                name: "Coalizione");

            migrationBuilder.DropTable(
                name: "voti_generali");

            migrationBuilder.DropTable(
                name: "liste");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "Senato");

            migrationBuilder.DropTable(
                name: "sezioni");

            migrationBuilder.DropTable(
                name: "Raggruppamento");

            migrationBuilder.DropTable(
                name: "plessi");

            migrationBuilder.DropTable(
                name: "tiposezione");

            migrationBuilder.DropTable(
                name: "sindaci");

            migrationBuilder.DropTable(
                name: "tipoelezione");
        }
    }
}
