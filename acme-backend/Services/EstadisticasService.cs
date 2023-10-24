﻿using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Models.Dtos;
using acme_backend.Models.Dtos.Ciudad;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace acme_backend.Services
{
    public class EstadisticasService
    {

        private ApplicationDbContext _db;
        private RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Usuario> _userManager;

        private readonly IMapper _mapper;

        public EstadisticasService(ApplicationDbContext db, IMapper mapper, RoleManager<IdentityRole> roleMan, UserManager<Usuario> userManag)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManag;
            _roleManager = roleMan;
        }

        public async Task<SortEstadisticasDTO> listarEstadisticas()
        {
            var estadisticas = new SortEstadisticasDTO();
            var empresas = _db.Empresas.ToList();
            var userRole = await _roleManager.FindByNameAsync("Usuario");
            if (userRole == null)
            {
                throw new Exception("Rol invalido");
            }
            var usuariosUsers = await _userManager.GetUsersInRoleAsync(userRole.Name);
            var compras = await _db.Compras.ToListAsync();

            estadisticas.EmpresasActivas = empresas.Count;
            estadisticas.UsuariosActivos = usuariosUsers.Count;
            estadisticas.ProductosVendidos = compras.Count;

            return estadisticas;
        }


    }
}
