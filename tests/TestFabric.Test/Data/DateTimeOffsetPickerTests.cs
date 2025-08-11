namespace TestFabric.Test.Data;

[TestSubject(typeof(DateTimeOffsetPicker))]
public class DateTimeOffsetPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        var start = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var range = new NumberRange<DateTimeOffset>(start, end);
        var sut = new DateTimeOffsetPicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        Assert.True(actual >= start && actual < end);
    }
}
