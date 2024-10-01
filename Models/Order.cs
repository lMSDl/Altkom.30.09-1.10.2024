using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace Models
{
    public class Order : Entity
    {
        private string? name;
        //private string? _name;
        //private string? m_name;
        private DateTime orderDate;

        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        //odpowiednik IsConcurrencyToken w konfiguracji
        //[ConcurrencyCheck]
        public DateTime DateTime
        {
            get => orderDate;
            set
            {
                orderDate = value;
                OnPropertyChanged();
            }
        }
        public /*virtual*/ ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        //public string? Description => $"{DateTime.ToShortDateString()}: {Name}";
        public string? Description { get; }

        public OrderTypes OrderType { get; set; }
        public Parameters Parameters { get; set; }

        public Point DeliveryPoint { get; set; }
    }
}