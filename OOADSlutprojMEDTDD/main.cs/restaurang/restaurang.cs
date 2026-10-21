//Rabatter sker i annan fil, restaurang ska inte veta att det finns rabatter och bara jämföra beställningspris med minimipris

namespace OOADSlutprojMEDTDD;

public class Restaurant
{
    // Property: restaurangens namn
    public string Name { get; }
    
    // Privat lista med maträtter
    private List<Dish> _menu = new List<Dish>();
        
    // Publik meny, andra får se men inte röra
    public IReadOnlyList<Dish> Menu => _menu;

    public void AddDish(Dish dish)
    {
        _menu.Add(dish);
    }
    

    // Konstruktor: körs när någon skriver new Restaurant(...)
    public Restaurant(string name)
    {
        Name = name;
    }
}