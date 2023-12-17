using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFunctionsForChinedHashMap.HashFunc
{
    public class MD5 : IChinedHashFunc
    {

        public string GetName() => "Хеш-функция MD5";

        private int Start(object data, int size)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {

                byte[] hash = ObjectToByteArray(data);
                byte[] hashBytes = md5.ComputeHash(hash);
                return Math.Abs(BitConverter.ToInt32(md5.ComputeHash(hashBytes))%size);
            }
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

