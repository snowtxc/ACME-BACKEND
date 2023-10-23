using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos.Departamento;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class DepartamentoService
    {

        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public DepartamentoService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<DepartamentoDTO> createDepartamento(DepartamentoCreateDTO depto)
        {
            var newDepto = new Departamento
            {
                Nombre = depto.Nombre
            };

            _db.Departamentos.Add(newDepto);
            await _db.SaveChangesAsync();

            return _mapper.Map<DepartamentoDTO>(newDepto);
        }

        public async Task<List<DepartamentoDTO>> listarDepartamentos()
        {
            var deptos = await _db.Departamentos
                .Include(d => d.Ciudades)
                .Select(c => _mapper.Map<DepartamentoDTO>(c))
                .ToListAsync();
            return deptos;
        }

        public async Task<DepartamentoDTO> getDepartamentoById(int id)
        {
            var depto = await _db.Departamentos
                .Include(d => d.Ciudades)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (depto != null)
            {
                return _mapper.Map<DepartamentoDTO>(depto);
            }
            else
            {
                throw new Exception("Departamento no encontrado.");
            }

        }

        public async Task<DepartamentoDTO> updateDepartamento(DepartamentoEditDTO departamento)
        {
            var foundDepartamento = await _db.Departamentos
                .Include(d => d.Ciudades)
                .FirstOrDefaultAsync(d => d.Id == departamento.Id);

            if (foundDepartamento == null)
            {
                throw new Exception("Departamento no encontrado.");
            }

            foundDepartamento.Nombre = departamento.Nombre;

            _db.Entry(foundDepartamento).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return _mapper.Map<DepartamentoDTO>(foundDepartamento);
        }

        public async Task deleteDepartamento(int id)
        {
            var depto = await _db.Departamentos.FirstOrDefaultAsync(d => d.Id == id);

            if (depto == null)
            {
                throw new Exception("Departamento no encontrado.");
            }

            _db.Departamentos.Remove(depto);
            await _db.SaveChangesAsync();
        }
    }
}
