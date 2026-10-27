namespace OOADSlutprojMEDTDD;

public interface IOrderState
{
    string Name { get; }
    IOrderState Next();
    IOrderState Cancel();
}