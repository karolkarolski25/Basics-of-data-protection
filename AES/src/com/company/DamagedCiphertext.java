package com.company;

import java.nio.file.Files;
import java.nio.file.Path;
import java.util.concurrent.ThreadLocalRandom;

public class DamagedCiphertext
{
    public static void TryDecrypt()
    {
        String plainText = ReadFromFile("src/small.txt");

        AES aes = new AES(plainText);

        String encrypted = aes.Encrypt(plainText, "AES/OFB/PKCS5Padding").toString();

        String damagedEncryptedSmall = DamageFile(encrypted);

        WriteToFile("src/damagedEncryptedSmallOFB.txt", damagedEncryptedSmall);

        String damagedDecryptedText = aes.Decrypt(damagedEncryptedSmall, "AES/OFB/PKCS5Padding");
        String decryptedText = aes.Decrypt(encrypted, "AES/OFB/PKCS5Padding");

        System.out.println("\n\n\nOryginał: ");

        for (int j = 0; j < 10000 ; j++)
        {
            System.out.print(decryptedText.charAt(j));
        }

        System.out.println("\nUszkodzony: ");

        for (int j = 0; j < 10000; j++)
        {
            System.out.print(damagedDecryptedText.charAt(j));
        }

        WriteToFile("src/damagedDecryptedSmallOFB.txt", damagedDecryptedText);
    }

    public static String DamageFile(String textToDamage) //65 - 122
    {
        StringBuilder text = new StringBuilder(textToDamage);

        for (int i = 0; i < 10000; i++)
        {
            char randomAlphabeticChar = (char)ThreadLocalRandom.current().nextInt(97, 123);

            text.setCharAt(i, randomAlphabeticChar);
        }

        return text.toString();
    }

    private static void WriteToFile(String path, String text)
    {
        try
        {
            Files.writeString(Path.of(path), text);
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas zapisu do pliku");
        }
    }

    private static String ReadFromFile(String path)
    {
        try
        {
            return Files.readString(Path.of(path));
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas odczytu z pliku");

            return null;
        }
    }
}
