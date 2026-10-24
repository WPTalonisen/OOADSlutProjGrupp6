namespace OOADSlutprojMEDTDD;

public class DeliveryManager
{
    // Queue with Availabe deliveryPersons
    private Queue<DeliveryPerson> _availableDeliveryPersons = new Queue<DeliveryPerson>();

    // Queue with waiting orders
    private Queue<Order> _waitingOrders = new Queue<Order>();

    // How many in respective queue
    public int AvailableCount => _availableDeliveryPersons.Count;
    public int WaitingCount => _waitingOrders.Count;

    // Adds DeliveryPerson last in queue
    public void AddDeliveryPerson(DeliveryPerson deliveryPerson)
    {
        _availableDeliveryPersons.Enqueue(deliveryPerson);
    }

    public DeliveryPerson? AssignOrder(Order order)
    {
        if (_availableDeliveryPersons.Count > 0)
        {
            DeliveryPerson deliveryPerson = _availableDeliveryPersons.Dequeue();
            deliveryPerson.AssignOrder(order);
            return deliveryPerson;
        }
        else
        {
            _waitingOrders.Enqueue(order);
            return null;
        }
    }
}
