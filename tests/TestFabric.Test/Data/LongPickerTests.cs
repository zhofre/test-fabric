using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(LongPicker))]
public class LongPickerTests
{
    [Fact]
    public void Given_Range_When_Pick_Then_Success()
    {
        // Arrange
        const long start = 3;
        const long end = 10;
        var range = new NumberRange<long>(start, end);
        var sut = new LongPicker();

        // Act
        var actual = sut.Pick(range);

        // Assert
        Assert.True(actual is >= start and < end);
    }
}
