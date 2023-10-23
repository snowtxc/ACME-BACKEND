using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos.Compra;
using acme_backend.Models.Dtos.Reclamo;
using acme_backend.Models.Dtos.Usuario;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class ReclamoService
    {
        private ApplicationDbContext _db;

        public ReclamoService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<ReclamoDto>> list()
        {
            List<Reclamo> reclamos = await _db.Reclamos.Include(r => r.Compra).ThenInclude(c => c.Usuario).ToListAsync();
            List<ReclamoDto> result =  new List<ReclamoDto>();
            foreach(Reclamo reclamo in reclamos)
            {
                Compra compra = reclamo.Compra;
                Usuario user = compra.Usuario;

                SortCompra compraSortDto = new SortCompra {  Id =  compra.Id , costoTotal = compra.CostoTotal, metodoEnvio = compra.MetodoEnvio.ToString(), metodoPago =  compra.MetodoPago.ToString() };
                SortUserDto userSortDto = new SortUserDto { Id = user.Id, Tel = user.Celular, Email = user.Email, Imagen = user.Imagen, Nombre = user.Nombre };

                result.Add(new ReclamoDto { Id = reclamo.Id, Description = reclamo.Descripcion, Estado = reclamo.EstadoReclamo.ToString(), Fecha = new DateTime(), compra = compraSortDto, usuario = userSortDto });

               
            }

            return result;
        }
    }
}
