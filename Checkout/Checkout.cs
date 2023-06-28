using Checkout.Models;

namespace Checkout
{
    interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }

    public class Checkout : ICheckout
    {
        private Discounts _discounts;
        private List<Product> _items { get; set; }

        public Checkout(Discounts discounts)
        {
            _discounts = discounts;
            _items = new List<Product>();
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            var product = Data.AvailableProducts.Items.FirstOrDefault(f => f.SKU == item);

            _items.Add(product);
        }

        public List<Product> Find(string sku)
        {
            return _items.FindAll(f => f.SKU == sku);
        }
    }
}
