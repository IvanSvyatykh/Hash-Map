﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hash_Map.DataStructs.ChainedHashMap;

namespace Hash_Map.DataStructs.OpenAdressHashMap
{
    internal class OpenAdressHashMap<TKey, TValue> : IHashMap<TKey, TValue>
    {
        public int Size { get; private set; }

        private bool IsExceptionNeed;

        private Func<object, int, int, int> hashFunc;

        private KeyAndValue<TKey, TValue>[] hashMapValues;
        public OpenAdressHashMap(int size, Func<object, int, int, int> hashFunc, bool isExceptionNeed)
        {
            Size = size;
            this.hashFunc = hashFunc;
            hashMapValues = new KeyAndValue<TKey, TValue>[size];
            IsExceptionNeed = isExceptionNeed;
        }

        public void Add(TKey key, TValue value)
        {
            int attemptNumber = 0;
            for (; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, Size, attemptNumber);
                if (hashMapValues[index] is null)
                {
                    hashMapValues[index] = new KeyAndValue<TKey, TValue>(key, value);
                    return;
                }
                else if (Equals(hashMapValues[index].GetKey(), key))
                {
                    throw new InvalidOperationException("В таблице уже существует пара с таким ключём");
                }
            }

            if (IsExceptionNeed)
            {
                throw new InvalidOperationException("Cлучилось переполнение хэш таблицы.");
            }

            IncreaseTableSize();
            Add(key, value);
        }

        private void IncreaseTableSize()
        {
            Size = Size * 2;
            KeyAndValue<TKey, TValue>[] oldHashMapValues = hashMapValues.ToArray();
            hashMapValues = new KeyAndValue<TKey, TValue>[Size];
            foreach (KeyAndValue<TKey, TValue> pair in oldHashMapValues)
            {
                if (pair is not null)
                {
                    Add(pair.GetKey(), pair.GetValue());
                }
            }
        }

        public void Remove(TKey key)
        {
            for (int attemptNumber = 0; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, Size, attemptNumber);
                if (hashMapValues[index] is not null && Equals(hashMapValues[index].GetKey(), key))
                {
                    Console.WriteLine($"Пара с ключем {key}, а именно " +
                        $"{hashMapValues[index].GetKey()};{hashMapValues[index].GetValue()} была успешно удалена из Хэш-таблицы");
                    hashMapValues[index] = null;
                    return;
                }
            }

            Console.WriteLine($"Элемента с данным ключём {key} нет в Хэш-таблице");
        }
        public void Get(TKey key)
        {
            for (int attemptNumber = 0; attemptNumber < Size; attemptNumber++)
            {
                int index = hashFunc(key, Size, attemptNumber);
                if (hashMapValues[index] is not null && Equals(hashMapValues[index].GetKey(), key))
                {
                    Console.WriteLine($"Вот искомая пара с ключем {key} из Хэш-таблицы:" +
                        $" {hashMapValues[index].GetKey()};{hashMapValues[index].GetValue()}");
                    return;
                }
            }

            Console.WriteLine($"Элемента с данным ключём {key} нет в Хэш-таблице");
        }

        public double GetKoef() => hashMapValues.ToList().Sum(x => x is not null ? 1 : 0) / (double)Size;

        public int GetLongestClusterLength()
        {
            int maxClusterLength = 0;
            int currentClusterLength = 0;
            for (int i = 0; i < Size; i++)
            {
                if (hashMapValues[i] is null)
                {
                    maxClusterLength = Math.Max(currentClusterLength, maxClusterLength);
                    currentClusterLength = 0;
                }
                else
                {
                    currentClusterLength++;
                }
            }

            maxClusterLength = Math.Max(currentClusterLength, maxClusterLength);
            return maxClusterLength;
        }

        public void Print()
        {
            foreach (KeyAndValue<TKey, TValue> keyAndValue in hashMapValues)
            {
                if (keyAndValue is not null)
                {
                    Console.WriteLine(keyAndValue.ToString());
                }
                else
                {
                    Console.WriteLine("null; null");
                }

            }
        }
    }
}
