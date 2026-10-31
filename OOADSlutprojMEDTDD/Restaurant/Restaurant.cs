// Discounts are handled elsewhere. Restaurant only compares the order price with its minimum amount

namespace OOADSlutprojMEDTDD;

public class Restaurant
{
    // Property: restaurant name
    public string Name { get; }
    
    // Property: restaurant ID
    public int Id { get; }
    
    // Property: minimum order amount, should reject if less than set amount
    public decimal MinimumOrderAmount { get; }
    
    // Property: restaurant description
    public string Description { get; }
    
    // Private list of dishes
    private List<Dish> _menu = new List<Dish>();
        
    // Public menu, others can read but not change it
    public IReadOnlyList<Dish> Menu => _menu;

    public void AddDish(Dish dish)
    {
        _menu.Add(dish);
    }

    public bool MeetsMinimumOrder(decimal subtotal)
    {
        return subtotal >= MinimumOrderAmount;
    }
    

    // Constructor: runs when someone writes new Restaurant(...)
    public Restaurant( int id, string name = "", decimal minimumOrderAmount = 0m, string description = "")
    {
        Id = id;
        Name = name;
        MinimumOrderAmount = minimumOrderAmount;
        Description = description;
    }
}