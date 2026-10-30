//Rabatter sker i annan fil, restaurang ska inte veta att det finns rabatter och bara jämföra beställningspris med minimipris

namespace OOADSlutprojMEDTDD;

public class Restaurant
{
    // Property: restaurangens namn
    public string Name { get; }
    
    // Property: restaurant ID
    public int Id { get; }
    
    // Property: minimum order amount, should reject if less than set amount
    public decimal MinimumOrderAmount { get; }
    
    // Property: restaurant description
    public string Description { get; }
    
    // Privat lista med maträtter
    private List<Dish> _menu = new List<Dish>();
        
    // Publik meny, andra får se men inte röra
    public IReadOnlyList<Dish> Menu => _menu;

    public void AddDish(Dish dish)
    {
        _menu.Add(dish);
    }

    public bool MeetsMinimumOrder(decimal subtotal)
    {
        return subtotal >= MinimumOrderAmount;
    }
    

    // Konstruktor: körs när någon skriver new Restaurant(...)
    public Restaurant( int id, string name = "", decimal minimumOrderAmount = 0m, string description = "")
    {
        Id = id;
        Name = name;
        MinimumOrderAmount = minimumOrderAmount;
        Description = description;
    }
}