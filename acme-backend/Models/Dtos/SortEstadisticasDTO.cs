
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace acme_backend.Models.Dtos
{
    public class SortEstadisticasDTO
    {
        public int? UsuariosActivos { get; set; } = 0;
        public int? EmpresasActivas { get; set; } = 0;
        public int? ProductosVendidos { get; set; } = 0;

    }
}