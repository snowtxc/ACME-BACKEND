using DataAccessLayer.Models.Dtos.Ciudad;

namespace DataAccessLayer.Models.Dtos.Departamento
{
    public class DepartamentoDTO
    {
        public int? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public IList<CiudadDTO>? Ciudades { get; set; } = new List<CiudadDTO>();
    }
}
