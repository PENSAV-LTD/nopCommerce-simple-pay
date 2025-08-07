using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class ErrorResponse
{
    [JsonPropertyName("errorCodes")]
    public List<int> ErrorCodes { get; set; }
}
