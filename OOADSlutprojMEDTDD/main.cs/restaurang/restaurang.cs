//Rabatter sker i annan fil, restaurang ska inte veta att det finns rabatter och bara jämföra beställningspris med minimipris

namespace OOADSlutprojMEDTDD;

public class Restaurant
{
    // Property: restaurangens namn
    public string Name { get; }
    
    // Int, restaurant ID
    public int Id { get; }
    
    // Decimal, minimum order amount, should reject if less than set amount
    public decimal MinimumOrderAmount { get; }
    
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
    public Restaurant( int id, string name, decimal minimumOrderAmount = 0m)
    {
        Name = name;
        Id = id;
        MinimumOrderAmount = minimumOrderAmount;
    }
}