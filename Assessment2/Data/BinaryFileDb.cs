namespace Assessment2.Data;

public class BinaryFileDb : FileSaver
{
    public BinaryFileDb(IFileSaver listSaver) : base(listSaver)
    {
    }

    public override void Save(IEnumerable<int> numbers)
    {
        var list = numbers as int[] ?? numbers.ToArray();
        using (var fs = new FileStream("list.bin", FileMode.Create))
        {
            var writer = new BinaryWriter(fs);
            foreach (var item in list)
            {
                writer.Write(item);
            }
        }
        base.Save(list);
    }
}