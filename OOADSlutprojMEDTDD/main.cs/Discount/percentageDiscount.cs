namespace OOADSlutprojMEDTDD;

// Procentuell rabatt, t.ex. 10% av summa
public class PercentageDiscount : IDiscountStrategy
{
    public decimal Percent { get; }
    public PercentageDiscount(decimal percent)
    {
        Percent = percent;
    }

    // Uppfyller IDiscountStrategys kontrakt
    public decimal CalculateDiscount(decimal subtotal)
    {
        return subtotal * Percent / 100m;
    }
}