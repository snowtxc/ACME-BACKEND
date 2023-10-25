using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace acme_backend.Services
{
    public class CategoriaService
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public CategoriaService(ApplicationDbContext db, UserManager<Usuario> userManager)
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

            var categorias = _db.Categorias.Include((cat) => cat.CategoriasProductos).Where((p) => p.EmpresaId == empresaId)
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

            var categorias = _db.Categorias.Include((cat) => cat.CategoriasProductos).Where((p) => p.EmpresaId == empresaId)
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
