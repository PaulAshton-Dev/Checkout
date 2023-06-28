﻿using Checkout.Models;

namespace Checkout.Data
{
    public class AvailableProducts
    {
        public List<Product> Items { get; set; }

        public AvailableProducts()
        {
            Items = new List<Product>
            {
                new Product { Sku = "A", Price = 0.50M },
                new Product { Sku = "B", Price = 0.30M },
                new Product { Sku = "C", Price = 0.20M },
                new Product { Sku = "D", Price = 0.15M }
            };
        }
    }
}
