using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class RelatedData
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            Transactions.Run(config, false);
            //config.UseLazyLoadingProxies();
            config.LogTo(Console.WriteLine);

            using (var context = new Context(config.Options))
            {
                //EagerLoading

                //var product = context.Set<Product>().Include(x => x.Order).ThenInclude(x => x.Products).First();
                var product = context.Set<Product>().AsSplitQuery().Include(x => x.Details).Include(x => x.Order).ThenInclude(x => x.Products).First();
            }

            using (var context = new Context(config.Options))
            {
                var product = context.Set<Product>().First();
                //ExpicitLoading
                context.Entry(product).Reference(x => x.Order).Load();
                //context.Set<Order>().Where(x => x.Id == context.Entry(product).Property<int?>("OrderId").CurrentValue).Load();
                context.Entry(product.Order).Collection(x => x.Products).Load();
            }

            using (var context = new Context(config.Options))
            {
                context.Set<Product>().Where(x => x.Id % 2 == 0).Load();
                var order = context.Set<Order>().Where(x => x.Id == 2).Single();
            }

            using (var context = new Context(config.Options))
            {
                var product = context.Set<Product>().First();
                //Lazy loading
                if (product.Order != null)
                    Console.WriteLine();
            }


        }

    }
}
