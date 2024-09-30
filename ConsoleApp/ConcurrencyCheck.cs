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
    internal static class ConcurrencyCheck
    {

        public static void Run(DbContextOptionsBuilder config)
        {
            config.LogTo(Console.WriteLine);

            using (var context = new Context(config.Options))
            {
                var order = new Order();
                order.DateTime = DateTime.Now;


                context.Add(order);
                context.SaveChanges();

                //order.Name = "1";
                order.DateTime = DateTime.Now.AddDays(-100);
                context.SaveChanges();


                var product = new Product() { Order = order, Name = "marchewka", Price = 15 };
                context.Add(product);
                context.SaveChanges();

                product.Price = product.Price * 1.1f;
                var saved = false;
                while(!saved)
                {
                    try
                    {
                        context.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            //wartości jakie chcemy wprowadzić do bazy
                            var currentValues = entry.CurrentValues;
                            //wartości jakie pobraliśmy z bazy
                            var originalValues = entry.OriginalValues;
                            //wartości aktualne w bazie
                            var databaseValues = entry.GetDatabaseValues();

                            switch (entry.Entity)
                            {
                                case Product:
                                    var property = currentValues.Properties.Single(x => x.Name == nameof(Product.Price));
                                    var currentValue = (float)currentValues[property];
                                    var originalValue = (float)originalValues[property];
                                    var databaseValue = (float)databaseValues[property];

                                    currentValue = databaseValue + (currentValue - originalValue);

                                    currentValues[property] = currentValue;

                                    break;
                            }

                            entry.OriginalValues.SetValues(databaseValues);
                        }
                    }
                }

            }


        }
    }
}
