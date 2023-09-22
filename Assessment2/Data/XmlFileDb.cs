using System.Xml.Serialization;

namespace Assessment2.Data;

public class XmlFileDb : FileSaver
{
    public XmlFileDb(IFileSaver listSaver) : base(listSaver)
    {
    }

    public override void Save(IEnumerable<int> numbers)
    { 
        var serializer = new XmlSerializer(typeof(List<int>));
        using (TextWriter writer = new StreamWriter("list.xml"))
        {
            serializer.Serialize(writer, numbers);
        }
        base.Save(numbers);
    }
}