/*
namespace OOADSlutprojMEDTDD
{
    [TestClass]
    public sealed class OrderItemTests
    {
        [TestMethod]
        public void Subtotal_ReturnsPriceTimesQuantity()
        {
            var dish = new Dish(70, "Aids med kolera");

        var item = new OrderItem(dish, 3);
            
            // Get Row Subtotal
            decimal subtotal = item.Subtotal;
            
            // 100kr x 3 should be 300kr
            Assert.AreEqual(100 * 3, subtotal);
        }
    }
}

*/