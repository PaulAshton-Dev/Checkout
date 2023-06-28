namespace CheckoutTests
{
    [TestClass]
    public class UnitTest1
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

        [TestMethod, Ignore("todo")]

        public void Test002_Scan_Unknown_Item_Failure()
        {
            // arrange

            // act

            // assert

        }

    }
}