using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptology.Common;

namespace Cryptology
{
    public class CardanoCipher : Cipher
    {
        private const int size = 4;

        public CardanoCipher(string text) : base(text)
        {
        }

        public int[,] RotateMatrix(int[,] mtrx)
        {
            var dim = mtrx.GetLength(0);
            var result = new int[dim, dim];

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    result[i, j] = mtrx[dim-1-j, i];
                }
            }
            return result;
        }
        

        public string[] GetStrArrayFromText(string text)
        {
            int length = text.Length / (size * size);
            var result = new string[length];
            for(int i = 0;i< length; i++)
            {
                result[i] = (text.Substring(i * size*size, size*size));
            }
            return result;
        }

        public override string Encrypt()
        {
            int[,] matrx = new int[size, size];
            matrx = new int[size, size] {
                {1, 0, 0, 0} ,
                {0, 0, 1, 0} ,
                {0, 0, 0, 1} ,
                {0, 0, 1, 0}
            };
            string textExample = Text2Encode;

            int sizeCalc = size*size;

            var result = new StringBuilder();
            char[] charArr = textExample.ToCharArray();
            int value = 0;
            int[] matrixArr = new int[sizeCalc];
            for(int rotation = 0; rotation < size; rotation++)
            {
                matrx = RotateMatrix(matrx);
                for(int i = 0; i < size; i++)
                {
                    for(int j = 0; j < size ; j++)
                    {
                        if(matrx[i, j] == 1)
                        {
                            matrixArr[i*size+j] = matrixArr.Max() + 1;
                        }
                    }
                }
            }
            var textArray = GetStrArrayFromText(textExample);
            foreach(var text in textArray)
            {
                string tmpText = "";
                for(int i = 0; i < text.Length; i++)
                {
                    tmpText = tmpText + text[matrixArr[i] - 1];
                }
                result.Append(tmpText);
            }
            EncodedText = result.ToString();
            return EncodedText;

        }

        public override string Decrypt()
        {

            var result = new StringBuilder(size*size);

            int sizeCalc = size * size;
            var textArray = GetStrArrayFromText(EncodedText);

            var matrx = new int[size, size] {
                {1, 0, 0, 0} ,
                {0, 0, 1, 0} ,
                {0, 0, 0, 1} ,
                {0, 0, 1, 0}
            };
            int[] matrixArr = new int[sizeCalc];
            for (int rotation = 0; rotation < size; rotation++)
            {
                matrx = RotateMatrix(matrx);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (matrx[i, j] == 1)
                        {
                            matrixArr[i * size + j] = matrixArr.Max() + 1;
                        }
                    }
                }
            }
            foreach (var text in textArray)
            {
                var tempResult = new char[size*size];
                for (int i = 0;i<sizeCalc; i++)
                {
                    tempResult[matrixArr[i] - 1] = text[i];
                }
                DecryptedText = DecryptedText + new string(tempResult);
            }
            return DecryptedText;
        }
    }
}
