﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class BitOperationHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с побитовыми операциями";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int key = (int)data;

            key += ~(key << 16);
            key ^= (key >> 5);
            key += (key << 3);
            key ^= (key >> 13);
            key += ~(key << 9);
            key ^= (key >> 17);

            return (key + attemptNumber) %size;
        }

        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}