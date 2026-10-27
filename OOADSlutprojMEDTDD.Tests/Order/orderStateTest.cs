using System.Reflection.Metadata.Ecma335;

namespace OOADSlutprojMEDTDD;

[TestClass]
public class OrderStateTest
{
    
    // Checks if order.State is an object of the class RecievedState
    [TestMethod]
    public void NewOrder_StateIsReceived()
    {
        var order = new Order();

        Assert.IsInstanceOfType<ReceivedState>(order.State);
    }

    [TestMethod]
    public void AdvanceStatus_FromReceived_BecomesPreparing()
    {
        var order = new Order();

        order.AdvanceStatus();

        Assert.IsInstanceOfType<PreparingState>(order.State);
    }

    [TestMethod]
    public void AdvanceStatus_FromPreparing_BecomesOutForDelivery()
    {
        var order = new Order();
        order.AdvanceStatus(); // Received -> Preparing
        order.AdvanceStatus(); // Preparing -> Delivering
        Assert.IsInstanceOfType<OutForDeliveryState>(order.State);
    }
    
    [TestMethod]
    public void AdvanceStatus_FromPreparing_BecomesDelivered()
    {
        var order = new Order();
        order.AdvanceStatus(); // Received -> Preparing
        order.AdvanceStatus(); // Preparing -> Delivering
        order.AdvanceStatus(); // Delivering -> Delivered

        Assert.IsInstanceOfType<DeliveredState>(order.State);
    }
}