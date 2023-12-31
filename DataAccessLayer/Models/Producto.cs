﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = "";

        [Required]
        public string Descripcion { get; set; } = "";

        public bool Activo { get; set; } = true;


        [Required]
        public string DocumentoPdf { get; set; } = "";

        [Required]
        public double Precio { get; set; } = 0;

        [Required]
        public int  TipoIvaId { get;set;}


        [Column]
        public DateTime CreatedAt { get; set; }

        public TipoIva TipoIva { get; set; } = null!;


        [Required]
        public string LinkFicha { get; set; } = "";

        public ICollection<CategoriaProducto> CategoriasProductos { get; set; }

        public ICollection<CompraProducto> ComprasProductos { get; set; }


        public ICollection<ProductoFoto> Fotos { get; set; }

        public Empresa Empresa { get; set; }

    }
}
