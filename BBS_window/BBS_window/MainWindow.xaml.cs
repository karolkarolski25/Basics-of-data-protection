using BBS_Tests;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BBS_window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Random random = new Random();

        private static string bbs = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            LengthTextBox.Focus();
        }

        static ulong NWD(ulong a, ulong b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;
        }

        static bool IsPrime(ulong n)
        {
            if (n < 2)
            {
                return false;
            }

            for (ulong i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static ulong GeneratePrimeNumber(ulong n)
        {
            if (IsPrime(n) && n % 4 == 3)
            {
                return n;
            }

            while (!(n % 4 == 3 && IsPrime(n)))
            {
                n++;
            }

            return n;
        }

        static ulong GenerateRelativelyPrimeNumber(ulong N)
        {
            ulong x = (ulong)random.Next(10_000_000, 15_000_000);

            while (NWD(x, N) != 1)
            {
                x = (ulong)random.Next(10_000_000, 15_000_000);
            }

            return x;
        }

        static string BBSGenerator(int n)
        {
            ulong p = GeneratePrimeNumber((ulong)random.Next(10_000_000, 15_000_000));
            ulong q = GeneratePrimeNumber((ulong)random.Next(10_000_000, 15_000_000));

            ulong N = p * q;
            ulong x = GenerateRelativelyPrimeNumber(N);


            ulong x0 = (ulong)(Math.Pow(x, 2) % N);

            StringBuilder stringBuilder = new StringBuilder((x0 & 1).ToString());

            for (int i = 0; i < n; i++)
            {
                ulong xi = (ulong)(Math.Pow(x0, 2) % N);

                stringBuilder.Append((xi & 1).ToString());

                x0 = xi;
            }

            return stringBuilder.ToString();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(LengthTextBox.Text);

                bbs = BBSGenerator(n);

                bbsTextBox.Text = bbs;

                var pokerTest = PokerTest.IsPokerTestPassed(bbs);
                var singleBitTest = SingleBitTest.IsSingleBitTestPassed(bbs);
                var longSeriesTest = LongSeriesTest.IsLongSeriesTestPassed(bbs);
                var seriesTest = SeriesTest.IsSeriesTestPassed(bbs);

                PoketTestLabel.Content = $"Test pokerowy:\t\t{(pokerTest.result ? "Zaliczony" : "Niezaliczony")}";
                LongSeriesLabel.Content = $"Test długiej serii:\t\t{(longSeriesTest.result ? "Zaliczony" : "Niezaliczony")}";
                SeriesLabel.Content = $"Test serii:\t\t\t{(seriesTest.result ? "Zaliczony" : "Niezaliczony")}";
                SingleBitTestLabel.Content = $"Test pojedynczych bitów:\t{(singleBitTest.result ? "Zaliczony" : "Niezaliczony")}";

                PokerTestResultLabel.Content = $"Test pokerowy - X: {Math.Round(pokerTest.value, 2)}\t\t(2.16 - 46.17)";
                SingleBitTestResultLabel.Content = $"Test pojedynczych bitów - {singleBitTest.value}\t(9725 - 10275)";
                LongSeriesTestResultLabel.Content = $"Test długiej serii - {longSeriesTest.value}";

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append($"1 - {seriesTest.value[1]} \t\t\t<2315 - 2685>\n");
                stringBuilder.Append($"2 - {seriesTest.value[2]} \t\t\t<1114 - 1386>\n");
                stringBuilder.Append($"3 - {seriesTest.value[3]} \t\t\t<527 - 723>\n");
                stringBuilder.Append($"4 - {seriesTest.value[4]} \t\t\t<240 - 384>\n");
                stringBuilder.Append($"5 - {seriesTest.value[5]} \t\t\t<103 - 209>\n");
                stringBuilder.Append($"6 lub więcej - {seriesTest.value[6]} \t<103 - 209>\n");

                SeriesTestResultTextBlock.Text = stringBuilder.ToString();
            }
            catch
            {
                MessageBox.Show("Podano złą wartość", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = "BBS",
                    DefaultExt = ".text",
                    Filter = "Text documents (.txt)|*.txt"
                };

                if (dlg.ShowDialog() == true)
                {
                    string filePath = dlg.FileName;

                    using var writer = new StreamWriter(filePath);

                    writer.Write(bbs);

                    MessageBox.Show($"Plik został pomyślnie zapisany\n{filePath}", "Zapisano",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisywania pliku\n{ex.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
