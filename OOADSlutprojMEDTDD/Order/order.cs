namespace OOADSlutprojMEDTDD;

public class Order
{
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
    }
}