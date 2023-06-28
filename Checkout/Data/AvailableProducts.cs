using Checkout.Models;

namespace Checkout.Data
{
    public static class AvailableProducts
    {
        public static List<Product> Items { get; set; }

        static AvailableProducts()
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
