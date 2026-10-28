namespace OOADSlutprojMEDTDD;

[TestClass]
public class OrderObserverTest
{
    [TestMethod]
    public void AdvanceStatus_WithObserver_ObserverIsNotified()
    {
        var order = TestData.CreateOrder();
        var observer = new FakeOrderObserver();
        order.Subscribe(observer);

        order.AdvanceStatus();

        Assert.AreEqual(1, observer.CallCount);
    }
    
    [TestMethod]
    public void AdvanceStatus_WithObserver_ObserverSeesNewState()
    {
        var order = TestData.CreateOrder();
        var observer = new FakeOrderObserver();
        order.Subscribe(observer);
        order.AdvanceStatus();
        Assert.IsInstanceOfType<PreparingState>(observer.OrderState);
    }
    
    [TestMethod]
    public void Cancel_WithObserver_ObserverIsNotified()
    {
        var order = TestData.CreateOrder();
        var observer = new FakeOrderObserver();
        order.Subscribe(observer);
        
        order.Cancel();
        
        Assert.AreEqual(1, observer.CallCount);
    }
    
    [TestMethod]
    public void Unsubscribe_ObserverIsNotNotified()
    {
        var order = TestData.CreateOrder();
        var observer = new FakeOrderObserver();
        order.Subscribe(observer);
        
        order.Unsubscribe(observer);
        
        order.AdvanceStatus();
        
        Assert.AreEqual(0, observer.CallCount);
    }

    [TestMethod]
    public void AdvanceStatus_TwoObservers_SecondIsNotified()
    { 
        var order = TestData.CreateOrder();
        var first = new FakeOrderObserver();
        var second = new FakeOrderObserver();
        order.Subscribe(first);
        order.Subscribe(second);

        order.AdvanceStatus();

        Assert.AreEqual(1, second.CallCount);
    }
    
    [TestMethod]
    public void ConsoleOrderNotifier_StatusChange_WritesMessage()
    {
        var order = TestData.CreateOrder();
        var writer = new StringWriter();
        var notifier = new ConsoleOrderNotifier(writer);
        order.Subscribe(notifier);
        
        order.AdvanceStatus();
        
        Assert.AreEqual("Order #1 is now Preparing", writer.ToString().Trim());
    }
}