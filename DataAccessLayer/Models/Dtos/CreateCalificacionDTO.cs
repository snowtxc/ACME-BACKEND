namespace DataAccessLayer.Models.Dtos
{
    public class CreateCalificacionDTO
    {

        public int ProductoId { get; set; } = 0;
        public string Comentario { get; set; } = "";
        public int Rate { get; set; } = 0;
    }
}
