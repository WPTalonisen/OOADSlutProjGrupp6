namespace OOADSlutprojMEDTDD;

// Procentuell rabatt, t.ex. 10% av summa
public class PercentageDiscount : IDiscountStrategy
{
    public decimal Percent { get; }
    public PercentageDiscount(decimal percent)
    {
        // Percent must be between 0 and 100
        if (percent < 0 || percent > 100)
        {
            throw new ArgumentException("Procentsatsen måste vara mellan 0 och 100", nameof(percent));
        }

        Percent = percent;
    }

    // Uppfyller IDiscountStrategys kontrakt
    public decimal CalculateDiscount(decimal subtotal)
    {
        return subtotal * Percent / 100m;
    }
}