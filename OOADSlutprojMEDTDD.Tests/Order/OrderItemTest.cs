namespace OOADSlutprojMEDTDD
{
    [TestClass]
    public sealed class OrderItemTest
    {
        [TestMethod]
        public void Subtotal_ReturnsPriceTimesQuantity()
        {
            var dish = new Dish(70, price: 10m);

        var item = new OrderItem(dish, 3);
            
            // Get Row Subtotal
            decimal subtotal = item.Subtotal;
            
            // Subtotal should be 30
            Assert.AreEqual(30, subtotal);
        }
    }
}