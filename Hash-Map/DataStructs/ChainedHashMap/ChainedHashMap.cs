using Hash_Map.DataStructs.OpenAdressHashMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hash_Map.DataStructs.ChainedHashMap
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

        public void Get(TKey key)
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

        public void Remove(TKey key)
        {
            int hash = _hashFunc(key, Size);
            Node<TKey, TValue> node = null;

            if (_values[hash].Count != 0)
            {
                MyLinkedList<TKey, TValue> nodes = _values[hash];
                nodes.RemoveSameElement(key);

            }
            else
            {
                throw new ArgumentException($"Hash-Map does not contains element with given key {key.ToString()}");
            }
        }

        public void Test(Dictionary<TKey, TValue> set)
        {
            if (set.Count > Size)
            {
                throw new ArgumentException($"Size of set more than Hash-Map.");
            }
            _values = new MyLinkedList<TKey, TValue>[Size];

            foreach (var el in set)
            {
                Add(el.Key, el.Value);
            }
        }
        public double GetKoef() => _values.ToList().Sum(x => x.Count) / (double)Size;

        public int GetShortestChain() => _values.ToList().Min(x => x.Count);

        public int GetLongestChain() => _values.ToList().Max(x => x.Count);


        public void Print()
        {
            foreach (var el in _values)
            {
                if (el is not null)
                {
                    bool count = true;
                    foreach (var e in el)
                    {
                        Console.Write($"({e.Key};{e.Value})->");
                    }
                }
                else
                {
                    Console.Write("null; null");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Длина макисмальной цепочки:{GetLongestChain()}");
        }

    }
}
