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
    public class DAL_EnvioPaquete : IDAL_EnvioPaquete
    {
        private ApplicationDbContext _db;

        public DAL_EnvioPaquete(ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task<EnvioPaquete?> getByNroRastreo(string trackingNumber)
        {
            EnvioPaquete? ep =  await _db.EnvioPaquetes.Include(c => c.Compra).Where(ep => ep.CodigoSeguimiento == trackingNumber).FirstOrDefaultAsync();
            return ep;
           
        }
    }
}
