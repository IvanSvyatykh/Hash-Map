using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class QuadraticResearchHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с квадратичным исследованием";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int sum = (int)data;
            return (sum % size + attemptNumber + 2 * attemptNumber * attemptNumber) % size;
        }
        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
