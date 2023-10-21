using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos.Ciudad;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class CiudadService
    {

        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CiudadService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CiudadDTO> createCiudad(CiudadCreateDTO ciudad)
        {
            var foundDepartamento = await _db.Departamentos.FindAsync(ciudad.DepartamentoId);
            if(foundDepartamento == null)
            {
                throw new Exception("Departamento no encontrado.");
            }
            var newCiudad = new Ciudad
            {
                Nombre = ciudad.Nombre,
                Departamento = foundDepartamento,
                DepartamentoId = ciudad.DepartamentoId,
            };

            _db.Ciudades.Add(newCiudad);
            await _db.SaveChangesAsync();

            return _mapper.Map<CiudadDTO>(newCiudad);
        }

        public async Task<List<CiudadDTO>> listarCiudades()
        {
            var ciudades = await _db.Ciudades
                .Select(c => _mapper.Map<CiudadDTO>(c))
                .ToListAsync();
            return ciudades;
        }

        public async Task<CiudadDTO> getCiudadById(int id)
        {
            var ciudad = await _db.Ciudades
                .FirstOrDefaultAsync(d => d.Id == id);

            if (ciudad == null)
            {
                throw new Exception("Ciudad no encontrada.");
            }
            return _mapper.Map<CiudadDTO>(ciudad);
        }

        public async Task<CiudadDTO> updateCiudad(CiudadDTO ciudad)
        {
            var foundCiudad = await _db.Ciudades.FirstOrDefaultAsync(d => d.Id == ciudad.Id);

            if (foundCiudad == null)
            {
                throw new Exception("Ciudad no encontrada.");
            }

            foundCiudad.Nombre = ciudad.Nombre;

            // en caso de querer cambiar la ciudad de depto
            if (ciudad.DepartamentoId != 0)
            {
                var foundDepartamento = await _db.Departamentos
                    .FirstOrDefaultAsync(d => d.Id == ciudad.DepartamentoId);
                if (foundDepartamento == null)
                {
                    throw new Exception("El departamento al que se desea cambiar la ciudad, no fue encontrado.");
                }
                foundCiudad.Departamento = foundDepartamento;
                foundCiudad.DepartamentoId = ciudad.DepartamentoId;
            }

            _db.Entry(foundCiudad).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return _mapper.Map<CiudadDTO>(ciudad);
        }

        public async Task deleteCiudad(int id)
        {
            var ciudad = await _db.Ciudades.FirstOrDefaultAsync(c => c.Id == id);

            if (ciudad == null)
            {
                throw new Exception("Ciudad no encontrada.");
            }

            _db.Ciudades.Remove(ciudad);
            await _db.SaveChangesAsync();
        }
    }
}
