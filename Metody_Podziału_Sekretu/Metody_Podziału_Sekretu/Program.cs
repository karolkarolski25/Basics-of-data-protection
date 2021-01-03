using System;

namespace Metody_Podziału_Sekretu
{
    class Program
    {
        private static void TrivialMethod()
        {
            int k, n, s;

            Console.WriteLine("Trivial method");

            Console.Write("Podaj k: ");
            k = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj n: ");
            n = Convert.ToInt32(Console.ReadLine());

            TrivialMethod trivialMethod = new TrivialMethod(n, k);

            var dividedSecret = trivialMethod.DivideSecret();

            var recoveredSecret = trivialMethod.RecoverSecret(dividedSecret);

            Console.WriteLine("Divided secret");

            foreach (var item in dividedSecret)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"Recovered secret: {recoveredSecret}");
        }

        private static void ShamirSheme()
        {
            Console.WriteLine("Sharmir scheme:");

            Console.Write("p: ");

            int p = Convert.ToInt32(Console.ReadLine());

            Console.Write("t: ");

            int t = Convert.ToInt32(Console.ReadLine());

            Console.Write("n: ");

            int n = Convert.ToInt32(Console.ReadLine());

            Console.Write("s: ");
            int s = Convert.ToInt32(Console.ReadLine());

            ShamirScheme shamirScheme = new ShamirScheme(s, t, n, p);

            Console.WriteLine("Divided secret");

            var dividedShamirSecret = shamirScheme.DivideSecret();

            for (int i = 0; i < dividedShamirSecret.Count; i++)
            {
                Console.WriteLine($"({i + 1}, {dividedShamirSecret[i]})");
            }

            Console.WriteLine($"Recovered secret: {shamirScheme.RecoverSecret(dividedShamirSecret)}");
        }

        static void Main(string[] args)
        {
            TrivialMethod();
            Console.WriteLine();
            ShamirSheme();
        }
    }
}
