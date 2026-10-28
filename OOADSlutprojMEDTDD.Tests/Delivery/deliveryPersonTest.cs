namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class DeliveryPersonTest
{
    [TestMethod] 
    public void NewDeliveryPerson_IsAvailable()
    {
        var deliveryPerson = new DeliveryPerson(1,"Big Dave");
        Assert.AreEqual(DeliveryStatus.Available, deliveryPerson.Status);
    }
    
    [TestMethod] 
    public void NewDeliveryPerson_OrderIsNull()
    {
        var deliveryPerson = new DeliveryPerson(1,"Big Dave");
        Assert.IsNull(deliveryPerson.CurrentOrder);
    }
    
    [TestMethod]
    public void NewDeliveryPerson_Name()
    {
        var deliveryPerson = new DeliveryPerson(1,"Big Dave");
        Assert.AreEqual("Big Dave",deliveryPerson.Name);
    }
    
    [TestMethod]
    public void NewDeliveryPerson_Id()
    {
        var deliveryPerson = new DeliveryPerson(1,"Big Dave");
        Assert.AreEqual(1,deliveryPerson.Id);
    }
    
    
    
    [TestMethod]
    public void AssignOrder_DeliveryPersonBecomesHasOrder()
    {
        var deliveryPerson = new  DeliveryPerson(1,"Big Dave");
        var order = TestData.CreateOrder();
        
        deliveryPerson.AssignOrder(order);
        
        Assert.AreEqual(order, deliveryPerson.CurrentOrder);
    }
    
    [TestMethod] 
    public void AssignOrder_DeliveryPersonBecomesBusy()
    {
        
        var deliveryPerson = new  DeliveryPerson(1,"Big Dave");
        var order = TestData.CreateOrder();
        
        deliveryPerson.AssignOrder(order);
        
        Assert.AreEqual(DeliveryStatus.Busy, deliveryPerson.Status);
    }
    
    [TestMethod]
    public void CompleteDelivery_CurrentStatus()
    {
        var deliveryPerson = new  DeliveryPerson(1,"Big Dave");
        var order = TestData.CreateOrder();
        deliveryPerson.AssignOrder(order);
        Assert.AreEqual(DeliveryStatus.Busy, deliveryPerson.Status);

        deliveryPerson.CompleteDelivery();
        Assert.AreEqual(DeliveryStatus.Available, deliveryPerson.Status);

    }
    
    [TestMethod]
    public void CompleteDelivery_OrderStatus()
    {
        var deliveryPerson = new  DeliveryPerson(1,"Big Dave");
        var order = TestData.CreateOrder();
        deliveryPerson.AssignOrder(order);
        Assert.AreEqual(order, deliveryPerson.CurrentOrder);

        deliveryPerson.CompleteDelivery();
        Assert.IsNull(deliveryPerson.CurrentOrder);

    }
    
    
    
    
    
    
    
    
}

