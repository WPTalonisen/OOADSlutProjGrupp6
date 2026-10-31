namespace OOADSlutprojMEDTDD;

public class DeliveryPerson
{
    // Delivery person name and ID
    public int Id { get; }
    public string Name { get; }

    // Status and current order, order can be null
    public DeliveryStatus Status { get; private set; }
    public Order? CurrentOrder { get; private set; }

    public DeliveryPerson(int id, string name)
    {
        Id = id;
        Name = name;
        Status = DeliveryStatus.Available;
    }

    // Takes an order and becomes busy 
    public void AssignOrder(Order order)
    {
        CurrentOrder = order;
        Status = DeliveryStatus.Busy;
    }
    
    // Delivery done, becomes available again
    public void CompleteDelivery()
    {    
        Status = DeliveryStatus.Available;
        CurrentOrder = null;
    }

}