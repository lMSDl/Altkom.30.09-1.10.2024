﻿namespace Models
{
    public class Product : Entity
    {
        public string? Name { get; set; }
        public float Price { get; set; }
        public Order? Order { get; set; }
    }
}
