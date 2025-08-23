namespace TestFabric.Test.Data;

[TestSubject(typeof(DecimalPicker))]
public class DecimalPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        const decimal start = 3.0m;
        const decimal end = 10.0m;
        var range = new NumberRange<decimal>(start, end);
        var sut = new DecimalPicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        actual.Should().BeGreaterThanOrEqualTo(start);
        actual.Should().BeLessThan(end);
    }
}
