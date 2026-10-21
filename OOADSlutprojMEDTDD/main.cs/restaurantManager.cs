namespace OOADSlutprojMEDTDD;

public class RestaurantManager
{
    // Lista med alla restauranger
    private List<Restaurant> _restaurants = new List<Restaurant>();
    
    public IReadOnlyList<Restaurant> Restaurants => _restaurants;

    public void AddRestaurant(Restaurant restaurant)
    {
        _restaurants.Add(restaurant);
    }
}