namespace OOADSlutprojMEDTDD;
[TestClass]
public sealed class RestaurantManagerTest
{
    [TestMethod]
    public void AddRestaurant_RestaurantIsInList_ReturnTrue()
    {
        // Create manager and restaurant
        var manager = new RestaurantManager();
        var restaurant = new Restaurant("Eskilstuna Style BBQ and Caviar");
        
        // Add restaurant to manager
        manager.AddRestaurant(restaurant);
        
        // Check if restaurant is in manager
        Assert.IsTrue(manager.Restaurants.Contains(restaurant));
    }
}