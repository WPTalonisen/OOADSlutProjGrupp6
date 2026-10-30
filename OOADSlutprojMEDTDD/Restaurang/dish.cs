
namespace OOADSlutprojMEDTDD;

public class Dish
{
    // Property: dish ID
    public int Id {get; }
    
    // Property: maträttens namn
    public string Name { get; }
    
    // Property: maträttens pris
    public decimal Price { get; }
    
    // Property: Dish description
    public string Description { get; }
    
    

    // Konstruktor: körs när någon skriver new Dish(...)
    public Dish(int id, string name = "",  decimal price = 0m, string description = "")
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}