using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptology.Common;

namespace Cryptology
{
    public class AffineCipher : Cipher
    {
        private readonly List<double> frequencyTbl = new List<double>
        {
            0.64297, 0.11746, 0.21902, 0.33483, 1.00000, 0.17541,
            0.15864, 0.47977, 0.54842, 0.01205, 0.06078, 0.31688, 0.18942,
            0.53133, 0.59101, 0.15187, 0.00748, 0.47134, 0.49811, 0.71296,
            0.21713, 0.07700, 0.18580, 0.01181, 0.15541, 0.00583
        };
        private List<double> freq;


        private readonly int a;
        private readonly int b;

        public AffineCipher(string text, int a, int b) : base(text)
        {
            this.a = a;
            this.b = b;
            freq = new List<double>(new double[26]);
        }

        public override string Encrypt()
        {
            string cipherText = "";
            char[] chars = Text2Encode.ToUpper().ToCharArray();

            foreach (char c in chars)
            {
                int x = Convert.ToInt32(c - 65);
                cipherText += Convert.ToChar(((a * x + b) % 26) + 65);
            }

            EncodedText = cipherText;
            return EncodedText;
        }

        public override string Decrypt()
        {
            string plainText = "";
            int aInverse = MultiplicativeInverse(a);
            char[] chars = EncodedText.ToUpper().ToCharArray();
            
            foreach (char c in chars)
            {
                int x = Convert.ToInt32(c - 65);
                if (x - b < 0) x = Convert.ToInt32(x) + 26;
                plainText += Convert.ToChar(((aInverse * (x - b)) % 26) + 65);
            }

            DecryptedText = plainText;
            return DecryptedText;
        }

        public override string Decode()
        {
            var items = new int[12] {1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25};

            var lowercaseText = EncodedText.ToLower();
            foreach (var letter in lowercaseText)
            {
                if (letter >= 97 && letter <= 122)
                {
                    freq[letter - 97] += 1;
                }
            }

            var maxValue = freq.Max();
            for (int i = 0; i < frequencyTbl.Count; i++)
            {
                freq[i] = freq[i] / maxValue;
            }

            double[] minimum = {0, 10000};
            foreach (var item in items)
            {

                for (int i = 0; i < 26; i++)
                {
                    var e = GetError();
                    Console.Write(e + " ");
                    Console.WriteLine(i);
                    if (e < minimum[1])
                    {
                        minimum[1] = e;
                        minimum[0] = i;
                    }

                    var x = freq[(item* freq.Count - i)%26] ;
                    frequencyTbl.RemoveAt((item * frequencyTbl.Count - i)%26);
                    frequencyTbl.Insert(0, x);
                }
            }

            return minimum[1].ToString();
        }

        private double GetError()
        {
            double e = 0;
            for (int i = 0; i < frequencyTbl.Count; i++)
            {
                e += Math.Abs(Math.Pow(this.freq[i] - this.frequencyTbl[i], 2));
            }

            return e;
        }

        //helper methods
        public static int MultiplicativeInverse(int a)
        {
            for (int x = 1; x < 27; x++)
            {
                if ((a * x) % 26 == 1)
                    return x;
            }

            throw new Exception("No multiplicative inverse found!");
        }
    }
}
