using FluentAssertions;
using Nop.Plugin.Payments.SimplePay.Messages.Formatters;

namespace Nop.Plugin.Payments.SimplePay.Unit.Test.Messages.Formatters;
public class DateTimeISO8601Tests
{
    private DateTimeISO8601 _sut;

    public DateTimeISO8601Tests()
    {
        _sut = new DateTimeISO8601();
    }

    [Fact]
    public void ConvertDateTimeToISO8601String()
    {
        var dateTime = new DateTime(2018, 9, 15, 11, 25, 37);
        var result = _sut.ToString(dateTime);
        result.Should().Be("2018-09-15T11:25:37+02:00");
    }

    [Fact]
    public void ConvertISO8601StringToDateTime()
    {
        var expected = new DateTime(2018, 9, 15, 11, 25, 37);
        var result = _sut.FromString("2018-09-15T11:25:37+02:00");
        result.Should().Be(expected);
    }
}
