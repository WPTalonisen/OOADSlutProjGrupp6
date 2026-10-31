namespace OOADSlutprojMEDTDD;

// Helper methods that create ready-made test data
public static class TestData
{
    // An order with default values, for tests that don't care about ID or restaurant
    public static Order CreateOrder()
    {
        return new Order(1, new Restaurant(1, "Test Restaurant"));
    }
}