namespace OOADSlutprojMEDTDD;

public class OutForDeliveryState : IOrderState
{
    public string Name => "Out for delivery";
    
    public IOrderState Next()
    {
        return new DeliveredState();
    }

    public IOrderState Cancel()
    {
        throw new InvalidOperationException("The order can not be cancelled in this state.");
    }
}