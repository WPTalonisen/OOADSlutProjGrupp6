namespace OOADSlutprojMEDTDD;

public class CancelledState : IOrderState
{
    public string Name => "Cancelled";

    public IOrderState Next()
    {
        throw new InvalidOperationException("A cancelled order can not change status.");
    }

    public IOrderState Cancel()
    {
        throw new InvalidOperationException("This order is already cancelled.");
    }
}