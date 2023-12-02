using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map
{
    public static class HashFunctions
    {
        public static int HashWithDevision(object data, int size)
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
                sum = sum << list[128 % size];
                sum *= 128 % size;
            }
            else
            {
                sum = sum << list[size % 128];
                sum *= size % 128;
            }
            if (sum > size)
            {
                return (sum / size) % size;
            }
            return (size / sum) % sum;
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
