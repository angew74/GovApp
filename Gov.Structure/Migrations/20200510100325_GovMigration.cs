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
                    Codice = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagina", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "TipoContenuto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    Codice = table.Column<string>(maxLength: 10, nullable: false),
                    ContenutoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoContenuto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoContenuto_Contenuto_ContenutoId",
                        column: x => x.ContenutoId,
                        principalTable: "Contenuto",
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
                table: "Pagina",
                columns: new[] { "Id", "Codice", "CreatedBy", "CreatedDate", "Denominazione", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(2917), "Inserimento Premier", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(3572), "Modifica Premier", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(3594), "Visualizzazione Premier", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TipoContenuto",
                columns: new[] { "Id", "Codice", "ContenutoId", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Testo", null, "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 173, DateTimeKind.Local).AddTicks(4011), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Icona", null, "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 175, DateTimeKind.Local).AddTicks(9709), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Immagine", null, "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 175, DateTimeKind.Local).AddTicks(9758), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Link", null, "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 175, DateTimeKind.Local).AddTicks(9763), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Contenuto",
                columns: new[] { "Id", "ContentuoCard", "CreatedBy", "CreatedDate", "PaginaId", "Tipo", "TipoContenutoId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Da questa pagina è possibile registrare un nuovo Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(5802), 1, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "user-secret", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6233), 1, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "/premier/inserimento", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6250), 1, "Link", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Da questa pagina è possibile modificare un Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6255), 2, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "user-secret", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6259), 2, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "/premier/modifica", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6263), 2, "Link", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Da questa pagina è possibile visualizzare i Premier", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6267), 3, "Testo", 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "user-secret", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6271), 3, "Icona", 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "/premier/visualizza", "Caricamento", new DateTime(2020, 5, 10, 12, 3, 25, 177, DateTimeKind.Local).AddTicks(6274), 3, "Link", 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contenuto_PaginaId",
                table: "Contenuto",
                column: "PaginaId");

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
                name: "IX_TipoContenuto_ContenutoId",
                table: "TipoContenuto",
                column: "ContenutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dicastero");

            migrationBuilder.DropTable(
                name: "Militanza");

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
                name: "Contenuto");

            migrationBuilder.DropTable(
                name: "Legislatura");

            migrationBuilder.DropTable(
                name: "Premier");

            migrationBuilder.DropTable(
                name: "Coalizione");

            migrationBuilder.DropTable(
                name: "Pagina");

            migrationBuilder.DropTable(
                name: "Camera");

            migrationBuilder.DropTable(
                name: "Senato");
        }
    }
}
