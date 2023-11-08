using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_EnvioPaquetes_EnvioPaqueteId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_EnvioPaqueteId",
                table: "Compras");

            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "EnvioPaquetes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnvioPaquetes_CompraId",
                table: "EnvioPaquetes",
                column: "CompraId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnvioPaquetes_Compras_CompraId",
                table: "EnvioPaquetes",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvioPaquetes_Compras_CompraId",
                table: "EnvioPaquetes");

            migrationBuilder.DropIndex(
                name: "IX_EnvioPaquetes_CompraId",
                table: "EnvioPaquetes");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "EnvioPaquetes");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EnvioPaqueteId",
                table: "Compras",
                column: "EnvioPaqueteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_EnvioPaquetes_EnvioPaqueteId",
                table: "Compras",
                column: "EnvioPaqueteId",
                principalTable: "EnvioPaquetes",
                principalColumn: "Id");
        }
    }
}
