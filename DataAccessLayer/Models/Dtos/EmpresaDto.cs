﻿namespace DataAccessLayer.Models.Dtos
{
    public class EmpresaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Telefono {  get; set; } = "";

        public string Direccion { get; set; } = "";

        public string Correo { get; set; } = "";

        public string Imagen { get; set; } = "";

        public double CostoEnvio { get; set; } = 0;

        public string Wallet { get; set; } = "";


    }
}
