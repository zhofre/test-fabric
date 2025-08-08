using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(ItemPicker<>))]
public class ItemPickerTests
{
    [Fact]
    public void Given_Items_When_Pick_Then_Success()
    {
        // Arrange
        var items = new[] { 1, 2, 3 };
        var range = new ItemRange<int>(items);
        var sut = new ItemPicker<int>();

        // Act
        var actual = sut.Pick(range);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_EmptyRange_When_Pick_Then_Throws()
    {
        // Arrange
        var range = new ItemRange<int>();
        var sut = new ItemPicker<int>();

        // Act
        var ex = Assert.Throws<ArgumentException>(() => sut.Pick(range));
    }
}
