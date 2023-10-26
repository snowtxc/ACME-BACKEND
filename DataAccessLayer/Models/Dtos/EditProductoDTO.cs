using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccessLayer.Models.Dtos
{
    public class EditProductoDTO
    {

        public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string DocumentoPdf { get; set; } = "";
        public int Precio { get; set; } = 0;
        public int TipoIva { get; set; } = 0;

        public int[] Categoria { get; set; } = new int[] { };

        public int[] ProductosRelacionados { get; set; } = new int[] { };

        public ProductoFotoDTO[] CurrentImagenes { get; set; } = new ProductoFotoDTO[] { };


        public string[] Imagenes { get; set; } = new string[] { };
    }
}
