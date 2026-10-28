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
}