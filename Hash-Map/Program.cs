using Hash_Map;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    public static void Main()
    {
        ChainedHashMap<string, int> dic = new ChainedHashMap<string, int>(10, HashFunctions.HashWithBitMovement);
        dic.Add("Пенсионер", 10);
        dic.Add("Пионер", 40);
        dic.Add("Курьер", 800);
        dic.Add("Вальер", 30);
        dic.Add("Карьер", 30);
        int a = HashFunctions.HashWithDevision("Вальер", 10);
        int b = HashFunctions.HashWithDevision("Карьер", 10);
        
        



        double c= dic.GetKoef();
        a = 5;


    }

}