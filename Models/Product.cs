using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Product : Entity
    {
        private ILazyLoader _lazyLoader;
        public Product() { }
        public Product(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public string? Name { get; set; }
        public float Price { get; set; }


        //public int? OrderId { get; set; }
        //public /*virtual*/ Order? Order { get; set; }

        private Order? order;
        public Order? Order
        {
            get
            {
                if (order == null)
                {
                    try
                    {
                        _lazyLoader?.Load(this, ref order);
                    }
                    catch
                    {
                        order = null;
                    }
                }
                return order;
            }
            set => order = value;
        }

        //odpowiednik IsRowVersion w konfiguracji
        //[Timestamp]
        //public byte[] Timestamp { get; }

        public ProductDetails? Details { get; set; }

    }
}
