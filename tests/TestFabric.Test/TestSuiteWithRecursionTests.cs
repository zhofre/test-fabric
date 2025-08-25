namespace TestFabric.Test;

[TestSubject(typeof(TestSuite.WithRecursion))]
public class TestSuiteWithRecursionTests : TestSuite.WithRecursion
{
    [Fact]
    public void Given_Nothing_When_CreateRecursionDummy_Then_Created()
    {
        // Arrange
        // Act
        var actual = Factory.Create<RecursionDummy>();

        // Assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void Given_RecursionDummies_When_UpdateRecursiveItem_Then_Updated()
    {
        // Arrange
        var dummy1 = Factory.Create<RecursionDummy>();
        var dummy2 = Factory.Create<RecursionDummy>();

        // Act
        var original = dummy2.RecursiveItem;
        dummy2.RecursiveItem = dummy1;
        var updated = dummy2.RecursiveItem;

        // Assert
        dummy2.RecursiveItem.Should().BeSameAs(dummy1);
        updated.Should().NotBeSameAs(original);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class RecursionDummy
    {
        // ReSharper disable once UnusedMember.Local
        public RecursionDummy? RecursiveItem { get; set; }
    }
}
