using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BLs
{
    public class BL_Categoria: IBL_Categoria
    {
        private IDAL_Categoria _cat;

        public BL_Categoria(IDAL_Categoria cat)
        {
            _cat = cat;
        }

        public Task<List<CategoriaDTO>> listarCategorias(string userId)
        {
            return _cat.listarCategorias(userId);
        }
        public Task<List<CategoriaDTO>> listarCategoriasByEmpresa(int empresaId)
        {
            return _cat.listarCategoriasByEmpresa(empresaId);
        }
    }
}
