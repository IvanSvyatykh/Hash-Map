using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class XorHashForInt : IOpenAdressHashFunc
    {
        public string GetName()
        {
            return "Хэш функции с побитовым сдвигом и с побитовым логическим исключающим ИЛИ";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int key = (int)data;
            string Hin = 224 * '0' + Convert.ToString(key + attemptNumber, 2);

            string[] konstants = new string[]{
            (64 * '0').ToString(),
            "ff00ffff000000ffff0000ff00ffff0000ff00ff00ff00ffff00ff00ff00ff00",
            (64 * '0').ToString()};

            string U = 224 * '0' + Convert.ToString(key + attemptNumber, 2);
            string V = 224 * '0' + Convert.ToString(key + attemptNumber, 2);
            string W = 224 * '0' + Convert.ToString(key + attemptNumber, 2);

            string[] keys = new string[4];
            keys[0] = FunctionP(W);
            for (int i = 1; i < 4; i++)
            {
                U = XorForStrings(FunctionA(U), konstants[i-1]);
                V = FunctionA(FunctionA(V));
                W = XorForStrings(U, V);
                keys[i] = FunctionP(W);
            }

            
            return 0;
        }
        private string XorForStrings(string value1,string value2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 3; i >= 0; i--)
            {
                result.Append((long.Parse(value1.Substring(i*64, 64)) ^ long.Parse(value2.Substring(i*64, 64))).ToString());
            }
           
            return result.ToString();
        }
        private string FunctionA(string value) 
        {
            string xorValue = (long.Parse(value.Substring(192,64)) ^ long.Parse(value.Substring(128, 64))).ToString();
            return xorValue + value.Substring(0, 64)+value.Substring(64,64)+ value.Substring(192, 64);
        }
        private string FunctionP(string value)
        {   
            StringBuilder resValue = new StringBuilder();
            for (int i = 31; i > 0; i--)
            {
                resValue.Append(value.Substring((FIFunction(i)-1)<<3, 8));
            }
            return resValue.ToString();
        }
        private int FIFunction(int arg)
        { 
            int i = arg & 0x03;//получаем число от 0 до 3
            int k = arg >> 2;//получаем число от 0 до 7
            k++;//преобразуем k в число от 1 до 8
            return (i << 3) + k; // умножили i на 8 и добавили k 
        }
        public Func<object, int, int, int> GetHashFunc()
        {
            return HashFunc;
        }
    }
}
