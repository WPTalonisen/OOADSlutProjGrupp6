namespace OOADSlutprojMEDTDD;

// Hjälpmetoder som skapar färdig testdata åt alla tester
public static class TestData
{
    // En order med standardvärden, för tester som inte bryr sig om ID eller restaurang
    public static Order CreateOrder()
    {
        return new Order(1, new Restaurant(1, "Test Restaurant"));
    }
}