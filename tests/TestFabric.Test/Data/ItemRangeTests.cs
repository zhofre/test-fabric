namespace TestFabric.Test.Data;

[TestSubject(typeof(ItemRange<>))]
public class ItemRangeTests
{
    [Fact]
    public void Given_Items_When_CreateItemRange_Then_ItemsSetCorrectly()
    {
        // Arrange
        var items = new[] { 1, 2, 3 };

        // Act
        var range = new ItemRange<int>(items);

        // Assert
        Assert.NotNull(range.Items);
        Assert.IsType<int[]>(range.Items);
        Assert.Equal(items, range.Items);
        Assert.NotSame(items, range.Items); // Ensure a copy was made
    }
}
