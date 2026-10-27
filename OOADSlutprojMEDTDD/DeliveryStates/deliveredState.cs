namespace OOADSlutprojMEDTDD;

public class DeliveredState : IOrderState
{
    public string Name => "Delivered";

    public IOrderState Next()
    {
        throw new InvalidOperationException("A delivered order can not change status");
    }
    
    public IOrderState Cancel()
    {
        throw new InvalidOperationException("The order can not be cancelled in this state.");
    }
}