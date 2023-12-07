using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress
{
    internal class DualHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с двойным хэшированием";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int sum = (int)data;
            return (sum % size + attemptNumber * (1 + sum % (size - 1))) % size;
        }
        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
