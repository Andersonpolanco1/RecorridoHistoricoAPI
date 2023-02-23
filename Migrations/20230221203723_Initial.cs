using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecorridoHistoricoApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FechasManuales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "Date", nullable: false),
                    EsRecurrente = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechasManuales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EsFlexible = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CantidadMaxima = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    Hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TandaId = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Tandas_TandaId",
                        column: x => x.TandaId,
                        principalTable: "Tandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorarioTipo",
                columns: table => new
                {
                    HorariosId = table.Column<int>(type: "int", nullable: false),
                    TiposRecorridoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorarioTipo", x => new { x.HorariosId, x.TiposRecorridoId });
                    table.ForeignKey(
                        name: "FK_HorarioTipo_Horarios_HorariosId",
                        column: x => x.HorariosId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HorarioTipo_Tipos_TiposRecorridoId",
                        column: x => x.TiposRecorridoId,
                        principalTable: "Tipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecorridosHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadVisitantes = table.Column<int>(type: "int", nullable: false),
                    FechaVisita = table.Column<DateTime>(type: "Date", nullable: false),
                    Institucion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCulminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    TipoRecorridoId = table.Column<int>(type: "int", nullable: false),
                    HorarioId = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecorridosHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecorridosHistorico_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecorridosHistorico_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecorridosHistorico_Tipos_TipoRecorridoId",
                        column: x => x.TipoRecorridoId,
                        principalTable: "Tipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_TandaId",
                table: "Horarios",
                column: "TandaId");

            migrationBuilder.CreateIndex(
                name: "IX_HorarioTipo_TiposRecorridoId",
                table: "HorarioTipo",
                column: "TiposRecorridoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecorridosHistorico_EstadoId",
                table: "RecorridosHistorico",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RecorridosHistorico_HorarioId",
                table: "RecorridosHistorico",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RecorridosHistorico_TipoRecorridoId",
                table: "RecorridosHistorico",
                column: "TipoRecorridoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechasManuales");

            migrationBuilder.DropTable(
                name: "HorarioTipo");

            migrationBuilder.DropTable(
                name: "RecorridosHistorico");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropTable(
                name: "Tandas");
        }
    }
}
