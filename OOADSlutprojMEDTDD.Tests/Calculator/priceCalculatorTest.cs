using System.Reflection;

namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class PriceCalculatorTest
{
    [TestMethod]
    public void CalculateTotal_PercentageDiscount_SubtotalMinusDiscountPlusFee()
    {
        var order = new Order();
        order.AddItem(new Dish(1, "En riktigt fet Calzone", 100m), 2);

        IDiscountStrategy discount = new PercentageDiscount(90m);
        var calculator = new PriceCalculator(10m);

        decimal total = calculator.CalculateTotal(order, discount);        


        Assert.AreEqual(30m,total);
    }

    [TestMethod]
    public void CalculateTotal_FixedAmountDiscount_SubtotalMinusDiscountPlusFee()
    {
        var order = new Order();
        order.AddItem(new Dish(1, "Hedemora Special", 100m), 1);

        IDiscountStrategy discount = new FixedAmountDiscount(90m);
        var calculator = new PriceCalculator(10m);
        
        decimal total = calculator.CalculateTotal(order, discount);
        
        Assert.AreEqual(20m,total);
    }

}