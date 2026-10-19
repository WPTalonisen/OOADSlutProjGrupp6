
namespace OOADSlutprojMEDTDD;

public class Dish
{
    // Property: maträttens namn
    public string Name { get; }
    // Property: maträttens pris
    public decimal Price { get; }
    
    

    // Konstruktor: körs när någon skriver new Dish(...)
    public Dish(string name,  decimal price)
    {
        Name = name;
        Price = price;
    }
}