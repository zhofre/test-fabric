namespace TestFabric.Test.Data;

[TestSubject(typeof(FloatPicker))]
public class FloatPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        const float start = 3.0f;
        const float end = 10.0f;
        var range = new NumberRange<float>(start, end);
        var sut = new FloatPicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        actual.Should().BeGreaterThanOrEqualTo(start);
        actual.Should().BeLessThan(end);
    }
}
