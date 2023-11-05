using DataAccessLayer.Models.Dtos.TipoIVA;
using DataAccessLayer.Models.Dtos.Usuario;

namespace DataAccessLayer.Models.Dtos
{
    public class ProductoLista
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string DocumentoPdf { get; set; } = "";
        public string LinkFicha { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public double Precio { get; set; }
        public bool Activo { get; set; }


        public TipoIvaList TipoIva { get; set; }

        public CategoriaLista[] Categorias { get; set; }

        public int CantCalificaciones { get; set; } = 0;

        public List<CalificacionItemList> calificaciones { get; set; } = new List<CalificacionItemList>();

        public int Rate { get; set; } = 0;


        public List<ProductoLista> ProductosRelacionados { get; set; } = new List<ProductoLista>();

        public ImagenList[] Imagenes { get; set; }
    }
}
