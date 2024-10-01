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
    static class Views
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            Transactions.Run(config);

            using var context = new Context(config.Options);

            var summary = context.Set<OrderSummary>().ToList();
        }
    }
}
