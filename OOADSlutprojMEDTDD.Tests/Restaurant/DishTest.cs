namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class DishTest
{

    [TestMethod]
    public void Constructor_SetDishId()
    {
        var dish = new Dish(1); 
        Assert.AreEqual(1, dish.Id);
    }
        
    [TestMethod]
    public void Constructor_SetDishName()
    {        
        var dish = new Dish(1, "Random Pizza"); 
        Assert.AreEqual("Random Pizza", dish.Name);
    }
    
    [TestMethod]
    public void Constructor_SetDishPrice()
    {
        var dish = new Dish(1, price: 99m);
        Assert.AreEqual(99m, dish.Price);
    }
    
    [TestMethod]
    public void Constructor_SetDishDescription()
    {
        var dish = new Dish(1, description: "Random Pizza");
        Assert.AreEqual("Random Pizza", dish.Description);
    }

    
    [TestMethod] 
    public void Constructor_DishSetAll()
    {
        int expectedId = 60;
        string expectedName = "Notgården Special";
        decimal expectedPrice = 10m;
        string expectedDescription = "Random Pizza";
        
        
        var dish = new Dish(expectedId, expectedName, expectedPrice, expectedDescription);
        
        Assert.AreEqual((expectedId, expectedName, expectedPrice, expectedDescription), (dish.Id, dish.Name, dish.Price, dish.Description));
        
    }

    [TestMethod]
    public void Constructor_SetEmptyDescription()
    {
        int expectedId = 60;
        string expectedName = "Notgården Special";
        decimal expectedPrice = 10m;
        string expectedDescription = "";
        
        var dish = new Dish(expectedId, expectedName, expectedPrice, expectedDescription);
        
        Assert.Contains(expectedDescription, dish.Description);
    }
    
}