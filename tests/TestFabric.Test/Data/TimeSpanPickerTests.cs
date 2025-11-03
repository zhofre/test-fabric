namespace TestFabric.Test.Data;

[TestSubject(typeof(TimeSpanPicker))]
public class TimeSpanPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        var start = TimeSpan.FromHours(1);
        var end = TimeSpan.FromHours(5);
        var picker = new TimeSpanPicker();

        // Act
        var result = picker.Pick(new NumberRange<TimeSpan>(start, end));

        // Assert
        result.Should().BeGreaterThanOrEqualTo(start);
        result.Should().BeLessThan(end);
    }

    [Fact]
    public void Given_ZeroLengthRange_When_Pick_Then_ReturnsStart()
    {
        // Arrange
        var ts = TimeSpan.FromMinutes(30);
        var picker = new TimeSpanPicker();

        // Act
        var result = picker.Pick(new NumberRange<TimeSpan>(ts, ts));

        // Assert
        result.Should().Be(ts);
    }
}
