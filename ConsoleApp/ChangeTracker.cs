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
    internal static class ChangeTracker
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            using var context = new Context(config.Options);

            //AutoDetectChangesEnabled dziala w przypadku wywołania SaveChanges, Local, Entry
            context.ChangeTracker.AutoDetectChangesEnabled = true;

            Order order = new Order() { DateTime = DateTime.Now, Name = "Zamówienie #124" };
            Product product = new Product() { Name = "Marchewka", Price = 4.23f };
            order.Products.Add(product);

            Console.WriteLine("Order przed dodaniem do kontekstu: " + context.Entry(order).State);
            Console.WriteLine("Product przed dodaniem do kontekstu: " + context.Entry(product).State);

            //context./*Set<Order>().*/Attach(order);
            context./*Set<Order>().*/Add(order);

            Console.WriteLine("Order po dodaniu do kontekstu: " + context.Entry(order).State);
            Console.WriteLine("Product po dodaniu do kontekstu: " + context.Entry(product).State);

            context.SaveChanges();

            Console.WriteLine("Order po zapisie do bazy: " + context.Entry(order).State);
            Console.WriteLine("Product po zapisie do bazy: " + context.Entry(product).State);

            order.DateTime = DateTime.Now.AddMinutes(-5);
            order.Products.Add(new Product { Name = "Kapusta", Price = 5 });

            Console.WriteLine("Order po edycji order: " + context.Entry(order).State);
            Console.WriteLine("Order.DateTime po edycji order: " + context.Entry(order).Property(x => x.DateTime).IsModified);
            Console.WriteLine("Order.Name po edycji order: " + context.Entry(order).Property(x => x.Name).IsModified);
            Console.WriteLine("Order.Products po edycji order: " + context.Entry(order).Collection(x => x.Products).IsModified);

            Console.WriteLine("Product po edycji order: " + context.Entry(product).State);

            context.Remove(product);

            Console.WriteLine("Product po usunięciu: " + context.Entry(product).State);

            context.SaveChanges();

            Console.WriteLine("Order po zapisie do bazy: " + context.Entry(order).State);
            Console.WriteLine("Product po zapisie do bazy: " + context.Entry(product).State);


            context.ChangeTracker.Clear();

            product = new Product() { Name = "Marchewka", Price = 4.23f, Order = new Order { Id = 1 } };
            context.Attach(product);
            Console.WriteLine("Product przed zapisem do bazy: " + context.Entry(product).State);

            context.SaveChanges();
            Console.WriteLine("Product po zapisie do bazy: " + context.Entry(product).State);

            context.ChangeTracker.Clear();

            context.Attach(order);
            order.Products.Remove(order.Products.First());
            order.Name = "alamakota";

            context.Entry(order).Property(x => x.Name).IsModified = false;

            context.Entry(order).State = EntityState.Unchanged;

            context.ChangeTracker.DetectChanges();

            Console.WriteLine("Order: " + context.Entry(order).State);
            Console.WriteLine("Order.Name: " + context.Entry(order).Property(x => x.Name).IsModified);
            Console.WriteLine("Order.Products: " + context.Entry(order).Collection(x => x.Products).IsModified);

            context.SaveChanges();

            order = new Order() { DateTime = DateTime.Now, Name = "Zamówienie #125" };
            product = new Product() { Name = "Banan", Price = 1.23f };
            order.Products.Add(product);

            context.Add(order);

            Console.WriteLine("Po dodaniu do kontekstu:");
            Console.WriteLine(context.ChangeTracker.DebugView.ShortView);
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);


            context.SaveChanges();

            Console.WriteLine("Po zapisie:");
            Console.WriteLine(context.ChangeTracker.DebugView.ShortView);
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            order.DateTime = DateTime.Now.AddMinutes(30);
            product.Price += 4;

            context.ChangeTracker.DetectChanges();

            Console.WriteLine("Po edycji:");
            Console.WriteLine(context.ChangeTracker.DebugView.ShortView);
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        }

        public static void TrackingProxies(DbContextOptionsBuilder config)
        {
            //Włączenie śledzenia zmian na podstawie proxy - wymaga specjalnego tworzenia obiektów (context.CreateProxy) i virtualizacji właściwości encji
            config.UseChangeTrackingProxies();

            using var context = new Context(config.Options);

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            //var order = new Order();
            var order = context.CreateProxy<Order>();
            order.Name = "alamakota";
            var product = context.CreateProxy<Product>(x => { x.Price = 1; x.Name = "pomarańcza"; });
            order.Products.Add(product);

            context.Add(order);

            context.SaveChanges();

            order.Name = "kotmaale";

            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        }

    }
}
