using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer.IDALs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IBL_Auth, BL_Auth>();
builder.Services.AddTransient<IBL_Categoria, BL_Categoria>();

builder.Services.AddTransient<IBL_Ciudad, BL_Ciudad>();

builder.Services.AddTransient<IBL_Compra, BL_Compra>();

builder.Services.AddTransient<IBL_Departamento, BL_Departamento>();

builder.Services.AddTransient<IBL_Empresa, BL_Empresa>();

builder.Services.AddTransient<IBL_Estadisticas, BL_Estadisticas>();

builder.Services.AddTransient<IBL_Mail, BL_Mail>();

builder.Services.AddTransient<IBL_Payments, BL_Payments>();

builder.Services.AddTransient<IBL_Pickup, BL_Pickup>();

builder.Services.AddTransient<IBL_Producto, BL_Producto>();

builder.Services.AddTransient<IBL_Reclamo, BL_Reclamo>();


builder.Services.AddTransient<IBL_TipoIVA, BL_TipoIVA>();

builder.Services.AddTransient<IBL_Users, BL_Users>();

var app = builder.Build();













