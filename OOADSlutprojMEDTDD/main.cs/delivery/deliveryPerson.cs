namespace OOADSlutprojMEDTDD;

public class DeliveryPerson
{
    // Budets namn och ID
    public int Id { get; }
    public string Name { get; }

    // Status och nuvarande order, order kan vara Null.
    public DeliveryStatus Status { get; private set; }
    public Order? CurrentOrder { get; private set; }

    public DeliveryPerson(int id, string name)
    {
        Id = id;
        Name = name;
        Status = DeliveryStatus.Available;
    }

    // Bud tar emot en order och blir busy 
    public void AssignOrder(Order order)
    {
        CurrentOrder = order;
        Status = DeliveryStatus.Busy;
    }
    
    // Budet har levererat och blir available igen
    public void CompleteDelivery()
    {    
        Status = DeliveryStatus.Available;
        CurrentOrder = null;
    }

}