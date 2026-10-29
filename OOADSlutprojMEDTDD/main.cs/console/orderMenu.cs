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
            
            int dishNumber = ConsoleInput.ReadInt("Choose dish number: ");
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
        Console.WriteLine($"To pay: {total} kr. (inlcuding {_priceCalculator.DeliveryFee} kr delivery fee)");
        
        // Status and delivery
        order.Subscribe(_notifier);
        order.AdvanceStatus();
        order.AdvanceStatus();

        DeliveryPerson? bud = _deliveryManager.AssignOrder(order);
        if (bud is null)
        {
            Console.WriteLine("No bud available, order is placed in waiting queue.");
            return;
        }

        Console.WriteLine($"{bud.Name} is delivering your order #{order.Id}");
        order.AdvanceStatus();
        _deliveryManager.CompleteDelivery(bud);
    }
}