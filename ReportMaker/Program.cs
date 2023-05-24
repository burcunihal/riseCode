using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReportMaker;
using ReportMaker.Models;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost; Database=dotnet-6-crud-api; Username=postgres; Password=mysecretpassword");
        services.AddSingleton<AppDbContext>(s => new AppDbContext(optionsBuilder.Options));

        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
