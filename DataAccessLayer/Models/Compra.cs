﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Enums;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public double CostoTotal { get; set; } = 0;

        public bool Activo { get; set; } = true;


        [Required]
        public MetodoPago MetodoPago { get; set; } 

        [Required]
        public MetodoEnvio MetodoEnvio { get; set; }

        [Required]
        public DateTime Fecha { get; set; }



        public string UsuarioId { get; set; }



        public Usuario Usuario { get; set; }


        public List<EstadoCompra> Estados { get; } = new();

        public List<CompraEstado> ComprasEstados { get; } = new();

        public List<Reclamo> Reclamos { get; set; } = new();

        public EnvioPaquete? EnvioPaquete { get; set; } = null;

        public List<CompraProducto> ComprasProductos { get; set; } = new();

        public int EmpresaId { get; set; }

        [JsonIgnore]
        public Empresa Empresa { get; set; }





    }
}
