namespace Nop.Plugin.Payments.SimplePay.Messages.Generators;
public interface ISaltGenerator
{
    public string Generate(int length = 32);
}
