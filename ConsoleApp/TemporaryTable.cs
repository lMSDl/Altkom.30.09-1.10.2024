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
    internal static class TemporalTable
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            using var context = new Context(config.Options);

            var person = new Person { Name = "Ewa" };

            context.Add(person);
            context.SaveChanges();

            Thread.Sleep(2500);

            person.Name = "Adam";
            context.SaveChanges();


            Thread.Sleep(2500);

            person.Name = "Wojciech";
            context.SaveChanges();

            Thread.Sleep(2500);

            person.Name = "Monika";
            context.SaveChanges();


            context.ChangeTracker.Clear();

            person = context.Set<Person>().First();
            var people = context.Set<Person>().ToList();

            var data = context.Set<Person>().TemporalAll()
                .OrderBy(x => EF.Property<DateTime>(x, "From"))
                .Select(x => new { x, FROM = EF.Property<DateTime>(x, "From"), TO = EF.Property<DateTime>(x, "To") })
                .ToList();

            person = context.Set<Person>().TemporalAsOf(DateTime.UtcNow.AddSeconds(-5)).Single();

            people = context.Set<Person>().TemporalBetween(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(-1)).ToList();
        }
    }
}
