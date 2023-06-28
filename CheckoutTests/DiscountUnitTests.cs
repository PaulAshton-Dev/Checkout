namespace CheckoutTests
{
    [TestClass]
    public class DiscountUnitTests
    {
        [TestMethod]
        public void Test001_Discount_For_Sku_A_3_For_130_Is_NOT_Applied()
        {
            // arrange
            const string sku = "A";
            var discounts = new Checkout.Models.Discounts();
            var discount = new Checkout.Models.Discount { Sku = sku, Quantity = 3, Value = 1.30M };
            discounts.Add(discount);

            var checkout = new Checkout.Checkout(discounts);

            // act
            checkout.Scan(sku);

            // assert
            var result = checkout.Find(sku);
            Assert.AreEqual(1, result.Count);

            var price = checkout.GetTotalPrice();
            Assert.AreNotEqual(discount.Value, price);
        }
    }
}
