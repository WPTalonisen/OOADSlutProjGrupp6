namespace OOADSlutprojMEDTDD;

public class OutForDeliveryState : IOrderState
{
    public string Name => "Out for delivery";
    
    public IOrderState Next()
    {
        return new DeliveredState();
    }
}