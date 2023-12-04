using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hash_Map.DataStructs.ChainedHashMap;

namespace Hash_Map.DataStructs.OpenAdressHashMap
{
    internal class OpenAdressHashMap<TKey, TValue> : IHashMap<TKey, TValue>
    {
        public int Size { get; private set; }

        private Func<object, int, int> hashFunc;

        private KeyAndValue<TKey, TValue>[] hashMapValues;
        public OpenAdressHashMap(int size, Func<object, int, int> hashFunc)
        {
            Size = size;
            this.hashFunc = hashFunc;
            hashMapValues = new KeyAndValue<TKey, TValue>[size];
        }

        public void Add(TKey key, TValue value)
        {
            for (int attemptNumber = 0; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, attemptNumber);
                if (hashMapValues[index] is null)
                {
                    hashMapValues[index] = new KeyAndValue<TKey, TValue>(key, value);
                    return;
                }
            }
            throw new InvalidOperationException("Случилось переполнение хэш таблицы");
        }

        public void Remove(TKey key)
        {
            for (int attemptNumber = 0; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, attemptNumber);
                if (hashMapValues[index] is not null && Equals(hashMapValues[index].GetKey(), key))
                {
                    hashMapValues[index] = null;
                }
            }

            throw new InvalidOperationException($"Элемента с данным ключём {key} нет в хэш таблице");
        }
        public TValue Get(TKey key)
        {
            for (int attemptNumber = 0; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, attemptNumber);
                if (hashMapValues[index] is not null && Equals(hashMapValues[index].GetKey(), key))
                {
                    return hashMapValues[index].GetValue();
                }
            }

            throw new InvalidOperationException($"Элемента с данным ключём {key} нет в хэш таблице");
        }

        public double GetKoef() => hashMapValues.ToList().Sum(x => x is not null ? 1 : 0) / (double)Size;

        public int GetLongestCluster()
        {
            int maxClusterLength = 0;
            int currentClusterLength = 0;
            for (int i = 0; i < Size; i++)
            {
                if (hashMapValues[i] is null){
                    maxClusterLength = Math.Max(currentClusterLength, maxClusterLength);
                    currentClusterLength = 0;
                }
                else
                {
                    currentClusterLength++;
                }
            }
            return maxClusterLength;
        }
    }
}
