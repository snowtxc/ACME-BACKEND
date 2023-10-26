using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDALs
{
    public interface IDAL_Categoria
    {
        Task<List<CategoriaDTO>> listarCategorias(string userId);
        Task<List<CategoriaDTO>> listarCategoriasByEmpresa(int empresaId);
    }
}
