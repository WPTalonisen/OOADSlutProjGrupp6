namespace OOADSlutprojMEDTDD;

// Fixed discount, e.g. 50 kr
public class FixedAmountDiscount : IDiscountStrategy
{
    
    public decimal Amount { get; }

    public FixedAmountDiscount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("The discount must be greater than 0", nameof(amount)); 
        }

        Amount = amount;
    }

    
    // FixedAmountDiscount can't be bigger than subtotal.
    public decimal CalculateDiscount(decimal subtotal)
    {
        if (subtotal < Amount)
        {
            return subtotal;
        }
        return Amount;
    }
    

}