﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    internal class ProductConfiguration : EntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.ToTable("OrderProducts");

            builder.HasOne(x => x.Order).WithMany(x => x.Products);//.IsRequired();

            builder
                //.Property(x => x.Timestamp)
                .Property<byte[]>("Timestamp")
                .IsRowVersion();

            builder.HasOne(x => x.Details).WithOne().HasForeignKey<ProductDetails>(x => x.Id);
        }
    }
}
