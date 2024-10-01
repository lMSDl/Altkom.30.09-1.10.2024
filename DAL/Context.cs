using DAL.Conventions;
using DAL.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models;

namespace DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        
        public Context() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        public static Func<Context, int, Product> GetProductsByDateOffset { get; } =
            EF.CompileQuery((Context context, int addSeconds) =>
                context.Set<Product>()
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.Id % 2 == 0)
                    .Where(x => x.Order.Id % 2 != 0)
                    .Where(x => x.Order.DateTime < DateTime.Now.AddSeconds(addSeconds))
                    .OrderByDescending(x => x.Order.DateTime)
                    .First()
            );


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferField);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);


            //modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.ClrType == typeof(int))
                .Where(x => x.Name == "Key")
                .ToList().ForEach(x =>
                {
                    x.IsNullable = false;
                    ((IMutableEntityType)x.DeclaringType).SetPrimaryKey(x);
                });

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.ClrType == typeof(string))
                .Where(x => x.PropertyInfo?.CanWrite ?? false)
                .ToList()
                .ForEach(x => x.SetValueConverter(new ObfustacionConverter()));


            /*modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.ClrType == typeof(string))
                .Where(x => x.PropertyInfo?.CanWrite ?? false)
                .Where(x => x.GetMaxLength() is not null)
                .ToList()
                .ForEach(x => x.SetValueConverter(new TurncateConverter(x.GetMaxLength()!.Value)));*/
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            //configurationBuilder.Properties<DateTime>().HavePrecision(5);
            configurationBuilder.Conventions.Add(_ => new DateTimeConvention());
            configurationBuilder.Conventions.Add(_ => new PluralizeTableNameConvention());
            
            //configurationBuilder.Properties<string>().HaveMaxLength(5);

            //configurationBuilder.Conventions.Remove(typeof(KeyDiscoveryConvention));
        }

        public bool RandomFail { get; set; }
        public override int SaveChanges()
        {
            if (RandomFail && new Random((int)DateTime.Now.Ticks).Next(1, 25) == 1)
                throw new Exception();

            return base.SaveChanges();
        }
    }
}
