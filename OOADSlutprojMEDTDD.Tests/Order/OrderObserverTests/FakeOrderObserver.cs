namespace OOADSlutprojMEDTDD;


public class FakeOrderObserver : IOrderObserver
{
    public int CallCount { get; private set; }
    
    public IOrderState? OrderState { get; private set; }
    
    public void OnStatusChanged(Order order)
    {
    CallCount++;
    OrderState = order.State;
    }
}