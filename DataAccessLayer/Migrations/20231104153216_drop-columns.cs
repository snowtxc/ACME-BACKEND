using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class dropcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Compras_CompraId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CompraId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "Productos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CompraId",
                table: "Productos",
                column: "CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Compras_CompraId",
                table: "Productos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }
    }
}
