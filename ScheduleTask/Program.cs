using DataAccessLayer.Db;
using DataAccessLayer.IDALs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScheduleTask;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>

    {
    

        services.AddHostedService<Worker>()
        .AddSingleton<ITasks, Tasks>()
        .AddSingleton<IDAL_Mail, DAL_Mail>();

        services.AddDbContext<ApplicationDbContext>(o => o.UseMySQL(hostContext.Configuration.GetConnectionString("DbConnection")), ServiceLifetime.Singleton);




    })
    .Build();

host.Run();
