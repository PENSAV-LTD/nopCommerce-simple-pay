using System.Globalization;

namespace Nop.Plugin.Payments.SimplePay.Messages.Formatters;
public class DateTimeISO8601
{
    private const string DATE_TIME_FORMAT = "yyyy-MM-ddTHH:mm:sszzz";

    public string ToString(DateTime dateTime)
    {
        return dateTime.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
    }

    public DateTime FromString(string dateTimeStr)
    {
        return DateTime.ParseExact(dateTimeStr, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }
}
