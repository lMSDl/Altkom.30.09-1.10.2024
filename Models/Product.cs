namespace Models
{
    public class Product : Entity
    {
        public virtual string? Name { get; set; }
        public virtual float Price { get; set; }
        public virtual Order? Order { get; set; }
    }
}
