﻿using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Compra;
using DataAccessLayer.Models.Dtos.Reclamo;
using DataAccessLayer.Models.Dtos.Usuario;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.IDALs
{
    public class DAL_Reclamo: IDAL_Reclamo
    {
        private ApplicationDbContext _db;

        public DAL_Reclamo(ApplicationDbContext db)
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

                result.Add(new ReclamoDto { Id = reclamo.Id, Description = reclamo.Descripcion, Estado = reclamo.EstadoReclamo.ToString(), Fecha = reclamo.CreatedAt, compra = compraSortDto, usuario = userSortDto });
            }

            return result;
        }

        public async Task crear(ReclamoCreateDTO data)
        {
            var compra = await _db.Compras.Where((item) => item.Id == data.compraId).FirstOrDefaultAsync();
            if (compra == null)
            {
                throw new Exception("Compra invalida");
            }

            var reclamo = new Reclamo();
            reclamo.Descripcion = data.Description;
            reclamo.Activo = true;
            reclamo.EstadoReclamo = Enums.EstadoReclamo.ACTIVO;
            reclamo.Compra = compra;
            reclamo.CreatedAt = DateTime.Now;
            await _db.Reclamos.AddAsync(reclamo);
            await _db.SaveChangesAsync();
        }

        public async Task cerrarReclamo(int reclamoId)
        {
            var reclamo = await _db.Reclamos.Where((item) => item.Id ==reclamoId).FirstOrDefaultAsync();
            if (reclamo == null)
            {
                throw new Exception("Reclamo invalido");
            }
            reclamo.EstadoReclamo = Enums.EstadoReclamo.CERRADO;
            _db.Reclamos.Update(reclamo);
            await _db.SaveChangesAsync();
        }
    }
}
