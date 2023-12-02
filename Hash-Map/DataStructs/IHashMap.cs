using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.DataStructs
{
    public interface IHashMap<TKey, TValue>
    {
        public double GetKoef();
        public int GetShortestChain();
        public int GetLongestChain();
        public TValue Get(TKey key);
        public void Add(TKey key, TValue value);
    }
}
