using DAL;
using Microsoft.EntityFrameworkCore;

var config = new DbContextOptionsBuilder<Context>()
    .UseSqlServer(@"Server=(local);Database=EFCore;Integrated security=true;TrustServerCertificate=true")
    .Options;


using ( var context = new Context(config))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}