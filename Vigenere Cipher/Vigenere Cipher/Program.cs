using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Vigenere_Cipher
{
    class Program
    {
        private static readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_. ";
        private static readonly string filePath = "loremIpsum.txt";

        private static readonly int alphabetLength = alphabet.Length;

        static int GetPosition(char letter)
        {
            return alphabet.IndexOf(char.ToUpper(letter));
        }

        static int Modulo(int a, int b)
        {
            return (a + b) % alphabetLength;
        }

        static string Encrpyt(string text, string key)
        {
            StringBuilder outputText = new StringBuilder(text);

            int keyIterator = 0;

            for (int i = 0; i < text.Length; i++)
            {
                var outputTextLetterPositionInAlphabet = GetPosition(outputText[i]);

                if (outputTextLetterPositionInAlphabet >= 0)
                {
                    var keyLetterPositionInAlphabet = GetPosition(key[keyIterator]);

                    var encryptedLetter = alphabet[Modulo(GetPosition(alphabet[outputTextLetterPositionInAlphabet]), GetPosition(alphabet[keyLetterPositionInAlphabet]))];

                    outputText[i] = char.IsUpper(outputText[i]) ? encryptedLetter : char.ToLower(encryptedLetter);
                }

                keyIterator = keyIterator + 1 == key.Length ? 0 : ++keyIterator;
            }

            return outputText.ToString();
        }

        static string Decrypt(string encryptedText, string key)
        {
            StringBuilder outputText = new StringBuilder(encryptedText);

            int keyIterator = 0;

            for (int i = 0; i < encryptedText.Length; i++)
            {
                var outputTextLetterPositionInAlphabet = GetPosition(outputText[i]);

                if (outputTextLetterPositionInAlphabet >= 0)
                {
                    var keyLetterPositionInAlphabet = GetPosition(key[keyIterator]);

                    char encryptedLetter;

                    if (outputTextLetterPositionInAlphabet >= keyLetterPositionInAlphabet)
                    {
                        encryptedLetter = alphabet[outputTextLetterPositionInAlphabet - keyLetterPositionInAlphabet];

                        outputText[i] = char.IsUpper(outputText[i]) ? encryptedLetter : char.ToLower(encryptedLetter);
                    }
                    else
                    {
                        encryptedLetter = alphabet[alphabetLength - Math.Abs(outputTextLetterPositionInAlphabet - keyLetterPositionInAlphabet) % alphabetLength];

                        outputText[i] = char.IsUpper(outputText[i]) ? encryptedLetter : char.ToLower(encryptedLetter);
                    }
                }

                keyIterator = keyIterator + 1 == key.Length ? 0 : ++keyIterator;
            }

            return outputText.ToString();
        }

        static void Main(string[] args)
        {
            string text = string.Empty;

            Console.Write("Cipher: ");
            string cipher = Console.ReadLine();

            Console.Write("Plain text: ");
            text = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Ciphering");
            string encryptedText = Encrpyt(text, cipher);
            Console.WriteLine($"Encrypted text: {encryptedText}");

            Console.WriteLine();

            Console.WriteLine("Deciphering");
            Console.WriteLine($"Decrypted text: {Decrypt(encryptedText, cipher)}");

            if (File.Exists(filePath))
            {
                text = string.Join(" ", File.ReadAllLines(filePath).Where(l => !string.IsNullOrWhiteSpace(l)));

                encryptedText = Encrpyt(text, cipher);

                var streamWriter = new StreamWriter("Encrypted.txt");

                streamWriter.Write(encryptedText);

                streamWriter = new StreamWriter("Decrypted.txt");

                streamWriter.Write(Decrypt(encryptedText, cipher));

                streamWriter.Close();
            }
            else
            {
                Console.WriteLine("Couldn't find given txt file");
            }
        }
    }
}
