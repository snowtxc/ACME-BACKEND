using Microsoft.EntityFrameworkCore.Migrations;

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
