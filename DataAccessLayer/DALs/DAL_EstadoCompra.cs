using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_EstadoCompra : IDAL_EstadoCompra
    {
        private ApplicationDbContext _db;

        public DAL_EstadoCompra(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<EstadoCompra> getById(int id)
        {
            EstadoCompra? estadoCompra = await  _db.EstadosCompras.Where(ec => ec.Id == id).FirstOrDefaultAsync();
            if (estadoCompra == null)
            {
                return null;
            }
            return estadoCompra;
              
        }
    }
}
