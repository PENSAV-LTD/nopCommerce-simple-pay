using System.ComponentModel;

namespace Nop.Plugin.Payments.SimplePay.Models;
public enum PaymentMethods
{
    [Description("CARD")]
    Card = 1,
    [Description("WIRE")]
    Wire = 2,
}

public static class PaymentMethodsExtensions
{
    public static string GetDescription(this PaymentMethods method)
    {
        var fieldInfo = method.GetType().GetField(method.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : method.ToString();
    }
}