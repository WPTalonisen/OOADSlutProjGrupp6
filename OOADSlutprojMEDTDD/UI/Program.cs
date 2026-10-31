namespace OOADSlutprojMEDTDD;

public static class Program
{
    public static void Main()
    {
        
        var restaurantManager = new RestaurantManager();

        var pizzeriaTalonisen = new Restaurant(1, "Pizzeria Talonisen", 100m);
        pizzeriaTalonisen.AddDish(new Dish(1, "TDD Pizza", 100m));
        pizzeriaTalonisen.AddDish(new Dish(2, "TDD Rulle", 100m));
        pizzeriaTalonisen.AddDish(new Dish(3, "IT-Special", 80m));
        pizzeriaTalonisen.AddDish(new Dish(4, "Vit Monster", 20m));
        pizzeriaTalonisen.AddDish(new Dish(5, "Vatten", 10m));
        restaurantManager.AddRestaurant(pizzeriaTalonisen);
        
        var maxSundburger = new Restaurant(2, "Max Sundburger", 60m);
        maxSundburger.AddDish(new Dish(1, "Max Sundburger", 110m));
        maxSundburger.AddDish(new Dish(2, "Max Sundburger Super", 150m));
        maxSundburger.AddDish(new Dish(3, "Glass", 30m));
        maxSundburger.AddDish(new Dish(4, "Strips", 40m));
        restaurantManager.AddRestaurant(maxSundburger);
        
        var noelHolsushi = new Restaurant(3, "Noel Holsushi", 100m);
        noelHolsushi.AddDish(new Dish(1, "Fried Rice", 59m));
        noelHolsushi.AddDish(new Dish(2, "California Roll 1 bit", 69m));
        noelHolsushi.AddDish(new Dish(3, "Avocado Roll 2 bitar", 89m));
        noelHolsushi.AddDish(new Dish(4, "Salmon Rolls 5 bitar", 150m));
        noelHolsushi.AddDish(new Dish(5, "Friterad Kyckling m. Ris och Jordnötsås", 200m));
        noelHolsushi.AddDish(new Dish(6, "Wagyu Stek 50g", 4000m));
        noelHolsushi.AddDish(new Dish(7, "Surströmming Nigiri 5 bitar (Röda Ulven)", 40m));
        noelHolsushi.AddDish(new Dish(8, "Döner Kebab m. Pommes", 89m));
        noelHolsushi.AddDish(new Dish(9, "Kolsyrad Dryck med Sojasmak", 15m));
        restaurantManager.AddRestaurant(noelHolsushi);
        
        var deliveryManager = new DeliveryManager();
        deliveryManager.AddDeliveryPerson(new DeliveryPerson(1, "William"));
        deliveryManager.AddDeliveryPerson(new DeliveryPerson(2, "Noel"));
        deliveryManager.AddDeliveryPerson(new DeliveryPerson(3, "Max"));
        deliveryManager.AddDeliveryPerson(new DeliveryPerson(4, "Pär"));
        
        var orderFactory = new OrderFactory();
        var orderService = new OrderService();
        var priceCalculator = new PriceCalculator(50m);
        var notifier = new ConsoleOrderNotifier(Console.Out);
        var orderMenu = new OrderMenu(restaurantManager, orderFactory, orderService, priceCalculator, deliveryManager, notifier);
        
        // Menu
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== SUPER DUPER EATS ===");
            Console.WriteLine("Est. 2026");
            Console.WriteLine("1. New Order");
            Console.WriteLine("2. Show Order History");
            Console.WriteLine("3. Deliver Order");
            Console.WriteLine("4. Cancel Order");
            Console.WriteLine("0. Exit");
            
            int userChoice = ConsoleInput.ReadInt("Enter choice: ");

            switch (userChoice)
            {
                case 1:
                    orderMenu.CreateOrder();
                    break;
                case 2:
                    orderMenu.ShowHistory();
                    break;
                case 3:
                    orderMenu.DeliverOrder();
                    break;
                case 4:
                    orderMenu.CancelOrder();
                    break;
                case 0:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}