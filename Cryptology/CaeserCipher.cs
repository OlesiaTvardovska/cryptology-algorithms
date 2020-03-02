using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptology.Common;

namespace Cryptology
{
    public class CaeserCipher : Cipher
    {
        private readonly List<double> frequencyTbl = new List<double>
        {
            0.64297, 0.11746, 0.21902, 0.33483, 1.00000, 0.17541,
            0.15864, 0.47977, 0.54842, 0.01205, 0.06078, 0.31688, 0.18942,
            0.53133, 0.59101, 0.15187, 0.00748, 0.47134, 0.49811, 0.71296,
            0.21713, 0.07700, 0.18580, 0.01181, 0.15541, 0.00583
        };
        private List<double> freq;
        private int key { get; set; }


        public CaeserCipher(string text, int key) : base(text)
        {
            freq = new List<double>(new double[26]);
            this.key = key;
        }

        public static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
            
        }

        public override string Encrypt()
        {
            var mainKey = this.key;
            string output = string.Empty;

            foreach (char ch in Text2Encode)
                output += Cipher(ch, mainKey);

            EncodedText = output;
            return EncodedText;
        }

        public override string Decrypt()
        {
            var mainKey = 26 - this.key;
            string output = string.Empty;

            foreach (char ch in EncodedText)
                output += Cipher(ch, mainKey);

            DecryptedText = output;
            return DecryptedText;
        }

        public override string Decode()
        {
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

            var mainKey = 26 - GetMinError();
            string output = string.Empty;

            foreach (char ch in EncodedText)
                output += Cipher(ch, mainKey);
            return output;
        }



        //helper functions
        private double GetError()
        {
            double e = 0;
            for (int i = 0; i < frequencyTbl.Count; i++)
            {
                e += Math.Abs(Math.Pow(this.freq[i] - this.frequencyTbl[i], 2));
            }

            return e;
        }

        private int GetMinError()
        {
            double[] minimum = { 0, 10000 };

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

                var x = freq[freq.Count - 1];
                frequencyTbl.RemoveAt(frequencyTbl.Count - 1);
                frequencyTbl.Insert(0, x);
            }

            return (int)minimum[0];
        }
    }
}
