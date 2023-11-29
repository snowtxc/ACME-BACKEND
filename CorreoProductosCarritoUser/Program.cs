using CorreoProductosCarritoUser;
using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<Worker>()
        .AddSingleton<ITasks, Tasks>()
        .AddSingleton<IDAL_Mail, DAL_Mail>();
        services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(hostContext.Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Singleton);
    })
    .Build();

host.Run();
