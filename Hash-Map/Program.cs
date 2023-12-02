using Hash_Map;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    public static void Main()
    {

        HashFunctions.ObjectToByteArray("eferwfwefwefwe").ToList().ForEach(x => Console.Write(x));

    }

}