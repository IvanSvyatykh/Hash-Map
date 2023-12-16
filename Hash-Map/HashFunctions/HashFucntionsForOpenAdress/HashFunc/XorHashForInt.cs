using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class XorHashForInt : IOpenAdressHashFunc
    {
        byte[,] S = new byte[8,16]{ // S-блоки, используемые ЦБ РФ
                        { 4, 10,  9,  2, 13,  8,  0, 14,  6, 11,  1, 12,  7, 15,  5,  3},
                        {14, 11,  4, 12,  6, 13, 15, 10,  2,  3,  8,  1,  0,  7,  5,  9},
                        { 5,  8,  1, 13, 10,  3,  4,  2, 14, 15, 12,  7,  6,  0,  9, 11},
                        { 7, 13, 10,  1,  0,  8,  9, 15, 14,  4,  6, 12, 11,  2,  5,  3},
                        { 6, 12,  7,  1,  5, 15, 13,  8,  4, 10,  9, 14,  0,  3, 11,  2},
                        { 4, 11, 10,  0,  7,  2,  1, 13,  3,  6,  8,  5,  9, 12, 15, 14},
                        {13, 11,  4,  1,  3, 15,  5,  9,  0, 10, 14,  7,  6,  8,  2, 12},
                        { 1, 15, 13,  0,  5,  7, 10,  4,  9,  2,  3, 14,  6, 11,  8, 12}};
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

            StringBuilder encValueParts = new StringBuilder();
            for (int i = 0; i < 4; i++) {
                encValueParts.Append(Encryption(Hin.Substring(64 * i, 64), keys[i]));
            }

            string encValue = encValueParts.ToString();

            
        }

        private string Encryption(string value, string key)
        {
            char[] firstPart = value.Substring(0,32).ToCharArray();
            char[] secondPart = value.Substring(32, 32).ToCharArray();

            
            for (int step = 0; step < 3; step++) {       // K1..K24 идут в прямом порядке - три цикла K1..K8
                for (int j = 0; j < 8; j += 1)
                {
                    char[] temp = new char[32];
                    firstPart.CopyTo(temp, 0);
                    
                    firstPart = Convert.ToString(Convert.ToInt32(new string(secondPart),2) ^ EncryptionFunc(firstPart,key.Substring(j*32,32)),2).ToCharArray();
                    secondPart = temp;
                }
            }

            for (int i = 7; i >= 0; i -= 1)
            {
                char[] temp = new char[32];
                firstPart.CopyTo(temp, 0);

                firstPart = Convert.ToString(Convert.ToInt32(new string(secondPart), 2) ^ EncryptionFunc(firstPart, key.Substring(i * 32, 32)), 2).ToCharArray();
                secondPart = temp;
            }

            return new string(firstPart) + new string(secondPart);
        }

        private int EncryptionFunc(char[] bitsOfNumber, string key)
        {
            uint value = (uint)((Convert.ToInt64(new string(bitsOfNumber), 2) + Convert.ToInt64(key.ToString(),2))%4294967296);
            string bitValue = Convert.ToString(value, 16);
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < 8; i++)
            {   
                result.Append(Convert.ToString(S[i, Convert.ToInt32(bitValue[i].ToString(), 16)],16));
            }
            return Convert.ToInt32(result.ToString(), 16)<<11;
        }

        private string XorForStrings(string value1,string value2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 3; i >= 0; i--)
            {
                result.Append((Convert.ToInt64(value1.Substring(i*64, 64)) ^ Convert.ToInt64(value2.Substring(i*64, 64))).ToString());
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
