using System;
using System.Text;
using System.Security.Cryptography;

public static class SHA
{
    public static byte[] sha256(string inputString)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha256.ComputeHash(bytes);
        return hash;
    }

    public static string sha512(string inputString)
    {
        SHA512 sha512 = SHA512.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha512.ComputeHash(bytes);
        return GetStringFromHash(hash);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }
}
