using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFunctionsForChinedHashMap.HashFunc
{
    public class MyXOR : IChinedHashFunc
    {
        public string GetName() => "Хеш-функция на основе XOR.";

        private int Start(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>(hash);

            int res = list.Sum(b => b);
            int x = res ^ size;

            return x % size;

        }

        public Func<object, int, int> GetHashFunc()
        {
            return Start;
        }

        private static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
