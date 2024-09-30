using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class ShadowProperty
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            config.LogTo(Console.WriteLine);
            using var context = new Context(config.Options);

            for (int i = 0; i < 17; i++)
            {
                var order = new Order();
                order.DateTime = DateTime.Now;
                var orderProduct = new Product { Name = "P" + i, Price = 1 + i };
                order.Products.Add(orderProduct);

                context.Add(order);
            }

            context.SaveChanges();
            context.ChangeTracker.Clear();

            var product = context.Set<Product>().Skip(5).First();

            var orderId = context.Entry(product).Property<int>("OrderId").CurrentValue;
            Console.WriteLine(orderId);

            orderId = context.Set<Product>().Skip(4).Select(x => EF.Property<int>(x, "OrderId")).First();
            Console.WriteLine(orderId);

            context.Entry(product).Property<int>("OrderId").CurrentValue = 1;
            context.SaveChanges();

            var products = context.Set<Product>().Where(x => EF.Property<int>(x, "OrderId") == 1).ToList();

        }


    }
}
