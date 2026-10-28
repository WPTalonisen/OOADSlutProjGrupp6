namespace OOADSlutprojMEDTDD;

public interface IOrderObserver
{
    void OnStatusChanged(Order order);
}