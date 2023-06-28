namespace CheckoutTests
{
    [TestClass]
    public class ScanUnitTests
    {
        private Checkout.Data.AvailableProducts? _availableProducts;

        [TestInitialize]
        public void TestInitialize()
        {
            _availableProducts = new Checkout.Data.AvailableProducts();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _availableProducts = null;
        }

        [TestMethod]
        public void Test001_Scan_Item_Success()
        {
            // arrange
            const string sku = "A";
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts(), _availableProducts);

            // act
            checkout.Scan(sku);

            // assert
            var result = checkout.Find(sku);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An unknown product has been scanned")]
        public void Test002_Scan_Unknown_Item_Failure()
        {
            // arrange
            const string sku = "Z";
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts(), _availableProducts);

            // act
            checkout.Scan(sku);

            // assert Exception thrown
        }

        [TestMethod]
        public void Test003_Scan_Multiple_Items_Success()
        {
            // arrange
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts(), _availableProducts);

            // act
            foreach (var scanItem in _availableProducts.Items)
            {
                checkout.Scan(scanItem.Sku);
                checkout.Scan(scanItem.Sku);
            }

            // assert
            var result = checkout.CountItems();
            Assert.AreEqual(_availableProducts.Items.Count * 2, result);
        }

        [TestMethod]
        public void Test004_Scan_Multiple_Items_Get_The_Price_Success()
        {
            // arrange
            decimal expectedTotalPrice = 0;
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts(), _availableProducts);

            // act
            foreach (var scanItem in _availableProducts.Items)
            {
                checkout.Scan(scanItem.Sku);
                expectedTotalPrice += scanItem.Price;

                checkout.Scan(scanItem.Sku);
                expectedTotalPrice += scanItem.Price;
            }

            // assert
            var result = checkout.CountItems();
            Assert.AreEqual(_availableProducts.Items.Count * 2, result);

            var price = checkout.GetTotalPrice();
            Assert.AreEqual(expectedTotalPrice, price);
        }
    }
}