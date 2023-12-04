using Hash_Map;
using Hash_Map.DataStructs.ChainedHashMap;
using Hash_Map.DataStructs.OpenAdressHashMap;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    public static void Main()
    {
        //ChainedHashMap<string, int> dic = new ChainedHashMap<string, int>(10, HashFunctions.HashWithBitMovement);
        //dic.Add("Пенсионер", 10);
        //dic.Add("Пионер", 40);
        //dic.Add("Курьер", 800);
        //dic.Add("Вальер", 30);
        //dic.Add("Карьер", 30);
        //int a = HashFunctions.HashWithDevision("Вальер", 10);
        //int b = HashFunctions.HashWithDevision("Карьер", 10);
        
        



        //double c= dic.GetKoef();
        //dic.Remove("fef");
        //a = 5;


        OpenAdressHashMap<int, int> openDictionary = new OpenAdressHashMap<int, int>(10000, HashFunctions.LineralyResearchHash);
        //openDictionary.Add("Пенсионер", 10);
        //openDictionary.Add("Пионер", 40);
        //openDictionary.Add("Курьер", 800);
        //openDictionary.Add("Вальер", 30);
        //openDictionary.Add("Карьер", 30000);
        //openDictionary.Add("Карьеры", 3);
        //openDictionary.Add("Карьера", 123);
        //openDictionary.Add("Легионер", 400000);

        Dictionary<int, int> testDict = DataSetsGenerator.GenerateSet(10000);
        foreach(int key in testDict.Keys)
        {
            openDictionary.Add(key,testDict[key]);
        }


        openDictionary.Print();
        
        Console.WriteLine(openDictionary.GetKoef());
        Console.WriteLine(openDictionary.GetLongestClusterLength());
        //openDictionary.Remove("fef");
    }

}