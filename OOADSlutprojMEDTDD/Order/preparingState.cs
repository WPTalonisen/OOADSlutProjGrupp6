namespace OOADSlutprojMEDTDD;

public class PreparingState : IOrderState
{
    public string Name => "Preparing";

    public IOrderState Next()
    {
        return new OutForDeliveryState();
    }
}