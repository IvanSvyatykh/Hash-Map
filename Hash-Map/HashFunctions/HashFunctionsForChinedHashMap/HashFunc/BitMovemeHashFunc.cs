﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFunctionsForChinedHashMap.HashFunc
{
    public class BitMovemeHashFunc : IChinedHashFunc
    {
        public string GetName() => "Хеш-функция на основе битовых операциях.";

        private int Start(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>(hash);          


            long res = int.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                res ^= ((res << 5) + list[i] + (res >> 2));
            }

            res = Math.Abs(res);
            return (int)(res % size);
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
