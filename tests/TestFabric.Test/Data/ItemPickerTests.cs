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
        items.Should().Contain(actual);
    }

    [Fact]
    public void Given_EmptyRange_When_Pick_Then_Throws()
    {
        // Arrange
        var range = new ItemRange<int>();
        var sut = new ItemPicker<int>();

        // Act + Assert
        Action act = () => sut.Pick(range);
        act.Should().Throw<ArgumentException>();
    }
}
