namespace OOADSlutprojMEDTDD;


// Calculates price: Subtotal - Discount + Delivery Fee
public class PriceCalculator
{
    public decimal DeliveryFee { get; }
    
    public PriceCalculator(decimal deliveryFee)
    {
        DeliveryFee = deliveryFee;
    }
    

    // Takes Order and DiscountStrategy and calculates the total price
    public decimal CalculateTotal(Order order, IDiscountStrategy discount)
    {
        decimal subtotal = order.Subtotal;
        decimal discountAmount = discount.CalculateDiscount(subtotal);
        return subtotal - discountAmount + DeliveryFee;
    }
}