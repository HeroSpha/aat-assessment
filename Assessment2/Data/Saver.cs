namespace Assessment2.Data;

public class Saver : IFileSaver
{
    public void Save(IEnumerable<int> numbers)
    {
        Console.WriteLine("Saving files");
    }
}