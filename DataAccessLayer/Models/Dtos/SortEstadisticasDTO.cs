
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Dtos
{
    public class SortEstadisticasDTO
    {
        public int? UsuariosActivos { get; set; } = 0;
        public int? EmpresasActivas { get; set; } = 0;
        public int? ProductosVendidos { get; set; } = 0;

    }
}