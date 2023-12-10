using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFunctionsForChinedHashMap.HashFunc
{
    public class DivHashFunc : IChinedHashFunc
    {
        public string GetName() => "Хеш-функция на основе деления";

        private int Start(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);

            if (size < 128)
            {
                sum = sum << list[127 % size];
                sum *= 127 % size;
            }
            else
            {
                sum = sum << list[size % 127];
                sum *= size % 127;
            }
            if (sum > size)
            {
                return (sum / size) % size;
            }
            return (size / sum) % sum;
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
