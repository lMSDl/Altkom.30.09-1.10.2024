using ConsoleApp;
using DAL;
using Microsoft.EntityFrameworkCore;

var config = new DbContextOptionsBuilder<Context>()
    .UseSqlServer(@"Server=(local);Database=EFCore;Integrated security=true;TrustServerCertificate=true");


using ( var context = new Context(config.Options))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

//ChangeTracker.Run(config);
ChangeTracker.TrackingProxies(config);