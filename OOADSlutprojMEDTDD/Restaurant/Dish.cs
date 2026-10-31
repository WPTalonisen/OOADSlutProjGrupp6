
namespace OOADSlutprojMEDTDD;

public class Dish
{
    // Property: dish ID
    public int Id {get; }
    
    // Property: dish name
    public string Name { get; }
    
    // Property: dish price
    public decimal Price { get; }
    
    // Property: Dish description
    public string Description { get; }
    
    

    // Constructor: runs when someone writes new Dish(...)
    public Dish(int id, string name = "",  decimal price = 0m, string description = "")
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }
}