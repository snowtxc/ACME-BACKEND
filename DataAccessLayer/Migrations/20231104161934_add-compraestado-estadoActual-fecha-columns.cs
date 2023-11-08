using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addcompraestadoestadoActualfechacolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
         

            migrationBuilder.AddColumn<bool>(
                name: "EstadoActual",
                table: "ComprasEstados",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "ComprasEstados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnvioPaquetes_CompraId",
                table: "EnvioPaquetes");

            migrationBuilder.DropColumn(
                name: "EstadoActual",
                table: "ComprasEstados");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "ComprasEstados");

            migrationBuilder.AddColumn<int>(
                name: "EnvioPaqueteId",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnvioPaquetes_CompraId",
                table: "EnvioPaquetes",
                column: "CompraId",
                unique: true);
        }
    }
}
