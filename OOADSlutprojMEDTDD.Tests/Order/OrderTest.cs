namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class OrderTest
{
    [TestMethod] 
    public void Subtotal_SumsAllItems()
    {
        var order = TestData.CreateOrder();
        var pizza = new Dish(1, "Kebabpizza utan kebab", 100m);
        var drink = new Dish(10, "Coca Cola Lutfisk", 50m);
        
        order.AddItem(pizza, 2);
        order.AddItem(drink, 1);

        Assert.AreEqual(250, order.Subtotal);

    }

    [TestMethod]
    public void Constructor_SetsId()
    {
        var restaurant = new Restaurant(1, "Bibi Gyros och Hummus");
        var order = new Order(1,restaurant);
        Assert.AreEqual(1, order.Id);
        
    }

    [TestMethod]
    public void Constructor_SetsResaurant()
    {
        var restaurant = new Restaurant(1, "Bibi Gyros och Hummus");
        var order = new Order(1, restaurant);

        Assert.AreEqual(restaurant, order.Restaurant);
    }
    
    [TestMethod]
    public void SetPrice_SetsDeliveryFee()
    {
        var order = TestData.CreateOrder();

        order.SetPrice(50m, 230m);

        Assert.AreEqual(50m, order.DeliveryFee);
    }
    
    [TestMethod]
    public void SetPrice_SetsTotalPrice()
    {
        var order = TestData.CreateOrder();

        order.SetPrice(50m, 230m);

        Assert.AreEqual(230m, order.TotalPrice);
    }
    
    [TestMethod]
    public void NewOrder_IsNotPaid()
    {
        var order = TestData.CreateOrder();

        Assert.IsFalse(order.IsPaid);
    }

    [TestMethod]
    public void MarkAsPaid_OrderIsPaid()
    {
        var order = TestData.CreateOrder();

        order.MarkAsPaid();

        Assert.IsTrue(order.IsPaid);
    }
}