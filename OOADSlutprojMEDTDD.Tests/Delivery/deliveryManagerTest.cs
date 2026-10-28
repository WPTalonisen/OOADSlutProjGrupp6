namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class DeliveryManagerTest
{
    
    // -- Available Delivery Person
    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_ReturnsThatDeliveryPerson()
    {
        var manager = new DeliveryManager();
        var deliveryPerson = new DeliveryPerson(1, "Big Dave");

        manager.AddDeliveryPerson(deliveryPerson);

        DeliveryPerson? assigned = manager.AssignOrder(TestData.CreateOrder());

        Assert.AreSame(deliveryPerson, assigned);
    }

    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_DeliveryPersonBecomesBusy()
    {
        var manager = new DeliveryManager();
        var deliveryPerson = new DeliveryPerson(1, "Big Dave");

        manager.AddDeliveryPerson(deliveryPerson);

        manager.AssignOrder(TestData.CreateOrder());

        Assert.AreEqual(DeliveryStatus.Busy, deliveryPerson.Status);
    }

    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_DeliveryPersonLeavesQueue()
    {
        var manager = new DeliveryManager();
        manager.AddDeliveryPerson(new DeliveryPerson(1, "Big Dave"));

        manager.AssignOrder(TestData.CreateOrder());

        Assert.AreEqual(0, manager.AvailableCount);
    }
    
    // -- No available Delivery Person
    
    [TestMethod]
    public void AssignOrder_NoDeliveryPersonAvailable_ReturnsNull()
    {
        var manager = new DeliveryManager();

        DeliveryPerson? assigned = manager.AssignOrder(TestData.CreateOrder());

        Assert.IsNull(assigned);
    }

    [TestMethod]
    public void AssignOrder_NoDeliveryPersonAvailable_OrderIsQueued()
    {
        var manager = new DeliveryManager();
        
        manager.AssignOrder(TestData.CreateOrder());

        Assert.AreEqual(1, manager.WaitingCount);
    }
    
    // -- Two Delivery Persons, FIFO

    [TestMethod]
    public void AssignOrder_TwoDeliveryPersons_FirstAddedGetsOrder()
    {
        var manager = new DeliveryManager();
        
        var bigDave = new DeliveryPerson(1, "Big Dave");
        var skinnyJeansAdam = new DeliveryPerson(2, "Skinny Jeans Adam");

        manager.AddDeliveryPerson(bigDave);
        manager.AddDeliveryPerson(skinnyJeansAdam);
        

        DeliveryPerson? assigned = manager.AssignOrder(TestData.CreateOrder());

        Assert.AreSame(bigDave, assigned);

    }
    
    [TestMethod]
    public void AssignOrder_TwoDeliveryPersons_FirstAddedGetsOrderSecondIsAvailable()
    {
        var manager = new DeliveryManager();
        
        var bigDave = new DeliveryPerson(1, "Big Dave");
        var skinnyJeansAdam = new DeliveryPerson(2, "Skinny Jeans Adam");

        manager.AddDeliveryPerson(skinnyJeansAdam);
        manager.AddDeliveryPerson(bigDave);
        

        DeliveryPerson? assigned = manager.AssignOrder(TestData.CreateOrder());

       
        Assert.AreEqual(skinnyJeansAdam, assigned);
        Assert.AreEqual(DeliveryStatus.Available, bigDave.Status);
        Assert.AreEqual(DeliveryStatus.Busy, skinnyJeansAdam.Status);

    }
    
    
    [TestMethod]
    public void AssignOrder_TwoDeliveryPersons_SecondStaysAvailable()
    {
        var manager = new DeliveryManager();
        
        var bigDave = new DeliveryPerson(1, "Big Dave");
        var skinnyJeansAdam = new DeliveryPerson(2, "Skinny Jeans Adam");
        
        manager.AddDeliveryPerson(bigDave);
        manager.AddDeliveryPerson(skinnyJeansAdam);
        
        manager.AssignOrder(TestData.CreateOrder());
        
        Assert.AreEqual(DeliveryStatus.Available, skinnyJeansAdam.Status);
    }
    
    // -- Delivery Completed, no orders waiting
    
    [TestMethod]
    public void CompletedDelivery_NoWaitingOrders_DeliveryPersonBecomesAvailable()
    {
        var manager = new DeliveryManager();
        var bigDave = new DeliveryPerson(1, "Big Dave");
        manager.AddDeliveryPerson(bigDave);
        manager.AssignOrder(TestData.CreateOrder());

        manager.CompleteDelivery(bigDave);

        Assert.AreEqual(DeliveryStatus.Available, bigDave.Status);
    }
    
    [TestMethod]
    public void CompletedDelivery_NoWaitingOrders_DeliveryPersonBackInQueue()
    {
        var manager = new DeliveryManager();
        var bigDave = new DeliveryPerson(1, "Big Dave");
        
        manager.AddDeliveryPerson(bigDave);
        manager.AssignOrder(TestData.CreateOrder());

        Assert.AreEqual(DeliveryStatus.Busy, bigDave.Status);

        manager.CompleteDelivery(bigDave);
        
        Assert.AreEqual(1, manager.AvailableCount);
    }
    
    // -- Delivery Completed, one order waiting
    
    
    // -- Går igenom stegvist test, 
    [TestMethod]
    public void CompletedDelivery_OrderWaiting_DeliveryPersonGetsWaitingOrderAndQueueIsEmpty()
    {
        var manager = new DeliveryManager();
        var bigDave = new DeliveryPerson(1, "Big Dave");
        manager.AddDeliveryPerson(bigDave);
        manager.AssignOrder(TestData.CreateOrder());

        Assert.AreEqual(DeliveryStatus.Busy, bigDave.Status);

        var waitingOrder = TestData.CreateOrder();
        manager.AssignOrder(waitingOrder);

        Assert.AreEqual(1, manager.WaitingCount);

        manager.CompleteDelivery(bigDave);

        Assert.AreSame(waitingOrder, bigDave.CurrentOrder);
        Assert.AreEqual(0, manager.WaitingCount);
    }
    
    [TestMethod]
    public void CompletedDelivery_OrderWaiting_DeliveryPersonStaysBusy()
    {
        var manager = new DeliveryManager();
        var bigDave = new DeliveryPerson(1, "Big Dave");
        manager.AddDeliveryPerson(bigDave);
        manager.AssignOrder(TestData.CreateOrder());
        manager.AssignOrder(TestData.CreateOrder());
        
        manager.CompleteDelivery(bigDave);
        
        Assert.AreEqual(DeliveryStatus.Busy, bigDave.Status);
    }
    
}