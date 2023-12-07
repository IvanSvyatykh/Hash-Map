using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress
{
    internal class LineralyResearchHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с линейным исследованием";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int key = (int)data;
            return (key % 68 + attemptNumber) % size;
        }
        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
