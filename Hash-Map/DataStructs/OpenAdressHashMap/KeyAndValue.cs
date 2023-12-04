using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.DataStructs.OpenAdressHashMap
{
    internal class KeyAndValue<TKey, TValue>
    {
        private readonly TKey _key;
        private readonly TValue _value;
        public KeyAndValue(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public TKey GetKey() { return _key; }
        public TValue GetValue() { return _value; }
    }
}
