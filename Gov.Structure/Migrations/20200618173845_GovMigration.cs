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
                    Codice = table.Column<string>(maxLength: 10, nullable: false),
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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "6218897a-541d-418e-ae37-5dcec6234ab4", null, "admin", "admin" },
                    { 2, "672e7d23-192e-4d2f-bdc2-500dbc0ab7bd", null, "user", "user" }
                });

            migrationBuilder.InsertData(
                table: "TipoContenuto",
                columns: new[] { "Id", "Codice", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Testo", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9258), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Icona", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9310), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Link", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9319), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Header", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9323), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Image", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9326), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Titolo", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(9330), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CodiceFiscale", "Cognome", "ConcurrencyStamp", "CustomTag", "Email", "EmailConfirmed", "LastModified", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sesso", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "RBRNCL74P16H501C", "Admin", null, null, "agnew74@gmail.com", false, new DateTime(2020, 6, 18, 19, 38, 45, 138, DateTimeKind.Local).AddTicks(2614), false, null, "Admin", "agnew74@gmail.com", "admin", null, "AQAAAAEAACcQAAAAEMDQBBppp2tpKOuW4fDjpGZXjyBApLm1t/FCe7TKGELWIz0wN3EGvoQm/pQOtolrgA==", "", false, "", "Maschio", false, "admin" });

            migrationBuilder.InsertData(
                table: "Pagina",
                columns: new[] { "Id", "Codice", "CreatedBy", "CreatedDate", "Denominazione", "RoleId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(263), "Inserimento Premier", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(1710), "Modifica Premier", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(2307), "Visualizzazione Premier", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(3238), "Indice Gestione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(5564), "Gestione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(6062), "Registrazione Utenti", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(6556), "Mio Profilo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Rights", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(7539), "Gestione Abilitazioni", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(9000), "Cambio Password", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "Home", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(8514), "Home page", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(8025), "Cambio Password", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "User", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(7046), "Mio Profilo", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(4923), "Visualizzazione Premier", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(4390), "Modifica Premier", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 140, DateTimeKind.Local).AddTicks(3839), "Inserimento Premier", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                    { 9, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(6572), "university", "/partito/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Partito" },
                    { 8, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(5954), "receipt", "/dicastero/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dicastero" },
                    { 7, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(5463), "history", "/governo/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Governo" },
                    { 6, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(4975), "user-secret", "/premier/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premier" },
                    { 1, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(1534), "user-secret", "/premier/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premier" },
                    { 2, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(2797), "history", "/governo/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Governo" },
                    { 3, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(3354), "receipt", "/dicastero/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dicastero" },
                    { 4, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(3903), "university", "/partito/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Partito" },
                    { 5, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(4478), "user", "/account/index", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Partito" },
                    { 11, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(7519), "handshake", "/rights/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abilitazioni" },
                    { 10, true, "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 141, DateTimeKind.Local).AddTicks(7044), "user", "/account/index", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Account" }
                });

            migrationBuilder.InsertData(
                table: "Contenuto",
                columns: new[] { "Id", "ContentuoCard", "CreatedBy", "CreatedDate", "PaginaId", "Tipo", "TipoContenutoId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Da questa pagina è possibile registrare un nuovo Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(6671), 1, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, "Gestione Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8913), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, "https://res.cloudinary.com/hzekpb1cg/image/upload/c_fill,h_581,w_1185,f_auto/s3/public/prod/s3fs-public/Quartieri-di-Roma.jpg", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8910), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "Gestione Utenti", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8906), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "https://www.panoramasanita.it/wp-content/uploads/2019/05/roma.jpg", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8903), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "Cambio password", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8899), 13, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "/account/changepassword", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8896), 13, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "gear-wide-connected", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8893), 13, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "Da questa pagina è possibile cambiare la password", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8889), 13, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "Registrazione Utente", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8886), 9, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "/account/register", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8882), 9, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "person-plus-fill", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8879), 9, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "Da questa pagina è possibile registrare nuovi utenti", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8875), 9, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "Gestione Utenti", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8872), 8, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "/account/manage", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8868), 8, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "people-fill", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8865), 8, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "Da questa pagina è possibile gestire gli Utenti", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8861), 8, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "Premier Visualizzazione", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8857), 3, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "/premier/visualizza", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8854), 3, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "people-fill", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8851), 3, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Da questa pagina è possibile visualizzare i Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8847), 3, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Premier Modifica", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8843), 2, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "/premier/modifica", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8839), 2, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "person-check-fill", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8834), 2, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Da questa pagina è possibile modificare un Premier", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8830), 2, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Premier Inserimento", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8825), 1, "Header", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "/premier/inserimento", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8820), 1, "Link", 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "person-plus-fill", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8782), 1, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, "https://roma.unicatt.it/ingresso-roma-992x560.jpg", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8916), 14, "Image", 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, "Gestione Governo", "Caricamento", new DateTime(2020, 6, 18, 19, 38, 45, 139, DateTimeKind.Local).AddTicks(8920), 14, "Titolo", 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

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
                name: "IX_Governo_LegislaturaId",
                table: "Governo",
                column: "LegislaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Governo_PremierId",
                table: "Governo",
                column: "PremierId");

            migrationBuilder.CreateIndex(
                name: "IX_Legislatura_CameraId",
                table: "Legislatura",
                column: "CameraId");

            migrationBuilder.CreateIndex(
                name: "IX_Legislatura_SenatoId",
                table: "Legislatura",
                column: "SenatoId");

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
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
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
                name: "IX_VoceMenu_RoleId",
                table: "VoceMenu",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contenuto");

            migrationBuilder.DropTable(
                name: "Dicastero");

            migrationBuilder.DropTable(
                name: "Militanza");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserAudit");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "VoceMenu");

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
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Legislatura");

            migrationBuilder.DropTable(
                name: "Premier");

            migrationBuilder.DropTable(
                name: "Coalizione");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "Senato");
        }
    }
}
