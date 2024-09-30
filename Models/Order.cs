namespace Models
{
    public class Order : Entity
    {
        public string? Name { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}