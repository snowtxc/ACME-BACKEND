using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Pickup;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.IDALs
{
    public class DAL_Pickup: IDAL_Pickup
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public DAL_Pickup(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<PickupDto> create(string userLoggedId, PickupCreateDto pickupCreate)
        {
            Usuario? user = await _db.Usuarios.Include(u => u.Empresa).FirstOrDefaultAsync(u => u.Id == userLoggedId);
            if (user == null)
            {
                throw new Exception("Usuario logeado no existe");
            }
            Empresa? empresa = user.Empresa;
            if (empresa == null)
            {
                throw new Exception("La empresa no existe");

            }
            Ciudad? ciudad = _db.Ciudades.Include(c => c.Departamento).FirstOrDefault(c => c.Id == pickupCreate.LocalidadId);
            if (ciudad == null)
            {
                throw new Exception("No existe la ciudad");
            }
            PickUp pickup = new PickUp { Nombre = pickupCreate.Nombre, Telefono = pickupCreate.Telefono, Foto = pickupCreate.Foto, Lat = pickupCreate.Lat, Lng = pickupCreate.Lng, PlazoDiasPreparacion = pickupCreate.PlazosDiasPreparacion, EmpresaId = empresa.Id, Empresa = empresa };

            Direccion dir = new Direccion
            {
                Calle = pickupCreate.Calle,
                CalleEntre1 = pickupCreate.CalleEntre1,
                CalleEntre2 = pickupCreate.CalleEntre2,
                NroPuerta = pickupCreate.NroPuerta,
                Ciudad = ciudad, 
                CiudadId = ciudad.Id,
            };

            pickup.Direccion = dir;
            _db.Pickups.Add(pickup);
            await _db.SaveChangesAsync();

            PickupDto pickupCreated = new PickupDto { Id = pickup.Id, Nombre = pickup.Nombre, Telefono = pickup.Telefono, Foto = pickup.Foto, PlazosDiasPreparacion = pickup.PlazoDiasPreparacion, Lat = pickup.Lat, Lng = pickup.Lng, CiudadNombre = ciudad.Nombre, DepartamentoNombre = ciudad.Departamento.Nombre, Calle = dir.Calle, CalleEntre1 = dir.CalleEntre1, CalleEntre2 = dir.CalleEntre2, NroPuerta = dir.NroPuerta };

            return pickupCreated;
        }

        public async Task<List<PickupDto>> listByEmpresa(int empresaId) {
            var listPickups = await _db.Pickups
                .Include(p => p.Direccion)
                .ThenInclude(d => d.Ciudad)
                .ThenInclude(c => c.Departamento)
                .Where((p) => p.EmpresaId == empresaId).ToListAsync();
            List<PickupDto> result = new List<PickupDto>();
            foreach (PickUp pickup in listPickups)
            {
                if (pickup.Activo)
                {
                    Direccion dirPickup = pickup.Direccion;
                    result.Add(new PickupDto { Id = pickup.Id, Nombre = pickup.Nombre, Telefono = pickup.Telefono, Foto = pickup.Foto, PlazosDiasPreparacion = pickup.PlazoDiasPreparacion, Lat = pickup.Lat, Lng = pickup.Lng, CiudadNombre = dirPickup.Ciudad.Nombre, DepartamentoNombre = dirPickup.Ciudad.Departamento.Nombre, Calle = dirPickup.Calle, CalleEntre1 = dirPickup.CalleEntre1, CalleEntre2 = dirPickup.CalleEntre2, NroPuerta = dirPickup.NroPuerta });
                }
            }
            return result;
        }


        public async Task<List<PickupDto>> list(string userLoggedId)
        {
            Usuario? user = await _db.Usuarios
               .Include(u => u.Empresa)
               .ThenInclude(e => e.Pickups)
               .ThenInclude(p => p.Direccion)
               .ThenInclude(d => d.Ciudad)
               .ThenInclude(c => c.Departamento)
               .FirstOrDefaultAsync(u => u.Id == userLoggedId);
            if (user == null)
            {
                throw new Exception("Usuario logeado no existe");
            }
          
            ICollection<PickUp> pickups = user.Empresa.Pickups;

            List<PickupDto> result = new List<PickupDto>();
            foreach(PickUp pickup in pickups)
            {
                if (pickup.Activo)
                {
                  Direccion dirPickup = pickup.Direccion;
                  result.Add(new PickupDto { Id = pickup.Id, Nombre = pickup.Nombre, Telefono = pickup.Telefono, Foto = pickup.Foto, PlazosDiasPreparacion = pickup.PlazoDiasPreparacion, Lat = pickup.Lat, Lng = pickup.Lng, CiudadNombre = dirPickup.Ciudad.Nombre, DepartamentoNombre = dirPickup.Ciudad.Departamento.Nombre, Calle = dirPickup.Calle , CalleEntre1 = dirPickup.CalleEntre1 , CalleEntre2 = dirPickup.CalleEntre2 , NroPuerta = dirPickup.NroPuerta });
                }
               

            }
            return result;


        }

        public async Task<List<PickupDto>> deletesByIds(int[] pickupsIds)
        {
            List<PickUp> pickupsToDeletes = await _db.Pickups.Where(e => pickupsIds.Contains(e.Id)).ToListAsync();

            _db.Pickups.RemoveRange(pickupsToDeletes);
            _db.SaveChanges();

            List<PickupDto> eliminatedPickups = new List<PickupDto>();
            foreach (PickUp pickupDeleted in pickupsToDeletes)
            {
                eliminatedPickups.Add(_mapper.Map<PickupDto>(pickupDeleted));
            }
            return eliminatedPickups;
        }

    }
}
