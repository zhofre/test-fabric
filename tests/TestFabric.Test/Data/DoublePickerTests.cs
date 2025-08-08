using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(DoublePicker))]
public class DoublePickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        const double start = 3.0;
        const double end = 10.0;
        var range = new NumberRange<double>(start, end);
        var sut = new DoublePicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        Assert.True(actual is >= start and < end);
    }
}
