using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cryptology.Common
{
    public class Cipher
    {

        public string Text2Encode { get; set; }
        public string EncodedText { get; set; }
        public string DecryptedText { get; set; }


        public Cipher(string text)
        {
            Text2Encode = text.Replace(" ", "").Replace(".","");
        }
        //We transform initial data
        public virtual string Encrypt()
        {
            return Text2Encode;
        }

        //We know the key
        public virtual string Decrypt()
        {
            return EncodedText;
        }

        //We do not know the key
        public virtual string Decode()
        {
            return EncodedText;
        }

    }
}
