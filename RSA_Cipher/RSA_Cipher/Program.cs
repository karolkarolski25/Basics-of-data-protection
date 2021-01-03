using System;
using System.IO;
using System.Linq;

namespace RSA_Cipher
{
    public class Program
    {
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            ulong p = NumberGenerator.GeneratePrimeNumber((ulong)random.Next(1000, 9999));
            ulong q = NumberGenerator.GeneratePrimeNumber((ulong)random.Next(1000, 9999));

            ulong n = p * q;
            ulong phi = (p - 1) * (q - 1);

            ulong e = NumberGenerator.GenerateRelativelyPrimeNumber(phi);

            ulong d = NumberGenerator.GenerateDNumber(e, phi);

            Console.WriteLine();
            Console.WriteLine($"Liczby pierwsze p i q: |{p}|{q}|");
            Console.WriteLine();
            Console.WriteLine($"Klucz prywatny: d, n |{d}|{n}|");
            Console.WriteLine();
            Console.WriteLine($"Klucz publiczny: e, n |{e}|{n}|");

            var cipher = new Cipher(n, e, d);

            try
            {
                string text = File.ReadAllText("plainText.txt");

                if (text.Length != 50)
                {
                    throw new Exception($"Błędna długość wiadomości ({text.Length})");
                }

                var encrypted = cipher.Encrypt(text.Select(c => Convert.ToInt32(c)).ToArray());
                var decrypted = cipher.Decrypt(encrypted);

                Console.WriteLine();
                Console.WriteLine($"Plain text: {text}");

                Console.WriteLine();
                Console.WriteLine($"Encrypted: {string.Concat(encrypted)}");

                Console.WriteLine();
                Console.WriteLine($"Decrypted: {decrypted}");

                File.WriteAllText("Encrypted.txt", string.Concat(encrypted));
                File.WriteAllText("Decrypted.txt", decrypted);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine();
        }
    }
}
