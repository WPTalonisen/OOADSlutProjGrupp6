namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class RestaurangTest
{
    [TestMethod] 
    public void Constructor_SetsName1()
    {
        string expectedName1 = "Pizzeria Talonisen";

        var restaurant = new Restaurant(expectedName1);

        Assert.AreEqual(expectedName1, restaurant.Name);
    }
    [TestMethod] 
    public void Constructor_SetsName2()
    {
        string expectedName2 = "Pizzeria Hedemora";
        string expectedName1 = "Pizzeria Talonisen";

        var restaurant1 = new Restaurant(expectedName1);

        Assert.AreEqual(expectedName1, restaurant1.Name);
        var restaurant2 = new Restaurant(expectedName2);
        Assert.AreEqual(expectedName2, restaurant2.Name);
    }
    
}