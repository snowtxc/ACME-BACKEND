using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using DataAccessLayer.DALs;
using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddTransient<IDAL_Compra, DAL_Compra>();
builder.Services.AddTransient<IBL_Compra,  BL_Compra>();


builder.Services.AddTransient<IDAL_CompraEstado, DAL_CompraEstado>();
builder.Services.AddTransient<IDAL_EstadoCompra, DAL_EstadoCompra>();

builder.Services.AddTransient<IDAL_EnvioPaquete, DAL_EnvioPaquete>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
