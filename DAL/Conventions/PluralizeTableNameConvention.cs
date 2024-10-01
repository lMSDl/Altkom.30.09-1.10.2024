using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Conventions
{
    internal class PluralizeTableNameConvention : IModelFinalizingConvention
    {
        public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            modelBuilder.Metadata.GetEntityTypes()
                //umożliwienie stosowania nazw tabel z konfiguracji encji
                .Where(x => x.GetDefaultTableName() == x.GetTableName()) //GetTableName - nazwa aktualna tabeli
                .ToList()
                //GetDefaultTableName - bazwa tabeli pierwotna
                .ForEach(entity => entity.SetTableName(new Pluralize.NET.Core.Pluralizer().Pluralize(entity.GetDefaultTableName())));
        }
    }
}
