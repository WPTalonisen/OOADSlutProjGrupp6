namespace OOADSlutprojMEDTDD;

public class RestaurantManager
{
    // All restaurants, stored by ID
    private Dictionary<int, Restaurant> _restaurants = new Dictionary<int, Restaurant>();
    
    public IReadOnlyList<Restaurant> Restaurants => _restaurants.Values.ToList();

    public void AddRestaurant(Restaurant restaurant)
    {
        _restaurants.Add(restaurant.Id, restaurant);
    }

    // Gets restaurant by Id, checks if key is in list and returns, else returns null
    public Restaurant? GetById(int id)
    {
        if (_restaurants.ContainsKey(id))
        {
            return _restaurants[id];
        }
        return null;
    }
}