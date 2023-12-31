﻿
namespace DataAccessLayer.Models.Dtos
{
    public class UsuarioListDto
    {
        public string? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;
        
        public string Imagen { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int EmpresaId { get; set; }

        public int Calificaciones { get; set; } = 0;

        public IList<DireccionDTO> Direcciones { get; set; } = new List<DireccionDTO>();
    }
}
