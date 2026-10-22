namespace OOADSlutprojMEDTDD;

// Fixed discount, t.ex. 50kr
public class FixedAmountDiscount : IDiscountStrategy
{
    public decimal Amount { get; }

    public FixedAmountDiscount(decimal amount)
    {
        Amount = amount;
    }

    
    // FixedAmountDiscount cant be bigger than subtotal.
    public decimal CalculateDiscount(decimal subtotal)
    {
        if (subtotal < Amount)
        {
            return subtotal;
        }
        return Amount;
    }
}