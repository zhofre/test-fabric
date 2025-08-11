namespace TestFabric.Test.Data;

[TestSubject(typeof(DateTimePicker))]
public class DateTimePickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        var start = new DateTime(2023, 1, 1);
        var end = new DateTime(2024, 1, 1);
        var range = new NumberRange<DateTime>(start, end);
        var sut = new DateTimePicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        Assert.True(actual >= start && actual < end);
    }
}
