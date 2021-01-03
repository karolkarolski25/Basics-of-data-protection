package com.company;

import jdk.swing.interop.SwingInterOpUtils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class Main {

    private static void PerformHashingAlgorithm(String input, String algorithm)
    {
        long startTime = System.nanoTime();

        var hashedString = HashText(input, algorithm);

        System.out.println(algorithm + ": " + hashedString + " (" + hashedString.length() + ") - "
                + (double)(System.nanoTime() - startTime) / 1000000 + " ms");

    }

    private static String HashText(String input, String algorithm)
    {
        try
        {
            MessageDigest messageDigest = MessageDigest.getInstance(algorithm);

            var bytes = messageDigest.digest(input.getBytes(StandardCharsets.UTF_8));

            BigInteger number = new BigInteger(1, bytes);

            System.out.println(number);

            return number.toString(16);
        }
        catch (Exception ex)
        {
            return "Error with " + algorithm + " algorithm: " + ex.toString();
        }
    }

    private static void SmallFile()
    {
        System.out.println("\nMały plik");

        String plainText = GetText("src/small.txt");

        InitializeTesting(plainText == null? null: plainText);
    }

    private static void MediumFile()
    {
        System.out.println("\nŚredni plik");

        String plainText = GetText("src/medium.txt");

        InitializeTesting(plainText == null? null: plainText);
    }

    private static void LargeFile()
    {
        System.out.println("\nDuży plik");

        String plainText = GetText("src/large.txt");

        InitializeTesting(plainText == null? null: plainText);
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

    private static void zad2()
    {
        SmallFile();
        MediumFile();
        LargeFile();
    }

    private static void InitializeTesting(String text)
    {
        System.out.println();

        PerformHashingAlgorithm(text, "MD5");

        System.out.println();

        PerformHashingAlgorithm(text, "SHA-1");

        System.out.println("\nSHA2\n");

        PerformHashingAlgorithm(text, "SHA-224");
        PerformHashingAlgorithm(text, "SHA-256");
        PerformHashingAlgorithm(text, "SHA-384");
        PerformHashingAlgorithm(text, "SHA-512");
        PerformHashingAlgorithm(text, "SHA-512/224");
        PerformHashingAlgorithm(text, "SHA-512/256");

        System.out.println("\nSHA3\n");

        PerformHashingAlgorithm(text, "SHA3-224");
        PerformHashingAlgorithm(text, "SHA3-256");
        PerformHashingAlgorithm(text, "SHA3-384");
        PerformHashingAlgorithm(text, "SHA3-512");
    }

    public static void main(String[] args) throws IOException, NoSuchAlgorithmException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        System.out.printf("Podaj tekst do hashowania: ");

//        String text = reader.readLine();

        String text = "Kot";

        System.out.println("Zadanie 1: ");

        InitializeTesting(text);

        System.out.println("Zadanie 2: ");

        zad2();
    }
}
