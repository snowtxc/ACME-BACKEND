using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addempresacolumncompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EmpresaId",
                table: "Compras",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Empresas_EmpresaId",
                table: "Compras",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Empresas_EmpresaId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_EmpresaId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Compras");
        }
    }
}
