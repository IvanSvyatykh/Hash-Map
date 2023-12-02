using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map
{
    public static class DataSetsGenerator
    {
        public static Dictionary<int, int> GenerateSet(int size)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Random random = new Random();

            HashSet<int> visited = new HashSet<int>();

            while (visited.Count < size)
            {
                visited.Add(random.Next(0, int.MaxValue));
            }

            foreach (int id in visited)
            {
                dictionary.Add(id, random.Next());
            }
            return dictionary;
        }
    }
}
