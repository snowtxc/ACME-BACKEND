namespace DataAccessLayer.Models.Dtos
{
    public class CategoriaDestacadaListDTO
    {
        public int Id { get; set; } = 0;

        public string Nombre { get; set; } = string.Empty;

        public string ImagenUrl { get; set; } = string.Empty;

        public int CategoriaId { get; set; }

    }
}
