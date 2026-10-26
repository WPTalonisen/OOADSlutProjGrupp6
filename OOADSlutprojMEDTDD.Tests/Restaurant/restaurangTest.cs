namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class RestaurangTest
{
    [TestMethod]
    public void Constructor_SetRestaurantId()
    {
        var restaurant = new Restaurant(1); 
        Assert.AreEqual(1, restaurant.Id);
    }
        
    [TestMethod]
    public void Constructor_SetRestaurantName()
    {        
        var restaurant = new Restaurant(1,"Pizzeria Talonisen"); 
        Assert.AreEqual("Pizzeria Talonisen", restaurant.Name);
    }
    
    
    [TestMethod]
    public void Constructor_SetDescription()
    {
        var restaurant = new Restaurant(1, description: "Sug min kuk javenne"); 
        Assert.AreEqual("Sug min kuk javenne", restaurant.Description);
    }
    
    [TestMethod] 
    public void Constructor_SetsNameForOneRestaurant()
    {
        string expectedName1 = "Pizzeria Talonisen";

        var restaurant = new Restaurant(1, expectedName1);

        Assert.AreEqual(expectedName1, restaurant.Name);
    }
    [TestMethod] 
    public void Constructor_SetsNameForTwoRestaurants()
    {
        string expectedName2 = "Pizzeria Hedemora";
        string expectedName1 = "Pizzeria Talonisen";

        var restaurant1 = new Restaurant(1, expectedName1);

        Assert.AreEqual(expectedName1, restaurant1.Name);
        var restaurant2 = new Restaurant(3, expectedName2);
        Assert.AreEqual(expectedName2, restaurant2.Name);
    }
    [TestMethod]
    public void AddDishToMenu()
    {

        
        // Skapa en restaurang och en maträtt
        var restaurant = new Restaurant(4, "Talonisens Spotify och Nattklub");
        var dish = new Dish(4, "Kattspya med bajs");
        
        // Lägg till ny maträtt i restaurangens meny
        restaurant.AddDish(dish);
        
        // Finns rätten i menyn?
        Assert.Contains(dish, restaurant.Menu);

    }

    // Compares subtotal with minimum order amount
    [TestMethod]
    [DataRow(99.99, false)]
    [DataRow(100.00, true)]
    [DataRow(100.01, true)]
    public void MeetsMinimumOrder_ComparesWithMinimum(double subtotal, bool expected)
    {
        var restaurant = new Restaurant(1, "Pizzeria Talonisen", 100m);
        bool result = restaurant.MeetsMinimumOrder((decimal)subtotal);
        Assert.AreEqual(expected, result);
    }
    
}