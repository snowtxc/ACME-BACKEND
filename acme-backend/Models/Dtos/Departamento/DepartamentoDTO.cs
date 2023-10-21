using acme_backend.Models.Dtos.Ciudad;

namespace acme_backend.Models.Dtos.Departamento
{
    public class DepartamentoDTO
    {
        public int? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public IList<CiudadDTO>? Ciudades { get; set; } = new List<CiudadDTO>();
    }
}
