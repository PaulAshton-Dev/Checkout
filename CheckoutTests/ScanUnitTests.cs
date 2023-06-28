using Checkout.Data;

namespace CheckoutTests
{
    [TestClass]
    public class ScanUnitTests
    {
        [TestMethod]
        public void Test001_Scan_Item_Success()
        {
            // arrange
            const string sku = "A";
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts());

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
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts());

            // act
            checkout.Scan(sku);

            // assert Exception thrown
        }

        [TestMethod]
        public void Test003_Scan_Multiple_Item_Success()
        {
            // arrange
            const string sku = "A";
            var checkout = new Checkout.Checkout(new Checkout.Models.Discounts());

            // act
            foreach (var scanItem in AvailableProducts.Items)
            {
                checkout.Scan(scanItem.SKU);
                checkout.Scan(scanItem.SKU);
            }


            // assert
            var result = checkout.CountItems();
            Assert.AreEqual(AvailableProducts.Items.Count * 2, result);
        }
    }
}