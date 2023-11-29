namespace DataAccessLayer.Models.Dtos
{
    public class LookAndFeelDTO
    {
        public string NombreSitio { get; set; } = "";

        public string LogoUrl { get; set; } = "";

        public string ColorPrincipal { get; set; } = "";

        public string ColorSecundario { get; set; } = "";

        public string ColorFondo { get; set; } = "";

        public int PlazoDias { get; set; } = 1;


        public CategoriaDestacadaListDTO? CategoriaDestacada { get; set; } = null;
    }
}
