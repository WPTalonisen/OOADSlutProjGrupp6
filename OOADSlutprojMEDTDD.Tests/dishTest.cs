namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class dishTest
{
    [TestMethod] 
    public void TestMethod1()
    {
        string expectedName = "Notgården Special";
        decimal expectedPrice = 10m;

        var dish = new Dish(expectedName, expectedPrice);

        Assert.AreEqual(expectedName, dish.Name);
        Assert.AreEqual(expectedPrice, dish.Price);
    }


    
}