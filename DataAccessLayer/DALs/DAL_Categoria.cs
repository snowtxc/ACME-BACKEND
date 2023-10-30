using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DataAccessLayer.IDALs
{
    public class DAL_Categoria: IDAL_Categoria
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public DAL_Categoria(ApplicationDbContext db, UserManager<Usuario> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<CategoriaDTO>> listarCategorias(string userId)
        {
            var userInfo = await _userManager.Users
            .Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.Id == userId);

            if (userInfo == null)
            {
                throw new Exception("Usuario invalido");
            }
            var empresaId = userInfo.Empresa?.Id;
            var empresaInfo = await _db.Empresas.FindAsync(empresaId);
            if (empresaInfo == null)
            {
                throw new Exception("Empresa invalida");
            }

            var categorias = _db.Categorias.Include((cat) => cat.CategoriasProductos).Where((p) => p.EmpresaId == empresaId).Where(c => c.Activo == true)
                .Select((c) => new CategoriaDTO
                {
                    CategoriaNombre = c.Nombre,
                    CategoriaId = c.Id,
                    CantidadProductos = c.CategoriasProductos.Count()
                })
                .ToList();

            return categorias;
        }

        public async Task<List<CategoriaDTO>> listarCategoriasByEmpresa(int empresaId)
        {
            var empresaInfo = await _db.Empresas.FindAsync(empresaId);
            if (empresaInfo == null)
            {
                throw new Exception("Empresa invalida");
            }

            var categorias = _db.Categorias.Include((cat) => cat.CategoriasProductos).Where((p) => p.EmpresaId == empresaId).Where((c) => c.Activo == true)
                .Select((c) => new CategoriaDTO
                {
                    CategoriaNombre = c.Nombre,
                    CategoriaId = c.Id,
                    CantidadProductos = c.CategoriasProductos.Count()
                })
                .ToList();

            return categorias;
        }

    }
}
