using System.Security.Cryptography;
using System.Text;

namespace Nop.Plugin.Payments.SimplePay.Messages.Validators;
public class MessageToSendValidator : IMessageToSendValidator
{
    public string CalculateSignature(string merchantKey, string message)
    {
        using var hmac = new HMACSHA384(Encoding.UTF8.GetBytes(merchantKey));
        byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return Convert.ToBase64String(hashValue);
    }
}
