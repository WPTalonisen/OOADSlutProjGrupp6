namespace OOADSlutprojMEDTDD;

public class OrderMenu
{
    private readonly RestaurantManager _restaurantManager;
    private readonly OrderFactory _orderFactory;
    private readonly OrderService _orderService;
    private readonly PriceCalculator _priceCalculator;
    private readonly DeliveryManager _deliveryManager;
    private readonly IOrderObserver _notifier;

    public OrderMenu(RestaurantManager restaurantManager, OrderFactory orderFactory, OrderService orderService,
        PriceCalculator priceCalculator, DeliveryManager deliveryManager, IOrderObserver notifier)
    {
        _restaurantManager = restaurantManager;
        _orderFactory = orderFactory;
        _orderService = orderService;
        _priceCalculator = priceCalculator;
        _deliveryManager  = deliveryManager;
        _notifier  = notifier;
    }

    
    // Choose restaurant
    public void CreateOrder()
    {
        Console.WriteLine();
        foreach (var r in _restaurantManager.Restaurants)
        {
            Console.WriteLine($"{r.Id}. {r.Name} (minimum {r.MinimumOrderAmount} kr)");
        }

        Restaurant? restaurant = _restaurantManager.GetById(ConsoleInput.ReadInt("Choose restaurant: "));
        if (restaurant is null)
        {
            Console.WriteLine("Restaurant not found");
            return;
        }
        
        Order order = _orderFactory.CreateOrder(restaurant);
        
        while (true)
        {
            Console.WriteLine();
            for (int i = 0; i < restaurant.Menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {restaurant.Menu[i].Name} -  {restaurant.Menu[i].Price} kr");
            }

            Console.WriteLine($"Subtotal so far: {order.Subtotal} kr");
            Console.WriteLine("0. Go to checkout");
            
            int dishNumber = ConsoleInput.ReadInt("Choose dish number (0 = go to checkout): ");
            if (dishNumber == 0)
            {
                break;
            }

            if (dishNumber < 1 || dishNumber > restaurant.Menu.Count)
            {
                Console.WriteLine("Invalid dish number");
                continue;
            }
            
            int quantity = ConsoleInput.ReadInt("Choose quantity: ");
            if (quantity < 1)
            {
                Console.WriteLine("Invalid quantity");
                continue;
            }
            
            order.AddItem(restaurant.Menu[dishNumber - 1], quantity);
        }
        
        // Confirm
        if (!_orderService.PlaceOrder(order, restaurant))
        {
            Console.WriteLine("Order could not be placed");
            Console.WriteLine($"Minimum order amount is {restaurant.MinimumOrderAmount} kr");
            return;
        }
        
        // Discount
        Console.WriteLine("Discount Selection");
        Console.WriteLine("1. No discount.");
        Console.WriteLine("2. 10% discount.");
        Console.WriteLine("3. 50kr  Amount");
        IDiscountStrategy discount = ConsoleInput.ReadInt("Choose discount: ") switch
        {
            2 => new PercentageDiscount(10m),
            3 => new FixedAmountDiscount(50m),
            _ => new PercentageDiscount(0m)
        };

        decimal total = _priceCalculator.CalculateTotal(order, discount);
        order.SetPrice(_priceCalculator.DeliveryFee, total);
        Console.WriteLine($"To pay: {total} kr. (including {_priceCalculator.DeliveryFee} kr delivery fee)");
        
        
        order.Subscribe(_notifier);
        Console.WriteLine("1. Pay");
        Console.WriteLine("0. Cancel order");

        if (ConsoleInput.ReadInt("Choose: ") != 1)
        {
            order.Cancel();
            Console.WriteLine("Your order was cancelled.");
            return;
        }

        Console.WriteLine("Payment received. Thank you!");
        order.MarkAsPaid();

        SimulateWait("Sending the order to the restaurant");
        order.AdvanceStatus();          // → Preparing
        Console.WriteLine("Use menu 3 to deliver or 4 to cancel.");
    }

    // Moves an order out for delivery, gives it to a courier and delivers it
    public void DeliverOrder()
    {
        Order? order = _orderService.GetOrderById(ConsoleInput.ReadInt("Order number to deliver: "));
        if (order is null)
        {
            Console.WriteLine("Order not found.");
            return;
        }

        try
        {
            SimulateWait("The kitchen is finishing your food");
            order.AdvanceStatus();          // → Out for delivery
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        DeliveryPerson? courier = _deliveryManager.AssignOrder(order);
        if (courier is null)
        {
            Console.WriteLine("No courier available, the order is waiting in the queue.");
            return;
        }

        Console.WriteLine($"{courier.Name} is delivering order #{order.Id}");
        SimulateWait("The courier is on the way");
        order.AdvanceStatus();              // → Delivered
        _deliveryManager.CompleteDelivery(courier);
    }

    // Cancels an order – the order's state decides if it is allowed
    public void CancelOrder()
    {
        Order? order = _orderService.GetOrderById(ConsoleInput.ReadInt("Order number to cancel: "));
        if (order is null)
        {
            Console.WriteLine("Order not found.");
            return;
        }

        try
        {
            order.Cancel();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Shows every accepted order with dishes and prices
    public void ShowHistory()
    {
        Console.WriteLine();

        if (_orderService.OrderHistory.Count == 0)
        {
            Console.WriteLine("No order history available");
            return;
        }

        foreach (var order in _orderService.OrderHistory)
        {
            Console.WriteLine($"Order #{order.Id} - {order.Restaurant.Name} - {order.State.Name}");

            foreach (var item in order.Items)
            {
                Console.WriteLine($"   {item.Quantity} x {item.Dish.Name} - {item.Subtotal} kr");
            }

            Console.WriteLine($"   Subtotal: {order.Subtotal} kr");
            Console.WriteLine($"   Delivery fee: {order.DeliveryFee} kr");
            if (order.State is CancelledState && order.IsPaid)
            {
                Console.WriteLine($"   Refunded: {order.TotalPrice} kr");
            }
            else if (order.State is CancelledState)
            {
                Console.WriteLine("   Not paid");
            }
            else
            {
                Console.WriteLine($"   Total: {order.TotalPrice} kr");
            }
        }
    }

    // Simulates time passing: waits a random time between 5 and 10 seconds
    private static void SimulateWait(string message)
    {
        int seconds = Random.Shared.Next(5, 11);
        Console.WriteLine($"{message} ({seconds} s)...");
        Thread.Sleep(seconds * 1000);
    }
}