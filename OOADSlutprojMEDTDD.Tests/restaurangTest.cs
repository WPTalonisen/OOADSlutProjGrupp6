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
    [TestMethod]
    public void AddDishToMenu()
    {

        
        // Skapa en restaurang och en maträtt
        var restaurant = new Restaurant("Talonisens Spotify och Nattklub");
        var dish = new Dish("Down in the Green", 100);
        
        // Lägg till ny maträtt i restaurangens meny
        restaurant.AddDish(dish);
        
        // Finns rätten i menyn?
        Assert.IsTrue(restaurant.Menu.Contains(dish));

    }
    
}