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

            if (_discounts.Items.Count() > 0)
            {
                var productGroup = _basket.GroupBy(i => i);

                // loop over the group sku and count
                foreach (var pgSku in productGroup)
                {
                    var discountRule = _discounts.Items.FirstOrDefault(f => f.Sku == pgSku.Key.Sku);

                    if (discountRule == null)
                    {
                        // no discount to apply for this sku
                        totalPrice += pgSku.Key.Price * pgSku.Count();
                    }
                    else
                    {
                        // apply discount rules
                        int noOfTimesDiscountApplies = pgSku.Count() / discountRule.Quantity;
                        int remainder = pgSku.Count() % discountRule.Quantity;

                        totalPrice += (noOfTimesDiscountApplies * discountRule.Value) + (remainder * pgSku.Key.Price);
                    }
                }
            }
            else
            {
                // no discounts then easy pricing
                foreach (var item in _basket)
                {
                    totalPrice += item.Price;
                }
            }

            return totalPrice;
        }

        public void Scan(string item)
        {
            var product = Data.AvailableProducts.Items.FirstOrDefault(f => f.Sku == item);

            if (product == null) throw new ArgumentOutOfRangeException("Unknown Sku");

            _basket.Add(product);
        }

        public List<Product> Find(string sku)
        {
            return _basket.FindAll(f => f.Sku == sku);
        }

        public int CountItems()
        {
            return _basket.Count;
        }
    }
}
