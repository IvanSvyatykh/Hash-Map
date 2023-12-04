using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map
{
    public static class HashFunctions
    {
        public static int HashWithBitMovement(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }


            long res = 1315423911;
            for (int i = 0; i < list.Count; i++)
            {
                res ^= ((res << 5) + list[i] + (res >> 2));
            }

            res = Math.Abs(res);
            return (int)(res % size);

        }

        public static int HashWithMultiplication(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);


            return (int)Math.Round((double)sum * 0.6180339887 * size) % size;
        }


        public static int HashWithDevision(object data, int size)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);

            if (size < 128)
            {
                sum = sum << list[127 % size];
                sum *= 127 % size;
            }
            else
            {
                sum = sum << list[size % 127];
                sum *= size % 127;
            }
            if (sum > size)
            {
                return (sum / size) % size;
            }
            return (size / sum) % sum;
        }

        public static int LineralyResearchHash(object data, int size, int attemptNumber)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);
            return (sum%68 + attemptNumber) % size;
        }
        public static int QuadraticResearchHash(object data, int size, int attemptNumber)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);
            return (sum%10 + attemptNumber + 2*attemptNumber*attemptNumber) % size;
        }
        public static int DualHash(object data, int size, int attemptNumber)
        {
            byte[] hash = ObjectToByteArray(data);
            List<byte> list = new List<byte>();
            if (hash.Length < 128)
            {
                for (int i = 0; i < 128 - hash.Length; i++)
                {
                    list.Add(0);
                }

                hash.ToList().ForEach(x => list.Add(x));
            }

            int sum = list.Sum(x => x);
            return (sum%size + attemptNumber*(1+sum%(size-1))) % size;
        }
        public static int QuadraticResearchHashForInt(object data, int size, int attemptNumber)
        {
            int sum = (int)data;
            return (sum % size + attemptNumber + 2 * attemptNumber * attemptNumber) % size;
        }
        public static int LineralyResearchHashForInt(object data, int size, int attemptNumber)
        {
            int sum = (int)data;
            return (sum % 68 + attemptNumber) % size;
        }
        public static int DualHashForInt(object data, int size, int attemptNumber)
        {
            int sum = (int)data;
            return (sum % size + attemptNumber * (1 + sum % (size - 1))) % size;
        }
        private static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
