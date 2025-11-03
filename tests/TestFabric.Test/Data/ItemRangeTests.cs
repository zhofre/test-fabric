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
        range.Items.Should().NotBeNull();
        range.Items.Should().BeOfType<int[]>();
        range.Items.Should().Equal(items);
        range.Items.Should().NotBeSameAs(items); // Ensure a copy was made
    }
}
