using Cryptology.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptology
{
    public class VigenereCipher : Cipher
    {
        private string key;
        public VigenereCipher(string text, string key) : base(text)
        {
            this.key = key;
        }


        public override string Encrypt()
        {
            string output = string.Empty;
            for (int i = 0; i < Text2Encode.Length; ++i)
            {
                bool cIsUpper = char.IsUpper(Text2Encode[i]);
                char offset = cIsUpper ? 'A' : 'a';
                int keyIndex = (i) % key.Length;
                int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                char ch = (char)((Mod(((Text2Encode[i] + k) - offset), 26)) + offset);
                output += ch;
            }
            EncodedText = output;
            return EncodedText;
        }

        public override string Decrypt()
        {
            string output = string.Empty;
            for (int i = 0; i < EncodedText.Length; ++i)
            {
                bool cIsUpper = char.IsUpper(EncodedText[i]);
                char offset = cIsUpper ? 'A' : 'a';
                int keyIndex = (i) % key.Length;
                int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                char ch = (char)((Mod(((EncodedText[i] - k) - offset), 26)) + offset);
                output += ch;
            }

            DecryptedText = output;
            return DecryptedText;
        }
        public int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }
    }
}
