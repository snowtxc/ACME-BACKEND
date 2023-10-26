using DataAccessLayer.Models.Dtos;

namespace BusinessLayer.IBLs
{
    public interface IBL_Categoria
    {
        public Task<List<CategoriaDTO>> listarCategorias(string userId);
        public Task<List<CategoriaDTO>> listarCategoriasByEmpresa(int empresaId);
    }
}
