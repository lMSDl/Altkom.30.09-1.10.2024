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
        public DateTime DateTime
        {
            get => dateTime;
            set
            {
                dateTime = value;
                OnPropertyChanged();
            }
        }
        public ICollection<Product> Products { get; set; } = new ObservableCollection<Product>();
    }
}