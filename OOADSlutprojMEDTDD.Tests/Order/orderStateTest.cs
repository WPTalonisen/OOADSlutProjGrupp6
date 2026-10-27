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
    
    // Cancel Order
    [TestMethod]
    public void Cancel_FromReceived_BecomesCancelled()
    {
        var order = new Order();

        order.Cancel();

        Assert.IsInstanceOfType<CancelledState>(order.State);
    }

    [TestMethod]
    public void Cancel_FromPreparing_BecomesCancelled()
    {
        var order = new Order();
        order.AdvanceStatus(); // Received -> Preparing

        order.Cancel();
        
        Assert.IsInstanceOfType<CancelledState>(order.State);
    }
    
    [TestMethod]
    public void Cancel_FromOutForDelivery_ThrowsInvalidOperationException()
    {
        var order = new Order();

        order.AdvanceStatus(); // Received -> Preparing
        order.AdvanceStatus(); // Preparing -> Delivering
        
        Assert.ThrowsExactly<InvalidOperationException>(() => order.Cancel());
    }
    
    [TestMethod]
    public void Cancel_FromDelivered_ThrowsInvalidOperationException()
    {
        var order = new Order();
        order.AdvanceStatus(); // Received -> Preparing
        order.AdvanceStatus(); // Preparing -> Delivering
        order.AdvanceStatus(); // Delivering -> Delivered

        Assert.ThrowsExactly<InvalidOperationException>(() => order.Cancel());
    }

    [TestMethod]
    public void AdvanceStatus_FromDelivered_ThrowsInvalidOperationException()
    {
        var order = new Order();
        order.Cancel();
        
        Assert.ThrowsExactly<InvalidOperationException>(() => order.AdvanceStatus());
    }
}