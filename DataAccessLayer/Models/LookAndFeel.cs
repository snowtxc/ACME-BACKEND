﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class LookAndFeel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string LogoUrl { get; set; } = "";

        [Required]
        public int? NavBarId { get; set; } = null;

        [Required]
        public int? HomeId { get; set; } = null;

        [Required]
        public string NombreSitio { get; set; } = "";

        [Required]
        public string ColorPrincipal { get; set; } = "";

        [Required]
        public string ColorSecundario { get; set; } = "";

        [Required]
        public string ColorFondo { get; set; } = "";

        [Required]
        public int EmpresaId;

        [Required]
        public Empresa Empresa = null!;

        [Required]
        public int? CategoriaDestacadaId = null;

        [Required]
        public CategoriaDestacada? CategoriaDestacada = null;
    }
}
