namespace OOADSlutprojMEDTDD;

public class ReceivedState : IOrderState
{
    public string Name => "Received";

    public IOrderState Next()
    {
        return new PreparingState();
    }
}