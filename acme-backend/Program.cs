using acme_backend.Db;
using acme_backend.Models;
using acme_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("DbConnection")));


builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
db.Database.Migrate();

async Task CreateDefaultRoles(IServiceScope scopeContext)
{
    var roleManager = scopeContext.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Verificar si los roles ya existen
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("Vendedor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Vendedor"));
    }
    if (!await roleManager.RoleExistsAsync("Usuario"))
    {
        await roleManager.CreateAsync(new IdentityRole("Usuario"));
    }
}

await CreateDefaultRoles(scope);


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
