using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_Map.HashFunctions.HashFucntionsForOpenAdress.HashFunc
{
    internal class GostHashForInt : IOpenAdressHashFunc
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
            return "Хэш функции на основе ГОСТ Р 34.11-94";
        }
        private int HashFunc(object data, int size, int attemptNumber)
        {
            int key = (int)data;
            string Hin = Convert.ToString(key, 2).PadLeft(256, '0');
            
            string[] konstants = new string[]{
            "".PadLeft(256, '0'),
            "111111110000000011111111111111110000000000000000111111111111111100000000111111110000000000111111111111111111000000111111110000000011111111111111110000000111111111111111110000000111111111111111111110000111111110000000011111111111111111100000011111111111111111100000011111111111111111110000011111111111111111110000011111111111111111111111000000",
            "".PadLeft(256, '0')};

            string U = Convert.ToString(key, 2).PadLeft(256, '0');
            string V = Convert.ToString(key, 2).PadLeft(256, '0');
            string W = Convert.ToString(key, 2).PadLeft(256, '0');

            string[] keys = new string[4];
            keys[0] = FunctionP(W);
            for (int i = 1; i < 4; i++)
            {
                U = XorForStringsWith256Length(FunctionA(U), konstants[i - 1]);
                V = FunctionA(FunctionA(V));
                W = XorForStringsWith256Length(U, V);
                keys[i] = FunctionP(W);
            }

            StringBuilder encValueParts = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                int k = i != 0 ? 1 : 0;
                encValueParts.Append(Encryption(Hin.Substring(64 * i - k, 64), keys[i]));
            }

            string encValue = encValueParts.ToString();
            
            StringBuilder resultParts = PsiFunction(encValue);
            //for (int i = 0; i < 12; i++)
            //{
            //    resultParts = PsiFunction(resultParts.ToString());
            //}
            //resultParts = new StringBuilder(XorForStringsWith256Length(Hin, resultParts.ToString()));

            //for (int i = 0; i < 61; i++)
            //{
            //    resultParts = PsiFunction(resultParts.ToString());
            //}

            int indexBeforeNormalization = (int)(Convert.ToUInt32(resultParts.ToString().Substring(223, 32), 2)% 2147483648);
            indexBeforeNormalization = indexBeforeNormalization % 2 == 0 ? indexBeforeNormalization + 1 : indexBeforeNormalization;

            return (indexBeforeNormalization + attemptNumber) % size;
        }

        private StringBuilder PsiFunction(string encValue)
        {
            StringBuilder resultParts = new StringBuilder();
            string lastPartOfResult = encValue.Substring(0, 16);

            for (int i = 1; i < 16; i++)
            {
                lastPartOfResult = XorForStringsWith16Length(lastPartOfResult, encValue.Substring(16 * i - 1, 16));
            }
            resultParts.Append(lastPartOfResult);

            for (int i = 15; i > 0; i--)
            {
                resultParts.Append(encValue.Substring(16 * i - 1, 16));
            }

            return resultParts;
        }

        private string Encryption(string value, string key)
        {
            char[] firstPart = value.Substring(0,32).ToCharArray();
            char[] secondPart = value.Substring(31, 32).ToCharArray();

            for (int step = 0; step < 3; step++) {       // K1..K24 идут в прямом порядке - три цикла K1..K8
                for (int j = 0; j < 8; j += 1)
                {
                    char[] temp = new char[32];
                    firstPart.CopyTo(temp, 0);

                    int k = j != 0 ? 1 : 0;
                    firstPart = Convert.ToString(Convert.ToUInt32(new string(secondPart),2) ^ EncryptionFunc(firstPart,key.Substring(j*32 - k,32)),2).PadLeft(32, '0').ToCharArray();
                    secondPart = temp;
                }
            }

            for (int i = 7; i >= 0; i -= 1)
            {
                char[] temp = new char[32];
                firstPart.CopyTo(temp, 0);

                int k = i != 0 ? 1 : 0;
                firstPart = Convert.ToString(Convert.ToUInt32(new string(secondPart), 2) ^ EncryptionFunc(firstPart, key.Substring(i * 32 - k, 32)), 2).PadLeft(32, '0').ToCharArray();
                secondPart = temp;
            }

            return new string(firstPart) + new string(secondPart);
        }

        private uint EncryptionFunc(char[] bitsOfNumber, string key)
        {
            uint value = (uint)((Convert.ToUInt64(new string(bitsOfNumber), 2) + Convert.ToUInt64(key.ToString(),2))%4294967296);
            string bitValue = Convert.ToString(value, 16).PadLeft(8, '0');
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < 8; i++)
            {   
                result.Append(Convert.ToString(S[i, Convert.ToUInt32(bitValue[i].ToString(), 16)],16));
            }
            string stringRes = result.ToString();
            uint uintRes = Convert.ToUInt32(stringRes,16);
            return uintRes << 11;
        }
        private string XorForStringsWith16Length(string value1, string value2)
        {
            return Convert.ToString(Convert.ToUInt16(value1, 2) ^ Convert.ToUInt16(value2, 2), 2).PadLeft(16, '0');
        }
        private string XorForStringsWith256Length(string value1,string value2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 7; i >= 0; i--)
            {
                int k = i != 0 ? 1 : 0;
                result.Append(Convert.ToString(Convert.ToUInt32(value1.Substring(i*32-k, 32),2) ^ Convert.ToUInt32(value2.Substring(i*32 - k, 32),2),2).PadLeft(32, '0'));
            }
           
            return result.ToString();
        }
        private string FunctionA(string value) 
        {
            string xorValuePart1 = Convert.ToString(Convert.ToUInt32(value.Substring(191, 32),2) ^ Convert.ToUInt32(value.Substring(127, 32),2),2).PadLeft(32, '0');
            string xorValuePart2 = Convert.ToString(Convert.ToUInt32(value.Substring(223, 32), 2) ^ Convert.ToUInt32(value.Substring(159, 32), 2), 2).PadLeft(32, '0');
            return xorValuePart1 + xorValuePart2 + value.Substring(0, 64)+value.Substring(63,64)+ value.Substring(191, 64);
        }
        private string FunctionP(string value)
        {   
            StringBuilder resValue = new StringBuilder();
            for (int i = 32; i > 0; i--)
            {
                resValue.Append(value.Substring(((FIFunction(i)-1)<<3) - 1, 8));
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
