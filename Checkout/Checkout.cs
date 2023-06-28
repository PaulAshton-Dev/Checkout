using Checkout.Models;

namespace Checkout
{
    interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }

    internal class Checkout : ICheckout
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
            throw new NotImplementedException();
        }
    }
}
