package com.company;

import java.nio.file.Files;
import java.nio.file.Path;

public class Main
{
    public static void main(String[] args)
    {
        SmallFile();
        MediumFile();
        LargeFile();

        var damageFile = new DamagedCiphertext();
        damageFile.TryDecrypt();
    }

    private static void SmallFile()
    {
        System.out.println("\nMały plik");

        String plainText = GetText("src/small.txt");

        InitializeTesting(plainText == null? null: plainText, "src/encryptedSmallOFB.txt",
                "src/decryptedSmallOFB.txt");
    }

    private static void MediumFile()
    {
        System.out.println("\nŚredni plik");

        String plainText = GetText("src/medium.txt");

        InitializeTesting(plainText == null? null: plainText, "src/encryptedMediumOFB.txt",
                "src/decryptedMediumOFB.txt");
    }

    private static void LargeFile()
    {
        System.out.println("\nDuży plik");

        String plainText = GetText("src/large.txt");

        InitializeTesting(plainText == null? null: plainText, "src/encryptedLargeOFB.txt",
                "src/decryptedLargeOFB.txt");
    }

    private static void InitializeTesting(String plainText, String encryptedPath, String decryptedPath)
    {
        if (plainText != null)
        {
            var aes = new AES(plainText);

            TestCipheringAlgorithms(aes, "AES/ECB/PKCS5Padding", plainText, encryptedPath, decryptedPath);
            TestCipheringAlgorithms(aes, "AES/CBC/PKCS5Padding", plainText, encryptedPath, decryptedPath);
            TestCipheringAlgorithms(aes, "AES/OFB/PKCS5Padding", plainText, encryptedPath, decryptedPath);
            TestCipheringAlgorithms(aes, "AES/CFB/PKCS5Padding", plainText, encryptedPath, decryptedPath);
            TestCipheringAlgorithms(aes, "AES/CTR/NoPadding", plainText, encryptedPath, decryptedPath);
        }
    }

    private static void WriteToFile(String encryptedPath, String text, AES aes, String decryptedPath)
    {
        try
        {
            Files.writeString(Path.of(encryptedPath), text);

            ReadFromFileAndDecrypt(encryptedPath, decryptedPath, aes);
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas zapisu danych do pliku");
        }
    }

    private static void ReadFromFileAndDecrypt(String encryptedPath, String decryptedPath, AES aes)
    {
        try
        {
            String encryptedString = Files.readString(Path.of(encryptedPath));

            String decryptedString = aes.Decrypt(encryptedString, "AES/OFB/PKCS5Padding");

            Files.writeString(Path.of(decryptedPath), decryptedString);
        }
        catch (Exception ex)
        {
            System.out.println("Wystąpił błąd podczas odczytywania z pliku i szyfrowania");
        }
    }

    private static String GetText(String path)
    {
        try
        {
            return Files.readString(Path.of(path));
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas odczytywania pliku");

            return null;
        }
    }

    private static void TestCipheringAlgorithms(AES aes, String aesMode, String plainText,
                                                String encryptedPath, String decryptedPath)
    {
        System.out.print('\n' + aesMode.substring(4, 7) + ": ");

        System.out.printf("\n\tEncrypting: ");

        var startTime = System.nanoTime();

        String encryptedString = aes.Encrypt(plainText, aesMode).toString();

        System.out.println((double)(System.nanoTime() - startTime) / 1000000000);

        System.out.printf("\tDecrypting: ");

        startTime = System.nanoTime();

        aes.Decrypt(encryptedString, aesMode);

        System.out.println((double)(System.nanoTime() - startTime) / 1000000000);

        if (aesMode.contains("OFB"))
        {
            WriteToFile(encryptedPath, encryptedString, aes, decryptedPath);
        }
    }
}