using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccessLayer.Db;
using DataAccessLayer.Models;
using DataAccessLayer.IDALs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseMySQL("Server=localhost;Database=acme;User=root;Password=;"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

string secret = "JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr";

var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddTransient<IDAL_Auth, DAL_Auth>();

builder.Services.AddTransient<IDAL_Categoria, DAL_Categoria>();

builder.Services.AddTransient<IDAL_Ciudad, DAL_Ciudad>();

builder.Services.AddTransient<IDAL_Departamento, DAL_Departamento>();

builder.Services.AddTransient<IDAL_Empresa, DAL_Empresa>();

builder.Services.AddTransient<IDAL_Estadisticas, DAL_Estadisticas>();

builder.Services.AddTransient<IDAL_Mail, DAL_Mail>();

builder.Services.AddTransient<IDAL_Pickup, DAL_Pickup>();

builder.Services.AddTransient<IDAL_Producto, DAL_Producto>();

builder.Services.AddTransient<IDAL_Reclamo, DAL_Reclamo>();

builder.Services.AddTransient<IDAL_TipoIVA, DAL_TipoIVA>();

builder.Services.AddTransient<IDAL_User, DAL_User>();




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


