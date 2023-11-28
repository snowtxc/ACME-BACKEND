using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Dtos.Empresa;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTask
{
    public class Tasks : ITasks
    {

        private ApplicationDbContext _db;
        private IDAL_Mail _mail;



        public Tasks(ApplicationDbContext db, IDAL_Mail mai)
        {
            _db = db;
            _mail = mai;
        }

        public async Task generarFacturaEmpresas()
        {

            DateTime firstDayOfPreviousMonth = DateTime.Now.AddMonths(-1).Date.AddDays(1 - DateTime.Now.Day);
            DateTime lastDayOfPreviousMonth = DateTime.Now.Date.AddDays(-DateTime.Now.Day);

            List<Empresa> empresas = await _db.Empresas.Include(e => e.Compras.Where(c => c.Fecha >= firstDayOfPreviousMonth && c.Fecha <= lastDayOfPreviousMonth )).ToListAsync();
            foreach (Empresa empresa in empresas)
            {
                var sumaCompras = empresa.Compras.Select(c => c.CostoTotal).Sum();
                var comision = (sumaCompras * 0.02);  //comision del 2%
                string path = @"./Templates/FacturacionMensualEmpresa.html";
                string content = File.ReadAllText(path);

                string finalContent = content.Replace("{{empresaNombre}}",  empresa.Nombre).Replace("{{comisionAPagar}}", 
                    comision.ToString())
                    .Replace("{{ventasTotales}}",sumaCompras.ToString())
                    .Replace("{{month}}", firstDayOfPreviousMonth.Month.ToString()).Replace("{{year}}", firstDayOfPreviousMonth.Year.ToString());

                _mail.sendMail(empresa.Correo, "Resumen Mensual de Costos y Comisiones para " + empresa.Nombre, finalContent);

            }
          
        }
    }
}
