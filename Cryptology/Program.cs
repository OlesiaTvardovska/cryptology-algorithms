using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptology
{
    class Program
    {
        static void Main(string[] args)
        {

            var startString =
                "A Cardan grille is made from a sheet of fairly rigid paper or parchment, or from thin metal. The paper is ruled to represent lines of handwriting and rectangular areas are cut out at arbitrary intervals between these lines.\r\n\r\nAn encipherer places the grille on a sheet of paper and writes his message in the rectangular apertures, some of which might allow a single letter, a syllable, or a whole word. Then, removing the grille, the fragments are filled out to create a note or letter that disguises the true message. Cardano suggested drafting the text three times in order to smooth any irregularities that might indicate the hidden words.";
            string text = File.ReadAllText(@"C:\Users\user\source\repos\Cryptology\Cryptology\Texts\text.txt");
            //var cypher = new CaeserCipher(text, 1);
            var cypher = new AffineCipher(text, 3, 4);

            Console.WriteLine("Encoded text:");
            var encryptedText = cypher.Encrypt();
            Console.WriteLine(encryptedText);
            File.WriteAllText(@"C:\Users\user\source\repos\Cryptology\Cryptology\Texts\textEncrypted.txt",encryptedText);


            Console.WriteLine("\nDecrypted text:");
            var decryptedText = cypher.Decrypt();
            Console.WriteLine(decryptedText);
            File.WriteAllText(@"C:\Users\user\source\repos\Cryptology\Cryptology\Texts\textDecrypted.txt", decryptedText);

            Console.WriteLine("\nDecoded text:");
            var decodedText = cypher.Decode();
            Console.WriteLine(decodedText);
            File.WriteAllText(@"C:\Users\user\source\repos\Cryptology\Cryptology\Texts\textDecoded.txt", decodedText);
            Console.ReadKey();
        }
    }
}
