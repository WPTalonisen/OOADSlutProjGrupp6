namespace OOADSlutprojMEDTDD;

// Percentage discount, e.g. 10% of the subtotal
public class PercentageDiscount : IDiscountStrategy
{
    public decimal Percent { get; }
    public PercentageDiscount(decimal percent)
    {
        // Percent must be between 0 and 100
        if (percent < 0 || percent > 100)
        {
            throw new ArgumentException("The percentage must be between 0 and 100", nameof(percent));
        }

        Percent = percent;
    }

    // Implements the IDiscountStrategy contract
    public decimal CalculateDiscount(decimal subtotal)
    {
        return subtotal * Percent / 100m;
    }
}