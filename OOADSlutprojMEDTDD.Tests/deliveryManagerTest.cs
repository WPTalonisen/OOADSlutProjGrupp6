namespace OOADSlutprojMEDTDD;

[TestClass]
public sealed class DeliveryManagerTest
{
    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_ReturnsThatDeliveryPerson()
    {
        var manager = new DeliveryManager();
        var deliveryPerson = new DeliveryPerson(1, "Big Dave");

        manager.AddDeliveryPerson(deliveryPerson);

        DeliveryPerson assigned = manager.AssignOrder(new Order());

        Assert.AreSame(deliveryPerson, assigned);
    }

    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_DeliveryPersonBecomesBusy()
    {
        var manager = new DeliveryManager();
        var deliveryPerson = new DeliveryPerson(1, "Big Dave");

        manager.AddDeliveryPerson(deliveryPerson);

        manager.AssignOrder(new Order());

        Assert.AreEqual(DeliveryStatus.Busy, deliveryPerson.Status);
    }

    [TestMethod]
    public void AssignOrder_DeliveryPersonAvailable_DeliveryPersonLeavesQueue()
    {
        var manager = new DeliveryManager();
        manager.AddDeliveryPerson(new DeliveryPerson(1, "Big Dave"));

        manager.AssignOrder(new Order());

        Assert.AreEqual(0, manager.AvailableCount);
    }
}