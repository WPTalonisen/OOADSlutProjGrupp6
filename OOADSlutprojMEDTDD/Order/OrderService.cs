namespace OOADSlutprojMEDTDD;

public class OrderService
{
    private List<Order> _orderHistory = new List<Order>();
    
    public IReadOnlyList<Order> OrderHistory => _orderHistory;
    
    public bool PlaceOrder(Order order, Restaurant restaurant)
    {
        if (!restaurant.MeetsMinimumOrder(order.Subtotal))
        {
            return false;
        }
        
        _orderHistory.Add(order);
        return true;
    }
}