using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Models.Dtos
{
    public class CrearProductoDTO
    {

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DocumentoPdf { get; set; } = "";
        public int Precio { get; set; }
        public int TipoIva {  get; set; }

        public int[] Categoria { get; set; }

        public int[] ProductosRelacionados { get; set; } = new int[] {};

        public string[] Imagenes { get; set; }
    }
}
