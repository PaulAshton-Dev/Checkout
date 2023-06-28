using Checkout.Models;

namespace Checkout.Data
{
    internal class AvailableProducts
    {
        public List<Product> Items { get; set; }

        public AvailableProducts()
        {
            Items = new List<Product>
            {
                new Product { SKU = "A", Price = 0.50M },
                new Product { SKU = "B", Price = 0.30M },
                new Product { SKU = "C", Price = 0.20M },
                new Product { SKU = "D", Price = 0.15M }
            };
        }
    }
}
