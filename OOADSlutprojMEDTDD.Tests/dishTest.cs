namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class dishTest
{

    [TestMethod]
    public void Constructor_SetId()
    {
        var dish = new Dish(1); 
        Assert.AreEqual(1, dish.Id);
    }
        
    [TestMethod]
    public void Constructor_SetName()
    {        
        var dish = new Dish(1, "Kattspya med bajs"); 
        Assert.AreEqual("Kattspya med bajs", dish.Name);
    }
    
    [TestMethod]
    public void Constructor_SetPrice()
    {
        var dish = new Dish(1, price: 99m);
        Assert.AreEqual(99m, dish.Price);
    }
    
    [TestMethod]
    public void Constructor_SetDescription()
    {
        var dish = new Dish(1, description: "Creamy and moist, ribbed for his pleasure");
        Assert.AreEqual("Creamy and moist, ribbed for his pleasure", dish.Description);
    }

    
    [TestMethod] 
    public void Constructor_DishSetAll()
    {
        int expectedId = 60;
        string expectedName = "Notgården Special";
        decimal expectedPrice = 10m;
        string expectedDescription = "Koskit med mangoraja";
        
        
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