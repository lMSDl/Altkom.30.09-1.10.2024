using System.Collections.ObjectModel;

namespace Models
{
    public class Order : Entity
    {
        private string? name;
        private DateTime dateTime;

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
            get => dateTime;
            set
            {
                dateTime = value;
                OnPropertyChanged();
            }
        }
        public /*virtual*/ ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        //public string? Description => $"{DateTime.ToShortDateString()}: {Name}";
        public string? Description { get; }
    }
}