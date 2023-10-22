﻿using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos.TipoIVA;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class TipoIvaService
    {

        private ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TipoIvaService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TipoIVADTO> createTipoIva(TipoIVADTO tipoIva)
        {
            var newTI = new TipoIva
            {
                Nombre = tipoIva.Nombre,
                Porcentaje = tipoIva.Porcentaje,
            };

            _db.TiposIva.Add(newTI);
            await _db.SaveChangesAsync();

            return _mapper.Map<TipoIVADTO>(newTI);
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

        public async Task<TipoIvaList> getTipoIvaById(int id)
        {
            var ti = await _db.TiposIva.FirstOrDefaultAsync((ti) => ti.Id == id);
            if (ti != null)
            {
                var tipoIvaDTO = new TipoIvaList
                {
                    Id = ti.Id,
                    Nombre = ti.Nombre,
                    Porcentaje = ti.Porcentaje,
                };
                return tipoIvaDTO;
            }
            else
            {
                throw new Exception("Tipo de IVA no encontrado.");
            }
        }

        public async Task<TipoIVADTO> updateTipoIva(TipoIVADTO tipoIva)
        {
            var foundTipoIva = await _db.TiposIva.FirstOrDefaultAsync(ti => ti.Id == tipoIva.Id);

            if (foundTipoIva == null)
            {
                throw new Exception("Tipo de IVA no encontrado.");
            }

            foundTipoIva.Nombre = tipoIva.Nombre;
            foundTipoIva.Porcentaje = tipoIva.Porcentaje;

            _db.Entry(foundTipoIva).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return _mapper.Map<TipoIVADTO>(foundTipoIva);
        }

        public async Task deleteTipoIva(int id)
        {
            var tipoIva = await _db.TiposIva.FirstOrDefaultAsync(ti => ti.Id == id);

            if (tipoIva == null)
            {
                throw new Exception("Tipo de IVA no encontrado.");
            }

            _db.TiposIva.Remove(tipoIva);
            await _db.SaveChangesAsync();
        }
    }
}
