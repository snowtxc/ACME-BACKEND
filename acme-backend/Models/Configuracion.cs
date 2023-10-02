﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models
{
    public class Configuracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Required]
        public int PlazosDiasRecordatorioCarrito { get; set; }
    }
}
