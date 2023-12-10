using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFunctionsForChinedHashMap
{
    public interface IChinedHashFunc
    {
        public string GetName();

        public Func<object, int, int> GetHashFunc();



    }
}
