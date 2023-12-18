using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.DataStructs.ChainedHashMap
{
    public class MyLinkedList<TKey, TValue> : IEnumerable<Node<TKey, TValue>>
    {
        public Node<TKey, TValue> Head { get; protected set; }

        public Node<TKey, TValue> Tail { get; protected set; }

        public MyLinkedList()
        {

        }

        public int Count { get; protected set; } = 0;

        public bool IsEmpty() => Head == null;

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public Node<TKey, TValue> Get(TKey key)
        {
            foreach (var item in this)
            {
                if (Equals(key, item.Key))
                {
                    return item;
                }
            }

            return null;
        }

        private bool Check(TKey key, TValue value)
        {
            foreach (var item in this)
            {
                if (Equals(key, item.Key) && Equals(value,item.Value))
                {
                    return true;
                }
            }

            return false;
        }


        public void Add(TKey key, TValue value)
        {
            if (Check(key, value))
            {
                throw new ArgumentException($"Hash-Map already conatains same Element");
            }
            if (IsEmpty())
            {
                Tail = Head = new Node<TKey, TValue>(key, value, null);
            }
            else
            {
                var item = new Node<TKey, TValue>(key, value, null);
                Tail.Next = item;
                Tail = item;
            }
            Count++;
        }


        public IEnumerator<Node<TKey, TValue>> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void RemoveSameElement(TKey key)
        {
            int count = 0;

            foreach (var el in this)
            {
                if (Equals(el.Key, key))
                {
                    RemoveAt(count);
                    return;
                }
                else
                {
                    count++;
                }

            }

            throw new ArgumentException($"Element with key {key} does not implement in Hash-Map");
        }

        public void RemoveAt(int index)
        {
            int count = 0;
            Node<TKey, TValue> current = Head;

            if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("index is outside of list");

            if (index == 0)
            {
                Head = Head.Next;
            }
            else
            {
                while (count != index - 1)
                {
                    current = current.Next;
                    count++;
                }

                if (index == Count - 1)
                {
                    Tail = current;
                }

                current.Next = current.Next.Next;
            }

            Count--;
        }
    }
    public class Node<K, V>
    {
        public V Value { get; set; }
        public V RealKey {  get; set; }
        public Node<K, V> Next { get; set; }
        public K Key { get; private set; }

        public Node(K key, V value, Node<K, V> next)
        {
            Key = key;
            Value = value;
            Next = next;
        }
    }
}
