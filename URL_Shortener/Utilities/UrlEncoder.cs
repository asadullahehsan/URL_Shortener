using System.Text;
using System.Numerics;
using System.Security.Cryptography;

namespace URL_Shortener.Utilities;

public static class UrlEncoder
{
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string GenerateShortCode(string input, int length = 6)
    {
        var hash = GenerateHash(input);
        var bytes = Enumerable.Range(0, 16)
            .Where(i => i % 2 == 0)
            .Select(i => Convert.ToByte(hash.Substring(i, 2), 16))
            .ToArray();

        return ToBase62(bytes).Substring(0, length);
    }

    private static string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }

    private static string ToBase62(byte[] input)
    {
        BigInteger bigInt = new BigInteger(input.Concat(new byte[] { 0 }).ToArray());
        var sb = new StringBuilder();

        while (bigInt > 0)
        {
            bigInt = BigInteger.DivRem(bigInt, 62, out var remainder);
            sb.Insert(0, Base62Chars[(int)remainder]);
        }

        return sb.ToString();
    }

}
