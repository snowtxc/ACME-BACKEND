using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class LookAndFeelImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NavBarId",
                table: "LooksAndFeels",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "LooksAndFeels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ColorFondo",
                table: "LooksAndFeels",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ColorPrincipal",
                table: "LooksAndFeels",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ColorSecundario",
                table: "LooksAndFeels",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "LooksAndFeels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriasDestacadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    ImagenUrl = table.Column<string>(type: "longtext", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: true),
                    LookAndFeelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDestacadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasDestacadas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoriasDestacadas_LooksAndFeels_LookAndFeelId",
                        column: x => x.LookAndFeelId,
                        principalTable: "LooksAndFeels",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LooksAndFeels_EmpresaId",
                table: "LooksAndFeels",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasDestacadas_CategoriaId",
                table: "CategoriasDestacadas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasDestacadas_LookAndFeelId",
                table: "CategoriasDestacadas",
                column: "LookAndFeelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LooksAndFeels_Empresas_EmpresaId",
                table: "LooksAndFeels",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LooksAndFeels_Empresas_EmpresaId",
                table: "LooksAndFeels");

            migrationBuilder.DropTable(
                name: "CategoriasDestacadas");

            migrationBuilder.DropIndex(
                name: "IX_LooksAndFeels_EmpresaId",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "ColorFondo",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "ColorPrincipal",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "ColorSecundario",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "LooksAndFeels");

            migrationBuilder.AlterColumn<string>(
                name: "NavBarId",
                table: "LooksAndFeels",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HomeId",
                table: "LooksAndFeels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
