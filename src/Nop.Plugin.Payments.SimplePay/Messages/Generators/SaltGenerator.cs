using System.Security.Cryptography;
using System.Text;

namespace Nop.Plugin.Payments.SimplePay.Messages.Generators;
public class SaltGenerator : ISaltGenerator
{
    public string Generate(int length = 32)
    {
        var salt = GetSalt(length);
        using var md5 = MD5.Create();
        var saltBytes = Encoding.UTF8.GetBytes(salt.ToString());
        var hashBytes = md5.ComputeHash(saltBytes);
        return Convert.ToHexString(hashBytes);
    }

    private static StringBuilder GetSalt(int length)
    {
        var salt = new StringBuilder();
        var random = new Random();
        for (var i = 0; i < length; i++)
        {
            var c = (char)random.Next(33, 126);
            salt.Append(c);
        }

        return salt;
    }
}
