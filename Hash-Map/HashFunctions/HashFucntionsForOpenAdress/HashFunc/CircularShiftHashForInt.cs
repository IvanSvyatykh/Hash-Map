using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class CircularShiftHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с побитовым сдвигом";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int number = (int)data;
            return (number << 5 | (number >> 27) + attemptNumber) % size;
        }

        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
