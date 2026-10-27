namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class OrderTest
{
    [TestMethod] 
    public void Subtotal_SumsAllItems()
    {
        var order = new Order();
        var pizza = new Dish(1, "Kebabpizza utan kebab", 100m);
        var drink = new Dish(10, "Coca Cola Lutfisk", 50m);
        
        order.AddItem(pizza, 2);
        order.AddItem(drink, 1);

        Assert.AreEqual(250, order.Subtotal);

    }
    
}