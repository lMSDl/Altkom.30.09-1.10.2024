using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Diagnostics;
using System.Threading.Channels;

namespace ConsoleApp
{
    internal static class CompileQuery
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            Transactions.Run(config, false);
            config.LogTo(Console.WriteLine);


            var timer1 = new Stopwatch();

            using (var context = new Context(config.Options))
            {
                timer1.Start();
                var orders = context.Set<Product>()
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.Id % 2 == 0)
                    .Where(x => x.Order.Id % 2 != 0)
                    .Where(x => x.Order.DateTime < DateTime.Now.AddSeconds(1))
                    .OrderByDescending(x => x.Order.DateTime)
                    .First();
                timer1.Stop();
            }


            var timer2 = new Stopwatch();
            using (var context = new Context(config.Options))
            {
                timer2.Start();
                var orders = context.Set<Product>()
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.Id % 2 == 0)
                    .Where(x => x.Order.Id % 2 != 0)
                    .Where(x => x.Order.DateTime < DateTime.Now.AddSeconds(2))
                    .OrderByDescending(x => x.Order.DateTime)
                    .First();
                timer2.Stop();
            }
            var timer3 = new Stopwatch();
            using (var context = new Context(config.Options))
            {
                timer3.Start();
                var orders = context.Set<Product>()
                    .Include(x => x.Order)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.Id % 2 == 0)
                    .Where(x => x.Order.Id % 2 != 0)
                    .Where(x => x.Order.DateTime < DateTime.Now.AddSeconds(3))
                    .OrderByDescending(x => x.Order.DateTime)
                    .First();
                timer3.Stop();
            }

            var timer4 = new Stopwatch();

            using (var context = new Context(config.Options))
            {
                timer4.Start();
                var order = Context.GetProductsByDateOffset(context, 1);
                timer4.Stop();
            }

            var timer5 = new Stopwatch();

            using (var context = new Context(config.Options))
            {
                timer5.Start();
                var order = Context.GetProductsByDateOffset(context, 2);
                timer5.Stop();
            }


            var timer6 = new Stopwatch();

            using (var context = new Context(config.Options))
            {
                timer6.Start();
                var order = Context.GetProductsByDateOffset(context, 3);
                timer6.Stop();
            }

            Console.WriteLine(timer1.ElapsedTicks);
            Console.WriteLine(timer2.ElapsedTicks);
            Console.WriteLine(timer3.ElapsedTicks);
            Console.WriteLine(timer4.ElapsedTicks);
            Console.WriteLine(timer5.ElapsedTicks);
            Console.WriteLine(timer6.ElapsedTicks);
        }
    }
}
