﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class XorHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с побитовым логическим исключающим ИЛИ";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int number = (int)data;
            return (number << 5 ^ attemptNumber) % size;
        }
        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
