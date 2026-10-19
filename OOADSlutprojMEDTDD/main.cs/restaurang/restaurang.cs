//Rabatter sker i annan fil, restaurang ska inte veta att det finns rabatter och bara jämföra beställningspris med minimipris

namespace OOADSlutprojMEDTDD;

public class Restaurant
{
    // Property: restaurangens namn
    public string Name { get; }
    
    

    // Konstruktor: körs när någon skriver new Restaurant(...)
    public Restaurant(string name)
    {
        Name = name;
    }
}