namespace OOADSlutprojMEDTDD;

public class ConsoleOrderNotifier : IOrderObserver
{
    private readonly TextWriter _output;
    public ConsoleOrderNotifier(TextWriter output)
    {
        _output = output;
    }

    public void OnStatusChanged(Order order)
    {
        _output.WriteLine($"Order #{order.Id} is now {order.State.Name}");    
    }
}