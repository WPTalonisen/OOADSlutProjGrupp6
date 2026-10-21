namespace OOADSlutprojMEDTDD;

public class OrderItem
{
    // What the dish the row concerns
    public Dish Dish { get; }
    
    // The amount of the dish 
    public int Quantity { get; }
    
    // Price per unit, locked when row is created
    public decimal UnitPrice { get; }
    
    // Subtotal for the row, price per unit x quantity
    public decimal Subtotal => Quantity * UnitPrice;

    public OrderItem(Dish dish, int quantity)
    {
        Dish = dish;
        Quantity = quantity;
        UnitPrice = dish.Price;
    }
}