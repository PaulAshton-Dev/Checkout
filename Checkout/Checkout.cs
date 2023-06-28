using Checkout.Models;

namespace Checkout
{
    interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
    }

    public class Checkout : ICheckout
    {
        private Discounts _discounts;
        private List<Product> _basket { get; set; }

        public Checkout(Discounts discounts)
        {
            _discounts = discounts;
            _basket = new List<Product>();
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var item in _basket)
            {
                totalPrice += item.Price;
            }

            return totalPrice;
        }

        public void Scan(string item)
        {
            var product = Data.AvailableProducts.Items.FirstOrDefault(f => f.SKU == item);

            if (product == null) throw new ArgumentOutOfRangeException("Unknown Sku");

            _basket.Add(product);
        }

        public List<Product> Find(string sku)
        {
            return _basket.FindAll(f => f.SKU == sku);
        }

        public int CountItems()
        {
            return _basket.Count;
        }
    }
}
