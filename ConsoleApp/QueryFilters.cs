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
    internal static class QueryFilters
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            ShadowProperty.Run(config);

            using var context = new Context(config.Options);

            var products = context.Set<Product>().ToList();

            var product = products.Skip(3).First();
            context.Entry(product).Property<bool>("IsDeleted").CurrentValue = true;
            product = products.Where(x => x.Id == 6).Single();
            context.Entry(product).Property<bool>("IsDeleted").CurrentValue = true;
            context.SaveChanges();

            context.ChangeTracker.Clear();

            products = context.Set<Product>().ToList();

            var order = context.Set<Order>().Include(x => x.Products).ToList();

            order = context.Set<Order>().IgnoreQueryFilters().Include(x => x.Products).ToList();

            var mapProducts = context.Set<Product>().IgnoreQueryFilters().Select(x => new { x, deleted = EF.Property<bool>(x, "IsDeleted") }).ToList();
        }
    }
}
