namespace TestFabric.Test.Data;

[TestSubject(typeof(NumberRange<>))]
public class NumberRangeTests
{
    [Fact]
    public void Given_StartAndEndValues_When_CreateNumberRange_Then_StartAndEndSetCorrectly()
    {
        // Arrange
        const int expectedStart = 10;
        const int expectedEnd = 20;

        // Act
        var range = new NumberRange<int>(expectedStart, expectedEnd);

        // Assert
        range.Start.Should().Be(expectedStart);
        range.End.Should().Be(expectedEnd);
    }
}
