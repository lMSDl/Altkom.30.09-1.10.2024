using ConsoleApp;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Diagnostics;

var config = new DbContextOptionsBuilder<Context>()
    .UseSqlServer(@"Server=(local);Database=EFCore;Integrated security=true;TrustServerCertificate=true", x => x.UseNetTopologySuite());


using ( var context = new Context(config.Options))
{
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

//ChangeTracker.Run(config);
//ChangeTracker.TrackingProxies(config);
//ChangeTracker.ChangedNotfication(config);

//ConcurrencyCheck.Run(config);

//ShadowProperty.Run(config);

//QueryFilters.Run(config);

//Transactions.Run(config);

//RelatedData.Run(config);

//TemporalTable.Run(config);

//CompileQuery.Run(config);

//StoredProcedures.Run(config);

//Views.Run(config);

//Spatial.Run(config);

Json.Run(config);