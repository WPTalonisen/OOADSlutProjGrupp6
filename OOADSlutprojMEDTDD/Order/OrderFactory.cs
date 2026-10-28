namespace OOADSlutprojMEDTDD;

public class OrderFactory
{
    private int _nextId = 1;

    public Order CreateOrder(Restaurant restaurant)
    {
        var order = new Order(_nextId, restaurant);
        _nextId++;
        return order;
    }
}