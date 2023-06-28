namespace Checkout.Models
{
    internal class Discounts
    {
        public List<Discount> Items { get; private set; }

        public Discounts()
        {
            Items = new List<Discount>();
        }

        public void Add(Discount discount)
        {
            Items.Add(discount);
        }
    }

    internal class Discount
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }

        public Discount()
        {
            Sku = string.Empty;
            Quantity = 0;
            Value = 0;
        }
    }
}
