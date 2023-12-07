using Hash_Map;
using Hash_Map.DataStructs;
using Hash_Map.DataStructs.ChainedHashMap;
using Hash_Map.DataStructs.OpenAdressHashMap;
using Hash_Map.HashFunctions.HashFucntionsForOpenAdress;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    private static int HashTableSize = 0;
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


        IHashMap<int, int> hashMap;
        Console.WriteLine("Здравствуйте, вас приветствует Консольное приложение, для работы с Хэш-таблицами\n"+
                          "С каким именно типом Хэш-таблиц вы бы хотели поработать?\n" +
                          "Введите 0, если вы хотите поработать с Хэш-таблицой, использующей метод разрешения коллизий с помощью цепочек.\n"+
                          "Введите 1, если вы хотите поработать с Хэш-таблицой, использующей метод открытой адресации для разрешения коллизий.");
        
        string typeFlag = Console.ReadLine();
        if (typeFlag == "0")
        {
            
        }
        else if (typeFlag == "1")
        {
            Console.Clear();

            Console.WriteLine("Сначала введите размер итоговой Хэш-таблицы: ");
            EnterHashTableSize();

            Console.WriteLine();
            Console.WriteLine("C какой Хэш функцией вы бы хотели создать Хэш-таблицу?");
            OpenAdressHashMap<int, int> openAdressHashMap = CreateHashTableObject();
            Console.WriteLine();
            Console.WriteLine("Хорошо, теперь приступим к работе с Хэш-таблицой");

        }


        //OpenAdressHashMap<int, int> openDictionary = new OpenAdressHashMap<int, int>(10000, HashFunctions.XorHashForInt);


        //Dictionary<int, int> testDict = DataSetsGenerator.GenerateSet(10000);
        //foreach(int key in testDict.Keys)
        //{
        //    openDictionary.Add(key,testDict[key]);
        //}


        //openDictionary.Print();

        //Console.WriteLine(openDictionary.GetKoef());
        //Console.WriteLine(openDictionary.GetLongestClusterLength());
    }

    private static OpenAdressHashMap<int, int> CreateHashTableObject()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();

        // Фильтруем типы, оставляя только те, которые реализуют интерфейс IOpenAdressHashFunc
        var implementingTypes = types.Where(type => typeof(IOpenAdressHashFunc).IsAssignableFrom(type) && !type.IsInterface);
        while (true)
        {
            // Создаем экземпляры объектов для найденных типов
            foreach (var implementingType in implementingTypes)
            {
                IOpenAdressHashFunc hashFucnObj = (IOpenAdressHashFunc)Activator.CreateInstance(implementingType);
                Console.WriteLine("Вы хотите создать её с использованием " + hashFucnObj.GetName() + "\n" +
                                  "Введите 1, если да, и 0, если нет");

                string isUserChoseThisHashFunc = Console.ReadLine();

                if (isUserChoseThisHashFunc == "1")
                {
                    return new OpenAdressHashMap<int, int>(HashTableSize, hashFucnObj.GetHashFunc());
                }
            }
            Console.WriteLine("Это были все варианты хэш-функций, вы должны выбрать хоть один из них, попробуйте ещё раз");
            Console.WriteLine();
        }
    }

    private static void EnterHashTableSize()
    {
        while (true)
        {
            string stringSizeHashTable = Console.ReadLine();

            if (int.TryParse(stringSizeHashTable, out int size) && size>0)
            {
                HashTableSize = size;
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели неверное число, попробуйте ещё раз");
            }
        }
    }
}