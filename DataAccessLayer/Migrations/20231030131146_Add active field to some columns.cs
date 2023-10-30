using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Addactivefieldtosomecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "TiposIva",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Reclamos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ProductosRelacionados",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ProductoFotos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Pickups",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "LooksAndFeels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "LineasCarrito",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EstadosCompras",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EnvioPaquetes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Empresas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Direcciones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ComprasEstados",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Compras",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "CategoriasProductos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Categorias",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Calificaciones",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TiposIva");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Reclamos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ProductosRelacionados");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ProductoFotos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "LooksAndFeels");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "LineasCarrito");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EstadosCompras");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EnvioPaquetes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Direcciones");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ComprasEstados");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CategoriasProductos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AspNetUsers");
        }
    }
}
