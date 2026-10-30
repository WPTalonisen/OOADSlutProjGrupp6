namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class OrderFactoryTest
{
    [TestMethod]
    public void CreateOrder_FirstOrder_GetsId1()
    {
        var factory = new OrderFactory();
        var restaurant = new Restaurant(1, "SuperDuper");

        Order order = factory.CreateOrder(restaurant);

        Assert.AreEqual(1, order.Id);
    }

    [TestMethod]
    public void CreateOrder_SecondOrder_GetsId2()
    {
        var factory = new OrderFactory();
        var restaurant = new Restaurant(1, "Pizzeria Talonisen");
        factory.CreateOrder(restaurant);

        Order second = factory.CreateOrder(restaurant);

        Assert.AreEqual(2, second.Id);
    }

    [TestMethod]
    public void CreateOrder_SetsRestaurant()
    {
        var factory = new OrderFactory();
        var restaurant = new Restaurant(1, "Pizzeria Talonisen");

        Order order = factory.CreateOrder(restaurant);

        Assert.AreSame(restaurant, order.Restaurant);
    }
}