namespace Assessment2.Data;

public abstract class FileSaver : IFileSaver
{
    protected IFileSaver _listSaver;

    public FileSaver(IFileSaver listSaver)
    {
        _listSaver = listSaver;
    }

    public virtual void Save(IEnumerable<int> numbers)
    {
        _listSaver.Save(numbers);
    }
}