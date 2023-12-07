using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress
{
    internal interface IOpenAdressHashFunc
    {
        public string GetName();
        public Func<object, int, int, int> GetHashFunc();
    }
}
