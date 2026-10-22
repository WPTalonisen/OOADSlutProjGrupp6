namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class DiscountTest
{
    [TestMethod]
    public void PercentageDiscount_TenPercentOf200_Returns20()
    {
        IDiscountStrategy discount = new PercentageDiscount(10m);

        decimal result = discount.CalculateDiscount(200m);
        
        Assert.AreEqual(20m, result);
    }
}