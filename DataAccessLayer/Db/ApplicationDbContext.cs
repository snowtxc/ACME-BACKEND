using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DataAccessLayer.Db
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {

        public DbSet<PickUp> Pickups { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }

        public DbSet<CategoriaRelacionada> CategoriaRelacionadas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<CategoriaProducto> CategoriasProductos { get; set; }

        public DbSet<CategoriaDestacada> CategoriasDestacadas { get; set; }

        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Compra> Compras { get; set; }

        public DbSet<CompraEstado> ComprasEstados { get; set; }

        public DbSet<CompraProducto> ComprasProductos { get; set; }

        public DbSet<ProductosRelacionados> ProductosRelacionados { get; set; }

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Direccion> Direcciones { get; set; }


        public DbSet<EnvioPaquete> EnvioPaquetes { get; set; }

        public DbSet<EstadoCompra> EstadosCompras { get; set; }
        public DbSet<LineaCarrito> LineasCarrito{ get; set; }

        public DbSet<LookAndFeel> LooksAndFeels{ get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<ProductoFoto> ProductoFotos { get; set; }

        public DbSet<Reclamo> Reclamos { get; set; }

        public DbSet<RetiroPickup> RetirosPickup { get; set; }

        public DbSet<TipoIva> TiposIva { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Configuracion> Configuraciones { get; set; }











        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoriaProducto>().HasOne(cp => cp.Categoria).WithMany(c => c.CategoriasProductos).HasForeignKey(cp => cp.ProductoId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CategoriaProducto>().HasOne(cp => cp.Producto).WithMany(p => p.CategoriasProductos).HasForeignKey(cp => cp.CategoriaId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompraEstado>().HasOne(ce => ce.EstadoCompra).WithMany(e => e.ComprasEstados).HasForeignKey(ce => ce.EstadoCompraId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CompraProducto>().HasOne(cp => cp.Compra).WithMany(c => c.ComprasProductos).HasForeignKey(cp => cp.CompraId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CompraProducto>().HasOne(cp => cp.Producto).WithMany(p => p.ComprasProductos).HasForeignKey(cp => cp.ProductoId).OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<LineaCarrito>().HasOne(lc => lc.Usuario).WithMany(u => u.LineasCarrito).HasForeignKey(cp => cp.UsuarioId).HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Compra>().HasOne(cp => cp.Empresa).WithMany(p => p.Compras).HasForeignKey(cp => cp.EmpresaId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Empresa>()
              .HasMany(e => e.Usuarios)
              .WithOne(u => u.Empresa)
              .HasForeignKey(u => u.EmpresaId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Compra>()
            .HasMany(cp => cp.ComprasEstados)
            .WithOne(e => e.compra)
            .HasForeignKey(e => e.CompraId)
            .OnDelete(DeleteBehavior.NoAction)
            ;



            modelBuilder.Entity<CategoriaRelacionada>()
            .HasOne(cr => cr.Categoria)
            .WithMany()
            .HasForeignKey(cr => cr.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CategoriaRelacionada>()
                .HasOne(cr => cr.CategoriaRel)
                .WithMany()
                .HasForeignKey(cr => cr.CategoriaRelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductosRelacionados>()
            .HasOne(cr => cr.producto)
            .WithMany()
            .HasForeignKey(cr => cr.productoId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductosRelacionados>()
                .HasOne(cr => cr.productoRel)
                .WithMany()
                .HasForeignKey(cr => cr.productoRelId)
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
