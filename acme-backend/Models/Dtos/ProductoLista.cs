using acme_backend.Models.Dtos.Usuario;

namespace acme_backend.Models.Dtos
{
    public class ProductoLista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DocumentoPdf { get; set; } = "";
        public double Precio { get; set; }
        public TipoIvaList TipoIva { get; set; }

        public CategoriaLista[] Categorias { get; set; }

        public int[] ProductosRelacionados { get; set; } = new int[] { };

        public ImagenList[] Imagenes { get; set; }
    }
}
