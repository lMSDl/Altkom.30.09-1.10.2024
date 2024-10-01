using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class StoredProcedures
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            Transactions.Run(config, false);

            using var context = new Context(config.Options);

            var multiplier = "-1.1";
            context.Database.ExecuteSqlRaw("EXEC ChangePrice @p0", multiplier);
            context.Database.ExecuteSqlInterpolated($"EXEC ChangePrice {multiplier}");

            var ordersummary = context.Database.SqlQueryRaw<OrderSummary>($"EXEC OrderSummary {3}").ToList();
        }
    }
}
