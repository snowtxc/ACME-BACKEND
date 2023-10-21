using acme_backend.Models.Dtos;
using acme_backend.Models;
using acme_backend.Db;
using acme_backend.Models.Dtos.Pickup;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class PickupService
    {
        private ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public PickupService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<PickupDto> create(string userLoggedId, PickupCreateDto pickupCreate)
        {
            Usuario? user =  await _db.Usuarios.FindAsync(userLoggedId);
            if(user == null)
            {
                throw new Exception("Usuario logeado no existe");
            }
            Empresa? empresa = user.Empresa;
            if(empresa == null)
            {
                throw new Exception("La empresa no existe");

            }
            Ciudad?  ciudad =   _db.Ciudades.Find(pickupCreate.LocalidadId);
            if(ciudad == null)
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
                Ciudad =  ciudad,
                CiudadId = ciudad.Id,
            };
     
            pickup.Direccion = dir;
            await  _db.SaveChangesAsync();
            PickupDto pickupCreated = _mapper.Map<PickupDto>(pickup);
            return pickupCreated;
        }

        public async Task<List<PickupDto>> list(string userLoggedId)
        {
            Usuario? user = await _db.Usuarios.FindAsync(userLoggedId);
            if (user == null)
            {
                throw new Exception("Usuario logeado no existe");
            }

            ICollection<PickUp> pickups = user.Empresa.Pickups.ToList();
            List<PickupDto> result = new List<PickupDto>();
            foreach(PickUp pickup in pickups)
            {
                result.Add(_mapper.Map<PickupDto>(pickup));
              
            }
            return result;

        }


        public async Task<PickupDto> delete(string userLoggedId, int pickupId)
        {
            Usuario? user = await _db.Usuarios.FindAsync(userLoggedId);
            if (user.Empresa == null)
            {
                throw new Exception("El usuario no está asociado a ninguna empresa");
            }

            Empresa empresa = user.Empresa;

            PickUp? pickupEntity = await _db.Pickups
                .FirstOrDefaultAsync(p => p.EmpresaId == empresa.Id && p.Id == pickupId);

            if (pickupEntity != null)
            {
                _db.Pickups.Remove(pickupEntity);
                await _db.SaveChangesAsync();
                return _mapper.Map<PickupDto>(pickupEntity);
            }
            else
            {
                throw new Exception("Pickup no encontrada");
            }



        }


    }
}
