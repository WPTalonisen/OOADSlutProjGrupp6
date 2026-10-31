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

    [TestMethod]
    public void PercentageDiscount_FiftyPercentOf100_Returns50()
    {
        IDiscountStrategy discount = new PercentageDiscount(50m);

        decimal result = discount.CalculateDiscount(100m);

        Assert.AreEqual(50m, result);
    }

    [TestMethod]
    public void FixedAmountDiscount_50From200Returns50()
    {
        // 50kr discount
        IDiscountStrategy discount = new FixedAmountDiscount(50m);
        
        // How much is deducted from 200
        decimal result = discount.CalculateDiscount(200m);
        
        Assert.AreEqual(50m, result);
    }

    [TestMethod]
    public void FixedAmountDiscount_DiscountIsLargerThanSubtotal_ReturnSubtotal()
    {
        // 50kr discount but total is only 30kr
        IDiscountStrategy discount = new FixedAmountDiscount(50m);
        
        decimal result = discount.CalculateDiscount(30m);
        
        Assert.AreEqual(30m, result);
        
    }

    // Checks that percentages below 0 or above 100 throw
    [TestMethod]
    public void FixedAmountDiscount_NegativeAmount_ThrowsArgumentException()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new FixedAmountDiscount(-50m));
    }
    
    [TestMethod]
    public void PercentageDiscount_NegativePercent_ThrowsArgumentException()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new PercentageDiscount(-10m));
    }

    [TestMethod]
    public void PercentageDiscount_OverHundredPercent_ThrowsArgumentException()
    {
        Assert.ThrowsExactly<ArgumentException>(() => new PercentageDiscount(150m));
    }
}