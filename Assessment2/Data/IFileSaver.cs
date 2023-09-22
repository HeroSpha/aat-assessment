namespace Assessment2.Data;

public interface IFileSaver
{
    void Save(IEnumerable<int> numbers);
}