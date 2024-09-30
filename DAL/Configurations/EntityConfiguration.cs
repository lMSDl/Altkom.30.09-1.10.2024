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
    internal class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property<bool>("IsDeleted");
            builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
        }
    }
}
