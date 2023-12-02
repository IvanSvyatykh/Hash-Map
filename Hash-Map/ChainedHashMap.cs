using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hash_Map
{
    public class ChainedHashMap<TKey, TValue> : IHashMap<TKey, TValue>
    {
        public int Size { get; private set; }

        private MyLinkedList<TKey, TValue>[] _values;

        private Func<object, int, int> _hashFunc;

        public ChainedHashMap(int size, Func<object, int, int> hash)
        {
            Size = size;
            _values = new MyLinkedList<TKey, TValue>[size];
            _hashFunc = hash;
            for (int i = 0; i < size; i++)
            {
                _values[i] = new MyLinkedList<TKey, TValue>();
            }

        }

        public TValue Get(TKey key)
        {
            int hash = _hashFunc(key, Size);
            Node<TKey, TValue> node = null;

            if (_values[hash].Count != 0)
            {
                MyLinkedList<TKey, TValue> nodes = _values[hash];
                node = nodes.Get(key);

            }
            if (node == null)
            {
                throw new ArgumentException($"Hash-Map does not contains element with given key {key.ToString()}");
            }

            return node.Value;
        }

        public void Add(TKey key, TValue value)
        {
            int hash = _hashFunc(key, Size);

            if (_values[hash].Count == 0)
            {
                MyLinkedList<TKey, TValue> nodes = new MyLinkedList<TKey, TValue>();
                nodes.Add(key, value);
                _values[hash] = nodes;
            }
            else
            {
                _values[hash].Add(key, value);
            }

        }

        public double GetKoef() => (_values.ToList().Sum(x => x.Count) / (double)Size);

        public int GetShortestChain() => _values.ToList().Min(x => x.Count);

        public int GetLongestChain() => _values.ToList().Max(x => x.Count);

    }
}
