using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorreoProductosCarritoUser
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

        public async Task enviarEmailUsuariosConProductos()
        {

            DateTime currentDate = DateTime.Now.Date;
            DateTime firstDayOfPreviousMonth = DateTime.Now.AddMonths(-1).Date.AddDays(1 - DateTime.Now.Day);
            DateTime lastDayOfPreviousMonth = DateTime.Now.Date.AddDays(-DateTime.Now.Day);

            var lineasCarrito = _db.LineasCarrito.Include((l) => l.Producto).Include((l) => l.Producto.Empresa).Include((l) => l.Usuario).Where((p) => p.correoEnviado == false).ToList();

            List<LineaCarritoUserDTO> usuariosToSendEmai = new List<LineaCarritoUserDTO>();

            foreach (var lineaCarrito in lineasCarrito)
            {
                var producto = lineaCarrito.Producto;
                var usuario = lineaCarrito.Usuario;
                var empresaCantidadDias = lineaCarrito.Producto.Empresa.DiasEnvioEmail;
                if (currentDate > lineaCarrito.CreatedAt.AddDays(empresaCantidadDias)) {
                    if (usuariosToSendEmai.Exists((u) => u.userId == usuario.Id && producto.Empresa.Id == u.empresaId) == false)
                    {
                        var nuevoItem = new LineaCarritoUserDTO()
                        {
                            userId = usuario.Id,
                            userName = usuario.Nombre,
                            userEmail = usuario.Email,
                            empresaId = producto.Empresa.Id,
                            empresaName = producto.Empresa.Nombre,
                            cantidadProductos = lineasCarrito.Where((item) => item.Usuario.Id == usuario.Id && item.Producto.Empresa.Id == producto.Empresa.Id).Count(),
                        };
                        usuariosToSendEmai.Add(nuevoItem);
                        lineaCarrito.correoEnviado = true;
                        await _db.SaveChangesAsync();
                    }
                }

            }

            foreach (var user in usuariosToSendEmai)
            {
                string path = @"./Templates/NoOlvidesProductos.html";
                string content = File.ReadAllText(path);

                string finalContent = content.Replace("{{ EmpresaName }}", user.empresaName).Replace("{{ productosCount }}",
                    user.cantidadProductos.ToString())
                    .Replace("{{ userName }}", user.userName.ToString());
                _mail.sendMail(user.userEmail, "No olvides tus productos - " + user.empresaName, finalContent);

            }
        }
    }
}
