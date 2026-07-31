namespace Nop.Plugin.Payments.SimplePay.Messages.Validators;
public interface IMessageToSendValidator
{
    public string CalculateSignature(string merchantKey, string message);
}
