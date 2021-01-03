package com.company;

import javax.crypto.Cipher;
import javax.crypto.KeyGenerator;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;
import java.security.spec.KeySpec;
import java.util.Base64;

public class AES
{
    private static byte[] iv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private static IvParameterSpec ivParameterSpec = new IvParameterSpec(iv);

    private static String _plainText = null;

    public AES (String plainText)
    {
        _plainText = plainText;
    }

    private static SecretKeySpec SetKey()
    {
        try
        {
            SecretKeyFactory factory = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA256");
            KeySpec spec = new PBEKeySpec(new char[] {}, _plainText.getBytes(), 65536, 256);
            SecretKey tmp = factory.generateSecret(spec);

            return new SecretKeySpec(tmp.getEncoded(), "AES");
        }
        catch (Exception ex)
        {
            ex.printStackTrace();

            return null;
        }
    }

    public static String Encrypt(String text, String mode)
    {
        try
        {
            Cipher cipher = Cipher.getInstance(mode);

            if (mode.contains("ECB"))
            {
                cipher.init(Cipher.ENCRYPT_MODE, SetKey());
            }
            else
            {
                cipher.init(Cipher.ENCRYPT_MODE, SetKey(), ivParameterSpec);
            }

            return Base64.getEncoder().encodeToString(cipher.doFinal(text.getBytes("UTF-8")));
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas szyfrowania: " + ex.toString());

            return null;
        }
    }

    public static String Decrypt(String encryptedText, String mode)
    {
        try
        {
            Cipher cipher = Cipher.getInstance(mode);

            if (mode.contains("ECB"))
            {
                cipher.init(Cipher.DECRYPT_MODE, SetKey());
            }
            else
            {
                cipher.init(Cipher.DECRYPT_MODE, SetKey(), ivParameterSpec);
            }

            return new String(cipher.doFinal(Base64.getDecoder().decode(encryptedText)));
        }
        catch (Exception ex)
        {
            System.out.println("Błąd podczas deszyfrowania: " + ex.toString());

            return null;
        }
    }
}