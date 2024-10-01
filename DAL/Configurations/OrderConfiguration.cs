using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
    internal class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.DateTime).HasField("orderDate").IsConcurrencyToken();

            //builder.Property(x => x.Description).HasComputedColumnSql("Cast([DateTime] as varchar(250)) + ': ' + [Name]");
            builder.Property(x => x.Description).HasComputedColumnSql("[Name] + ' alamakota'", stored: true);

            builder.Property<DateTime>("Timer").HasComputedColumnSql("getdate()");

        }
    }
}
