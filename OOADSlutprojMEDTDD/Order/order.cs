namespace OOADSlutprojMEDTDD;

public class Order
{

    public int Id { get; }

    public Restaurant Restaurant { get; }

    public Order(int id, Restaurant restaurant)
    {
        Id = id;
        Restaurant = restaurant;
    }
    
    public decimal DeliveryFee { get; private set; }
    public decimal TotalPrice { get; private set; }
    
    public void SetPrice(decimal deliveryFee, decimal totalPrice)
    {
        DeliveryFee = deliveryFee;
        TotalPrice = totalPrice;
    }
    
    // True when the customer has paid
    public bool IsPaid { get; private set; }

    // Called when the payment is received
    public void MarkAsPaid()
    {
        IsPaid = true;
    }

    // Private list with OrderItem
    private List<OrderItem> _items = new List<OrderItem>();

    // Public list others can see but not change
    public IReadOnlyList<OrderItem> Items => _items;

    // Creates an OrderRow of Dish + quantity and adds to the list
    public void AddItem(Dish dish, int quantity)
    {
        var item = new OrderItem(dish, quantity);
        _items.Add(item);
    }

    public decimal Subtotal
    {
        get
        {
            decimal sum = 0m;
            foreach (var item in _items)
            {
                sum += item.Subtotal;
            }

            return sum;
        }
    }

    public IOrderState State { get; private set; } = new ReceivedState();

    public void AdvanceStatus()
    {
        State = State.Next();
        NotifyObservers();
    }



    public void Cancel()
    {
        State = State.Cancel();
        NotifyObservers();
    }

    // OBSERVER

    private List<IOrderObserver> _observers = new List<IOrderObserver>();

    // Sign up for notifications
    public void Subscribe(IOrderObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IOrderObserver observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.OnStatusChanged(this);
        }
    }
}