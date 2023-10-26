
using AutoMapper;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Ciudad;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IDALs
{
    public class DAL_Ciudad: IDAL_Ciudad
    {

        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public DAL_Ciudad(ApplicationDbContext db, IMapper mapper)
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

        public async Task<List<CiudadDTO>> listarCiudadesByDepartamento(int departamentoId)
        {
            var ciudades = await _db.Ciudades
                .Where(c => c.DepartamentoId == departamentoId)
                .Select(c => _mapper.Map<CiudadDTO>(c))
                .ToListAsync();
            return ciudades;
        }


    }
}
