using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecorridoHistoricoApi.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FechasManuales",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "Date", nullable: false),
                    es_recurrente = table.Column<bool>(type: "bit", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fechas_manuales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tandas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tandas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    es_flexible = table.Column<bool>(type: "bit", nullable: false),
                    color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cantidad_maxima = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dia = table.Column<int>(type: "int", nullable: false),
                    hora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tanda_id = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_horarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_horarios_tandas_tanda_id",
                        column: x => x.tanda_id,
                        principalTable: "Tandas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "horario_tipo",
                columns: table => new
                {
                    horarios_id = table.Column<int>(type: "int", nullable: false),
                    tipos_recorrido_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_horario_tipo", x => new { x.horarios_id, x.tipos_recorrido_id });
                    table.ForeignKey(
                        name: "fk_horario_tipo_horarios_horarios_id",
                        column: x => x.horarios_id,
                        principalTable: "Horarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_horario_tipo_tipos_tipos_recorrido_id",
                        column: x => x.tipos_recorrido_id,
                        principalTable: "Tipos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecorridosHistorico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidad_visitantes = table.Column<int>(type: "int", nullable: false),
                    fecha_visita = table.Column<DateTime>(type: "Date", nullable: false),
                    institucion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    idioma = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_culminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estado_id = table.Column<int>(type: "int", nullable: false),
                    tipo_recorrido_id = table.Column<int>(type: "int", nullable: false),
                    horario_id = table.Column<int>(type: "int", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recorridos_historico", x => x.id);
                    table.ForeignKey(
                        name: "fk_recorridos_historico_estados_estado_id",
                        column: x => x.estado_id,
                        principalTable: "Estados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_recorridos_historico_horarios_horario_id",
                        column: x => x.horario_id,
                        principalTable: "Horarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_recorridos_historico_tipos_tipo_recorrido_id",
                        column: x => x.tipo_recorrido_id,
                        principalTable: "Tipos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_horario_tipo_tipos_recorrido_id",
                table: "horario_tipo",
                column: "tipos_recorrido_id");

            migrationBuilder.CreateIndex(
                name: "ix_horarios_tanda_id",
                table: "Horarios",
                column: "tanda_id");

            migrationBuilder.CreateIndex(
                name: "ix_recorridos_historico_estado_id",
                table: "RecorridosHistorico",
                column: "estado_id");

            migrationBuilder.CreateIndex(
                name: "ix_recorridos_historico_horario_id",
                table: "RecorridosHistorico",
                column: "horario_id");

            migrationBuilder.CreateIndex(
                name: "ix_recorridos_historico_tipo_recorrido_id",
                table: "RecorridosHistorico",
                column: "tipo_recorrido_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechasManuales");

            migrationBuilder.DropTable(
                name: "horario_tipo");

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
