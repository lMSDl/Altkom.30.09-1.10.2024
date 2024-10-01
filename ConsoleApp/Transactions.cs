using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class Transactions
    {
        public static void Run(DbContextOptionsBuilder config, bool randomFail = true)
        {
            var products = Enumerable.Range(100, 50).Select(x => new Product
            {
                Name = $"Produkt {x}",
                Price = 1.23f * x,
                Details = new ProductDetails { Height = x, Width = 2 * x, Weight = 3 * x }
            }).ToList();
            var orders = Enumerable.Range(1, 5).Select(x => new Order { Name = $"Order {x}", DateTime = DateTime.Now.AddMinutes(-3.21 * x),
            OrderType = (OrderTypes)(x % 3), Parameters = (Parameters) (x % 16),
            DeliveryPoint = new NetTopologySuite.Geometries.Point(51 + 0.1 * x, 19 - 0.1 * x) { SRID = 4326 }
            }).ToList();

            using var context = new Context(config.Options);
            context.RandomFail = randomFail;

            using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                //współdzielenie transakcji
                //context2.Database.UseTransaction(transaction.GetDbTransaction())

                for (int i = 0; i < orders.Count; i++)
                {
                    string savepoint = i.ToString();
                    try
                    {
                        transaction.CreateSavepoint(savepoint);
                        var subproducts = products.Skip(i * 10).Take(10).ToList();

                        foreach (var product in subproducts)
                        {
                            context.Add(product);
                            context.SaveChanges();
                        }

                        var order = orders[i];
                        order.Products = subproducts;
                        context.Add(order);
                        context.SaveChanges();
                    }
                    catch
                    {
                        Console.WriteLine(context.ChangeTracker.DebugView.ShortView);
                        transaction.RollbackToSavepoint(savepoint);
                    }
                    context.ChangeTracker.Clear();
                }
                transaction.Commit();
            }


        }
    }
}
