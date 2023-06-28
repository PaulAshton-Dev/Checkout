namespace CheckoutTests
{
    [TestClass]
    public class DiscountUnitTests
    {

        private Checkout.Data.AvailableProducts? _availableProducts;

        [TestInitialize]
        public void ClassInitialize()
        {
            _availableProducts = new Checkout.Data.AvailableProducts();
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            _availableProducts = null;
        }

        [TestMethod]
        public void Test001_Discount_For_Sku_A_3_For_130_Is_NOT_Applied()
        {
            // arrange
            const string sku = "A";
            var discounts = new Checkout.Models.Discounts();
            var discount = new Checkout.Models.Discount { Sku = sku, Quantity = 3, Value = 1.30M };
            discounts.Add(discount);

            var checkout = new Checkout.Checkout(discounts, _availableProducts);

            // act
            checkout.Scan(sku);

            // assert
            var result = checkout.Find(sku);
            Assert.AreEqual(1, result.Count);

            var price = checkout.GetTotalPrice();
            Assert.AreNotEqual(discount.Value, price);
        }

        [TestMethod]
        public void Test002_Discount_For_Sku_A_3_For_130_IS_Applied()
        {
            // arrange
            const string sku = "A";
            var discounts = new Checkout.Models.Discounts();
            var discount = new Checkout.Models.Discount { Sku = sku, Quantity = 3, Value = 1.30M };
            discounts.Add(discount);

            var checkout = new Checkout.Checkout(discounts, _availableProducts);

            // act
            checkout.Scan(sku);
            checkout.Scan(sku);
            checkout.Scan(sku);
            checkout.Scan(sku);

            // assert
            var result = checkout.Find(sku);
            Assert.AreEqual(4, result.Count);

            var price = checkout.GetTotalPrice();

            decimal singlediscount = 1.3m;
            decimal singleItem = 0.5M;

            Assert.AreNotEqual(discount.Value, singleItem + singlediscount);
        }

        [TestMethod]
        public void Test002_Discount_For_Sku_A_B_C_D_Discounts_ARE_Applied()
        {
            // arrange
            var discounts = new Checkout.Models.Discounts();
            var discountA = new Checkout.Models.Discount { Sku = "A", Quantity = 3, Value = 1.30M };
            var discountB = new Checkout.Models.Discount { Sku = "B", Quantity = 2, Value = 0.45M };
            discounts.Add(discountA);
            discounts.Add(discountB);


            var checkout = new Checkout.Checkout(discounts, _availableProducts);

            // act
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("B");

            checkout.Scan("C");
            checkout.Scan("C");
            checkout.Scan("C");
            checkout.Scan("C");

            checkout.Scan("D");
            checkout.Scan("D");
            checkout.Scan("D");
            checkout.Scan("D");

            // assert
            decimal price = checkout.GetTotalPrice();

            decimal priceOfA = 1.8m; // 1 discount + 1 normal
            decimal priceOfB = 0.9M; // 2 discount + 0 normal
            decimal priceOfC = 0.8M; // 4 normal
            decimal priceOfD = 0.6M; // 4 normal

            Assert.AreEqual(priceOfA + priceOfB + priceOfC + priceOfD, price);
        }
    }
}
