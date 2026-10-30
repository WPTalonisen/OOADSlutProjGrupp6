namespace OOADSlutprojMEDTDD;

public static class ConsoleInput
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (input == null)
            {
                return 0;
            }

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Write a number.");
        }
    }
}