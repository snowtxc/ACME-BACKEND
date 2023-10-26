﻿// <auto-generated />
using System;
using DataAccessLayer.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231025144631_Add producto createdAt twoss")]
    partial class AddproductocreatedAttwoss
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("acme_backend.Models.Calificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("acme_backend.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("acme_backend.Models.CategoriaProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ProductoId");

                    b.ToTable("CategoriasProductos");
                });

            modelBuilder.Entity("acme_backend.Models.Ciudad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Ciudades");
                });

            modelBuilder.Entity("acme_backend.Models.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("CostoTotal")
                        .HasColumnType("double");

                    b.Property<int?>("EnvioPaqueteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MetodoEnvio")
                        .HasColumnType("int");

                    b.Property<int>("MetodoPago")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("EnvioPaqueteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("acme_backend.Models.CompraEstado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<int>("EstadoCompraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("EstadoCompraId");

                    b.ToTable("ComprasEstados");
                });

            modelBuilder.Entity("acme_backend.Models.CompraProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<double>("PrecioUnitario")
                        .HasColumnType("double");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("ComprasProductos");
                });

            modelBuilder.Entity("acme_backend.Models.Configuracion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PlazosDiasRecordatorioCarrito")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Configuraciones");
                });

            modelBuilder.Entity("acme_backend.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("acme_backend.Models.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CalleEntre1")
                        .HasColumnType("longtext");

                    b.Property<string>("CalleEntre2")
                        .HasColumnType("longtext");

                    b.Property<int>("CiudadId")
                        .HasColumnType("int");

                    b.Property<string>("NroPuerta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CiudadId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("acme_backend.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("CostoEnvio")
                        .HasColumnType("double");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Wallet")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("acme_backend.Models.EnvioPaquete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CodigoSeguimiento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DireccionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DireccionId");

                    b.ToTable("EnvioPaquetes");
                });

            modelBuilder.Entity("acme_backend.Models.EstadoCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CompraId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.ToTable("EstadosCompras");
                });

            modelBuilder.Entity("acme_backend.Models.LineaCarrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("LineasCarrito");
                });

            modelBuilder.Entity("acme_backend.Models.LookAndFeel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HomeId")
                        .HasColumnType("int");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NavBarId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreSitio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LooksAndFeels");
                });

            modelBuilder.Entity("acme_backend.Models.PickUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DireccionId")
                        .HasColumnType("int");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Lat")
                        .HasColumnType("double");

                    b.Property<double>("Lng")
                        .HasColumnType("double");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PlazoDiasPreparacion")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DireccionId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Pickups");
                });

            modelBuilder.Entity("acme_backend.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("CompraId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("date");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DocumentoPdf")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("LinkFicha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Precio")
                        .HasColumnType("double");

                    b.Property<int>("TipoIvaId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("TipoIvaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("acme_backend.Models.ProductoFoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("ProductoFotos");
                });

            modelBuilder.Entity("acme_backend.Models.ProductosRelacionados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("productoId")
                        .HasColumnType("int");

                    b.Property<int>("productoRelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productoId");

                    b.HasIndex("productoRelId");

                    b.ToTable("ProductosRelacionados");
                });

            modelBuilder.Entity("acme_backend.Models.Reclamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EstadoReclamo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.ToTable("Reclamos");
                });

            modelBuilder.Entity("acme_backend.Models.RetiroPickup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RetirosPickup");
                });

            modelBuilder.Entity("acme_backend.Models.TipoIva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Porcentaje")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("TiposIva");
                });

            modelBuilder.Entity("acme_backend.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("acme_backend.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("acme_backend.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("acme_backend.Models.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("acme_backend.Models.Calificacion", b =>
                {
                    b.HasOne("acme_backend.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("acme_backend.Models.Categoria", b =>
                {
                    b.HasOne("acme_backend.Models.Empresa", null)
                        .WithMany("Categorias")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("acme_backend.Models.CategoriaProducto", b =>
                {
                    b.HasOne("acme_backend.Models.Producto", "Producto")
                        .WithMany("CategoriasProductos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Categoria", "Categoria")
                        .WithMany("CategoriasProductos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("acme_backend.Models.Ciudad", b =>
                {
                    b.HasOne("acme_backend.Models.Departamento", "Departamento")
                        .WithMany("Ciudades")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("acme_backend.Models.Compra", b =>
                {
                    b.HasOne("acme_backend.Models.EnvioPaquete", "EnvioPaquete")
                        .WithMany()
                        .HasForeignKey("EnvioPaqueteId");

                    b.HasOne("acme_backend.Models.Usuario", "Usuario")
                        .WithMany("compras")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnvioPaquete");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("acme_backend.Models.CompraEstado", b =>
                {
                    b.HasOne("acme_backend.Models.Compra", "Compra")
                        .WithMany("ComprasEstados")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.EstadoCompra", "EstadoCompra")
                        .WithMany("ComprasEstados")
                        .HasForeignKey("EstadoCompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("EstadoCompra");
                });

            modelBuilder.Entity("acme_backend.Models.CompraProducto", b =>
                {
                    b.HasOne("acme_backend.Models.Compra", "Compra")
                        .WithMany("ComprasProductos")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Producto", "Producto")
                        .WithMany("ComprasProductos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("acme_backend.Models.Direccion", b =>
                {
                    b.HasOne("acme_backend.Models.Ciudad", "Ciudad")
                        .WithMany()
                        .HasForeignKey("CiudadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Usuario", null)
                        .WithMany("Direcciones")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Ciudad");
                });

            modelBuilder.Entity("acme_backend.Models.EnvioPaquete", b =>
                {
                    b.HasOne("acme_backend.Models.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direccion");
                });

            modelBuilder.Entity("acme_backend.Models.EstadoCompra", b =>
                {
                    b.HasOne("acme_backend.Models.Compra", null)
                        .WithMany("Estados")
                        .HasForeignKey("CompraId");
                });

            modelBuilder.Entity("acme_backend.Models.LineaCarrito", b =>
                {
                    b.HasOne("acme_backend.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Usuario", "Usuario")
                        .WithMany("LineasCarrito")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("acme_backend.Models.PickUp", b =>
                {
                    b.HasOne("acme_backend.Models.Direccion", "Direccion")
                        .WithMany()
                        .HasForeignKey("DireccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Empresa", "Empresa")
                        .WithMany("Pickups")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direccion");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("acme_backend.Models.Producto", b =>
                {
                    b.HasOne("acme_backend.Models.Compra", null)
                        .WithMany("Productos")
                        .HasForeignKey("CompraId");

                    b.HasOne("acme_backend.Models.Empresa", "Empresa")
                        .WithMany("Productos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.TipoIva", "TipoIva")
                        .WithMany()
                        .HasForeignKey("TipoIvaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("TipoIva");
                });

            modelBuilder.Entity("acme_backend.Models.ProductoFoto", b =>
                {
                    b.HasOne("acme_backend.Models.Producto", "Producto")
                        .WithMany("Fotos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("acme_backend.Models.ProductosRelacionados", b =>
                {
                    b.HasOne("acme_backend.Models.Producto", "producto")
                        .WithMany()
                        .HasForeignKey("productoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("acme_backend.Models.Producto", "productoRel")
                        .WithMany()
                        .HasForeignKey("productoRelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("producto");

                    b.Navigation("productoRel");
                });

            modelBuilder.Entity("acme_backend.Models.Reclamo", b =>
                {
                    b.HasOne("acme_backend.Models.Compra", "Compra")
                        .WithMany("Reclamos")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");
                });

            modelBuilder.Entity("acme_backend.Models.Usuario", b =>
                {
                    b.HasOne("acme_backend.Models.Empresa", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("acme_backend.Models.Categoria", b =>
                {
                    b.Navigation("CategoriasProductos");
                });

            modelBuilder.Entity("acme_backend.Models.Compra", b =>
                {
                    b.Navigation("ComprasEstados");

                    b.Navigation("ComprasProductos");

                    b.Navigation("Estados");

                    b.Navigation("Productos");

                    b.Navigation("Reclamos");
                });

            modelBuilder.Entity("acme_backend.Models.Departamento", b =>
                {
                    b.Navigation("Ciudades");
                });

            modelBuilder.Entity("acme_backend.Models.Empresa", b =>
                {
                    b.Navigation("Categorias");

                    b.Navigation("Pickups");

                    b.Navigation("Productos");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("acme_backend.Models.EstadoCompra", b =>
                {
                    b.Navigation("ComprasEstados");
                });

            modelBuilder.Entity("acme_backend.Models.Producto", b =>
                {
                    b.Navigation("CategoriasProductos");

                    b.Navigation("ComprasProductos");

                    b.Navigation("Fotos");
                });

            modelBuilder.Entity("acme_backend.Models.Usuario", b =>
                {
                    b.Navigation("Direcciones");

                    b.Navigation("LineasCarrito");

                    b.Navigation("compras");
                });
#pragma warning restore 612, 618
        }
    }
}
