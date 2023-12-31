﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = "";        
        
        public int EmpresaId { get; set; }

        public bool Activo { get; set; } = true;


        public Empresa empresa = null!;

        public List<CategoriaProducto> CategoriasProductos { get; set; }


        public List<CategoriaRelacionada> CategoriasRelacionadas { get; set; }

    }

}
