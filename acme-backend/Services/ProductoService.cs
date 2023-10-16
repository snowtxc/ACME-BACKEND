using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace acme_backend.Services
{
    public class ProductoService
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public ProductoService(ApplicationDbContext db, UserManager<Usuario> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<bool> createProduct(CrearProductoDTO data)
        {
            return true;
        }

    }
}
