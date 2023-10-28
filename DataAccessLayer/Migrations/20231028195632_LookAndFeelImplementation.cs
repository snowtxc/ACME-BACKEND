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
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

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

            migrationBuilder.CreateTable(
                name: "CategoriasDestacadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    ImagenUrl = table.Column<string>(type: "longtext", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasDestacadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasDestacadas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasDestacadas_CategoriaId",
                table: "CategoriasDestacadas",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasDestacadas");

            migrationBuilder.DropColumn(
                name: "ColorFondo",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "ColorPrincipal",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "ColorSecundario",
                table: "LooksAndFeels");

            migrationBuilder.AlterColumn<string>(
                name: "NavBarId",
                table: "LooksAndFeels",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
