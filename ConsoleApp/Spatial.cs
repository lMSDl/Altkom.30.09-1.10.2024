using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal static class Spatial
    {
        public static void Run(DbContextOptionsBuilder config)
        {
            Transactions.Run(config, false);
            config.LogTo(Console.WriteLine);
            using var context = new Context(config.Options);

            var order = context.Set<Order>().Skip(2).First();

            var point = new Point(51, 19) { SRID = 4326 };

            var distance = point.Distance(order.DeliveryPoint); //dystans * 100 = km

            var polygon = new Polygon(new LinearRing(new Coordinate[] { new Coordinate(51, 19),
                                                                            new Coordinate(50, 20),
                                                                            new Coordinate(51, 21),
                                                                            new Coordinate(52, 20),
                                                                            new Coordinate(51, 19)}))
            { SRID = 4326 };

            var intersect = polygon.Intersects(point);
            intersect = polygon.Intersects(order.DeliveryPoint);


            var orders = context.Set<Order>().Where(x => x.DeliveryPoint.IsWithinDistance(point, 0.5)).ToList();
        }
    }
}
