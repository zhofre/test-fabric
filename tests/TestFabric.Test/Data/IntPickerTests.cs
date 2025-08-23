namespace TestFabric.Test.Data;

[TestSubject(typeof(IntPicker))]
public class IntPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        const int start = 3;
        const int end = 10;
        var range = new NumberRange<int>(start, end);
        var sut = new IntPicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        actual.Should().BeGreaterThanOrEqualTo(start);
        actual.Should().BeLessThan(end);
    }
}
