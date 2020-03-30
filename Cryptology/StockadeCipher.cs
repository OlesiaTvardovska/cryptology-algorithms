using Cryptology.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptology
{
    public class StockadeCipher : Cipher
    {
        private int _key;
        private Random _random;

        private List<List<char>> arrayOfLetters;
        public StockadeCipher(string text) : base(text)
        {
            _key = (int)Math.Floor(Math.Sqrt(Text2Encode.Length));
            _random = new Random();

            arrayOfLetters = new List<List<Char>>();
            for (int i = 0; i < _key; i++)
            {
                var a = new List<char>();
                arrayOfLetters.Add(a);
            }
        }

        public override string Encrypt()
        {
            int i = 0;
            while (i < Text2Encode.Length)
            {
                for (int j = 0; j < _key; j++)
                {
                    arrayOfLetters[j].Add(Text2Encode[i]);
                    i++;
                }
                for (int k = _key - 2; k > 0 && i < Text2Encode.Length; k--)
                {
                    arrayOfLetters[k].Add(Text2Encode[i]);
                    i++;
                }
            }

            //check if needed some extra junk symbols
            double newKey = (double)(Text2Encode.Length - 1) / (double)(2*(_key - 1));
            var res = newKey % 1 == 0 ? 0 : (Text2Encode.Length - 1) / (2 * ((int)newKey + 1) - 1);
            var numOfElems = res * (_key - 1) * 2 + 1 - Text2Encode.Length;
            //add some junk
            while (numOfElems > 0)
            {
                for (int k = 0; k < _key && numOfElems > 0; k++)
                {
                    arrayOfLetters[k].Add((char)_random.Next('a', 'z'));
                    numOfElems--;
                }
                for (int k = _key - 1; k > 0 && numOfElems > 0; k--)
                {
                    arrayOfLetters[k].Add((char)_random.Next('a', 'z'));
                    numOfElems--;
                }
            }

            var result = new StringBuilder();
            for (int iter = _key - 1; iter >= 0; iter--)
            {
                result.Append(new string(arrayOfLetters[iter].ToArray()));
            }
            EncodedText = result.ToString();
            return EncodedText;
        }

        public override string Decrypt()
        {
            double newKey = (double)(Text2Encode.Length - 1) / (double)(2 * (_key - 1));
            var res = newKey % 1 == 0 ? 0 : (Text2Encode.Length - 1) / (2 * ((int)newKey + 1) - 1);

            arrayOfLetters = new List<List<Char>>();
            for (int iteration = 0; iteration < _key; iteration++)
            {
                var a = new List<char>();
                arrayOfLetters.Add(a);
            }
            var iter = 0;
            arrayOfLetters[0].AddRange(EncodedText.Substring(0, res));
            iter += res;
            for(int m = 1;m< _key-1; m++)
            {
                arrayOfLetters[m].AddRange(EncodedText.Substring(iter,res*2));
                iter += res * 2;
            }
            arrayOfLetters[_key-1].AddRange(EncodedText.Substring(iter, res + 1));


            var result = new StringBuilder();
            int i = 0;
            result.Append((arrayOfLetters[_key - 1][0]));
            arrayOfLetters[_key - 1].RemoveAt(0);
            while (i < Text2Encode.Length)
            {
                for (int k = _key - 2; k > 0 && i < Text2Encode.Length; k--)
                {
                    result.Append(arrayOfLetters[k][0]);
                    arrayOfLetters[k].RemoveAt(0);
                    i++;
                }
                for (int j = 0; j < _key && i < Text2Encode.Length; j++)
                {
                    result.Append(arrayOfLetters[j][0]);
                    arrayOfLetters[j].RemoveAt(0);
                    i++;
                }
            }

            DecryptedText = result.ToString();
            return DecryptedText;
        }

    }
}
