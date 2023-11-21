using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.IDALs;
using BusinessLayer.IBLs;
using BusinessLayer.BLs;
using DataAccessLayer.DALs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

  string secret = "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr";

  var key = Encoding.ASCII.GetBytes(secret); // Reemplaza con tu propia clave secreta
  builder.Services.AddAuthentication(options =>
  {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
      options.RequireHttpsMetadata = false; // En producci�n, configura esto como true para usar HTTPS
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false, // Reemplaza con tu emisor si es necesario
          ValidateAudience = false // Reemplaza con tu audiencia si es necesario
      };
  });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddTransient<IDAL_Auth, DAL_Auth>();
builder.Services.AddTransient<IBL_Auth, BL_Auth>();

builder.Services.AddTransient<IDAL_Categoria, DAL_Categoria>();
builder.Services.AddTransient<IBL_Categoria, BL_Categoria>();

builder.Services.AddTransient<IDAL_Ciudad, DAL_Ciudad>();
builder.Services.AddTransient<IBL_Ciudad, BL_Ciudad>();

builder.Services.AddTransient<IDAL_Carrito, DAL_Carrito>();
builder.Services.AddTransient<IBL_Carrito, BL_Carrito>();

builder.Services.AddTransient<IDAL_Departamento, DAL_Departamento>();
builder.Services.AddTransient<IBL_Departamento, BL_Departamento>();

builder.Services.AddTransient<IDAL_Empresa, DAL_Empresa>();
builder.Services.AddTransient<IBL_Empresa, BL_Empresa>();

builder.Services.AddTransient<IDAL_Estadisticas, DAL_Estadisticas>();
builder.Services.AddTransient<IBL_Estadisticas, BL_Estadisticas>();

builder.Services.AddTransient<IDAL_Mail, DAL_Mail>();
builder.Services.AddTransient<IBL_Mail, BL_Mail>();

builder.Services.AddTransient<IDAL_Pickup, DAL_Pickup>();
builder.Services.AddTransient<IBL_Pickup, BL_Pickup>();

builder.Services.AddTransient<IDAL_Producto, DAL_Producto>();
builder.Services.AddTransient<IBL_Producto, BL_Producto>();

builder.Services.AddTransient<IDAL_Reclamo, DAL_Reclamo>();
builder.Services.AddTransient<IBL_Reclamo, BL_Reclamo>();

builder.Services.AddTransient<IDAL_TipoIVA, DAL_TipoIVA>();
builder.Services.AddTransient<IBL_TipoIVA, BL_TipoIVA>();

builder.Services.AddTransient<IDAL_User, DAL_User>();
builder.Services.AddTransient<IBL_Users, BL_Users>();

builder.Services.AddTransient<IBL_Compra,BL_Compra>();
builder.Services.AddTransient<IDAL_Compra, DAL_Compra>();




builder.Services.AddTransient<IDAL_CompraEstado, DAL_CompraEstado>();
builder.Services.AddTransient<IDAL_EstadoCompra, DAL_EstadoCompra>();

builder.Services.AddTransient<IDAL_EnvioPaquete, DAL_EnvioPaquete>();

var app = builder.Build();

var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.Migrate(); 

async Task CreateDefaultRoles(IServiceScope scopeContext)
{
    var roleManager = scopeContext.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Vendedor", "Usuario" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

await CreateDefaultRoles(scope);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


