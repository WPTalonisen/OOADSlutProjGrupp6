namespace OOADSlutprojMEDTDD;

[TestClass]
public class OrderServiceTest
{
    // Order reaches the minimum amount
    [TestMethod]
    public void PlaceOrder_AboveMinimumFee_ReturnsTrue()
    {
        var service = new OrderService();
        var restaurant = new Restaurant(1, "Pizzeria Super Coder Extraordinaire", 100m);
        var order = TestData.CreateOrder();
        order.AddItem(new Dish(1, "En riktigt bra TDD", 100m), 2);
        
        bool accepted = service.PlaceOrder(order, restaurant);
        
        Assert.IsTrue(accepted);
    }

    [TestMethod]
    public void PlaceOrder_AboveMinimumFee_OrderIsInHistory()
    {
        var service = new OrderService();
        var restaurant = new Restaurant(1, "Pizzeria Super Coder Extraordinaire", 100m);
        var order = TestData.CreateOrder();
        order.AddItem(new Dish(1, "En riktigt bra TDD", 100m), 2);
        
        service.PlaceOrder(order, restaurant);
        
        Assert.Contains(order, service.OrderHistory);
    }
    
    // Order does not reach the minimum amount
    [TestMethod]
    public void PlaceOrder_BelowMinimumFee_ReturnsFalse()
    {
        var service = new OrderService();
        var restaurant = new Restaurant(1, "Pizzeria Super Coder Extraordinaire", 1000m);
        var order = TestData.CreateOrder();
        order.AddItem(new Dish(1, "En riktigt bra TDD", 100m), 2);
        
        service.PlaceOrder(order, restaurant);
        
        bool accepted = service.PlaceOrder(order, restaurant);
        
        Assert.IsFalse(accepted);
    }

    [TestMethod]
    public void PlaceOrder_BelowMinimumFee_OrderIsNotInHistory()
    {
        var service = new OrderService();
        var restaurant = new Restaurant(1, "Pizzeria Super Coder Extraordinaire", 112345678m);
        var order = TestData.CreateOrder();
        order.AddItem(new Dish(1, "En riktigt bra TDD", 100m), 2);
        
        service.PlaceOrder(order, restaurant);
        
        Assert.AreEqual(0, service.OrderHistory.Count);
    }
}