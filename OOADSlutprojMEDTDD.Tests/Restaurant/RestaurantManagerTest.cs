namespace OOADSlutprojMEDTDD;
[TestClass]
public sealed class RestaurantManagerTest
{
    [TestMethod]
    public void AddRestaurant_RestaurantIsInList_ReturnTrue()
    {
        // Create manager and restaurant
        var manager = new RestaurantManager();
        var restaurant = new Restaurant(2, "Eskilstuna Style BBQ and Caviar",0m);
        
        // Add restaurant to manager
        manager.AddRestaurant(restaurant);
        
        // Check if restaurant is in manager
        Assert.IsTrue(manager.Restaurants.Contains(restaurant));
    }
    [TestMethod]
    public void GetById_ReturnsCorrectRestaurant()
    {
        
        // Create manager and restaurant
        var manager = new RestaurantManager();
        var restaurant = new Restaurant(2, "Eskilstuna Style BBQ and Caviar", 0m);
        
        // Add and get by ID
        manager.AddRestaurant(restaurant);
        var found = manager.GetById(2);
        
        // Check if restaurant and ID match
        Assert.AreSame(restaurant, found);
    }

    // Checks what happens if Unknown Restaurant ID, should return Null
    [TestMethod]
    public void GetById_UnknownId_ReturnsNull()
    {
        var manager = new RestaurantManager();
        var found = manager.GetById(1123);
        Assert.IsNull(found);
    }
}