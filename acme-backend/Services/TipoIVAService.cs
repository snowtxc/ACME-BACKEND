using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace acme_backend.Services
{
    public class TipoIvaService
    {

        private ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;

        public TipoIvaService(ApplicationDbContext db, UserManager<Usuario> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<TipoIVADTO>> listarTiposIVA()
        {
            var tiposIVA = _db.TiposIva
                .Select((c) => new TipoIVADTO
                {
                    Nombre = c.Nombre,
                    Id = c.Id,
                    Porcentaje = c.Porcentaje,
                })
                .ToList();
            return tiposIVA;
        }
    }
}
