using Hash_Map;
using Hash_Map.DataStructs;
using Hash_Map.DataStructs.ChainedHashMap;
using Hash_Map.DataStructs.OpenAdressHashMap;
using Hash_Map.HashFunctions.HashFucntionsForOpenAdress;
using System.Net.Http.Headers;
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
                          "Введите 1, если вы хотите поработать с Хэш-таблицой, использующей метод разрешения коллизий с помощью цепочек.\n"+
                          "Введите что угодно кроме 1, если вы хотите поработать с Хэш-таблицой, использующей метод открытой адресации для разрешения коллизий.");
        
        string typeFlag = Console.ReadLine();
        if (typeFlag == "1")
        {
            
        }
        else
        {
            WorkWithOpenAdressTable();
        }
    }

    private static void WorkWithOpenAdressTable()
    {
        Console.Clear();

        Console.WriteLine("Сначала введите размер итоговой Хэш-таблицы: ");
        EnterHashTableSize();

        Console.WriteLine();
        Console.WriteLine("C какой Хэш функцией вы бы хотели создать Хэш-таблицу?");
        OpenAdressHashMap<int, int> openAdressHashMap = CreateHashTableObject();
        Console.WriteLine();
        Console.WriteLine("Хорошо, теперь приступим к работе с Хэш-таблицой");

        Console.WriteLine("Вы бы хотели просто заполнить таблицу целиком, а потом посмотреть на получившийся результат или сами заполнять таблицу по элементно?\n" +
                          "Введите 1, если да, что угодно кроме 1, если нет.");

        string isUserWantGenereateNumbers = Console.ReadLine();
        if (isUserWantGenereateNumbers == "1")
        {
            Console.Clear();

            Console.WriteLine("Хорошо, тогда вот итоговая таблица:");
            Console.WriteLine();

            Dictionary<int, int> testDict = DataSetsGenerator.GenerateSet(HashTableSize);
            foreach (int key in testDict.Keys)
            {
                openAdressHashMap.Add(key, testDict[key]);
            }
            openAdressHashMap.Print();

            Console.WriteLine("На этом работа с программой завершена, теперь вы можете только перезапустить её");
        }
        else
        {
            WorkWithHashTableByCommands(openAdressHashMap);
        }
    }

    private static void WorkWithHashTableByCommands(OpenAdressHashMap<int, int> openAdressHashMap)
    {
        Console.Clear();
        Console.WriteLine("Хорошо, тогда теперь у вас есть следующий перечень команд:\n" +
                          "Введите 1, чтобы добавить новую пару в таблицу (ключ и значение нужно ввести в следующей за командой строчке, через ;)\n" +
                          "Введите 2, чтобы получить пару ключ;значение по ключу, если они есть в таблице (ключ нужно ввести в следующей за командой строчке)\n" +
                          "Введите 3, чтобы удалить пару ключ;значение по ключу, если они есть в таблице (ключ нужно ввести в следующей за командой строчке)\n" +
                          "Введите 4, чтобы вывести текущее состояние Хэш-таблицы с коэфициентом заполнения и размером самого длинного кластера\n" +
                          "Введите 5, чтобы закончить работу с программой\n");
        
        while (true)
        {
            Console.Write("Введите команду: ");
            string userCommand = Console.ReadLine();
            switch (userCommand)
            {
                case "1":
                    AddPairToTable(openAdressHashMap);
                    break;
                case "2":
                    FindPairFromTable(openAdressHashMap);
                    break;
                case "3":
                    RemovePairFromTable(openAdressHashMap);
                    break;
                case "4":
                    PrintInfoAboutTable(openAdressHashMap);
                    break;
                case "5":
                    Console.WriteLine("Программа завершила свою работу, теперь вы можете только перезапустить её");
                    return;
                default:
                    Console.WriteLine("Вы ошиблись с введенной командой, попробуйте ещё раз");
                    break;
            }
        }
    }

    private static void AddPairToTable(OpenAdressHashMap<int, int> openAdressHashMap)
    {
        while (true)
        {
            Console.Write("Введите пару ключ;значение: ");
            string newKeyValuePair = Console.ReadLine();
            string[] keyAndValue = newKeyValuePair.Split(";");
            if (keyAndValue.Length == 1 || keyAndValue.Length > 2)
            {
                Console.WriteLine("Вы ввели пару ключ;значение в неправильном формате, попробуйте ещё раз");
                continue;
            }

            if (int.TryParse(keyAndValue[0], out int key) && int.TryParse(keyAndValue[1], out int value))
            {
                try
                {
                    openAdressHashMap.Add(key, value);
                    Console.WriteLine($"Пара {key};{value} была успешно добавлена в Хэш-таблицу");
                    Console.WriteLine();
                }catch (Exception e) {
                    Console.WriteLine("К сожалению, при попытке добавить новую пару в Хэш-таблицу, случилось переполнение");
                }
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели неправильно пару ключ;значение они должны быть целыми цислами, попробуйте ещё раз");
                continue;
            }

        }
    }

    private static void FindPairFromTable(OpenAdressHashMap<int, int> openAdressHashMap)
    {
        while (true)
        {
            Console.Write("Введите ключ, по которому вы хотите получить пару ключ;значение: ");
            string stringKey = Console.ReadLine();

            if (int.TryParse(stringKey, out int key))
            {
                openAdressHashMap.Get(key);
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели неправильный ключ он должен быть целым числом, попробуйте ещё раз");
                continue;
            }
        }
    }
    private static void RemovePairFromTable(OpenAdressHashMap<int, int> openAdressHashMap)
    {
        while (true)
        {
            Console.Write("Введите ключ, по которому вы хотите удалить пару ключ;значение: ");
            string stringKey = Console.ReadLine();

            if (int.TryParse(stringKey, out int key))
            {
                openAdressHashMap.Remove(key);
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели неправильный ключ он должен быть целым числом, попробуйте ещё раз");
                continue;
            }
        }
    }
    private static void PrintInfoAboutTable(OpenAdressHashMap<int, int> openAdressHashMap)
    {
        Console.WriteLine("Вот как выглядит текущее состояние Хэш-таблицы:");
        openAdressHashMap.Print();

        Console.WriteLine("Коэфициент заполнения равен: "+openAdressHashMap.GetKoef());
        Console.WriteLine("Длина самого длинного кластера равна: " + openAdressHashMap.GetLongestClusterLength());
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
                                  "Введите 1 если да, и что угодно кроме 1, если нет");

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

            if (int.TryParse(stringSizeHashTable, out int size) && size>1)
            {
                HashTableSize = size;
                return;
            }
            else
            {
                Console.WriteLine("Вы ввели неверное число, размер может быть только натуральным числом > 1, попробуйте ещё раз");
            }
        }
    }
}