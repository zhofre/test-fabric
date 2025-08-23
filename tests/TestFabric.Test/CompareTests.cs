namespace TestFabric.Test;

public class CompareTests
{
    [Fact]
    public void Given_Nothing_When_CompareDoubleAbsolute_Then_AbsoluteDoubleComparer()
    {
        // Arrange

        // Act
        var actual = Compare.DoubleAbsolute(1e-30);

        // Assert
        actual.Should().BeOfType<AbsoluteDoubleComparer>();
    }

    [Fact]
    public void Given_Nothing_When_CompareDoubleRelative_Then_RelativeDoubleComparer()
    {
        // Arrange

        // Act
        var actual = Compare.DoubleRelative(1e-30, 1e-30);

        // Assert
        actual.Should().BeOfType<RelativeDoubleComparer>();
    }

    [Fact]
    public void Given_Nothing_When_CompareArrayInt_Then_ArrayComparer()
    {
        // Arrange

        // Act
        var actual = Compare.Array<int>();

        // Assert
        actual.Should().BeOfType<ArrayEqualityComparer<int>>();
    }

    [Fact]
    public void Given_Nothing_When_CompareArrayString_Then_ArrayComparer()
    {
        // Arrange

        // Act
        var actual = Compare.Array<string>();

        // Assert
        actual.Should().BeOfType<ArrayEqualityComparer<string>>();
    }

    [Fact]
    public void Given_Nothing_When_CompareListInt_Then_ListComparer()
    {
        // Arrange

        // Act
        var actual = Compare.List<int>();

        // Assert
        actual.Should().BeOfType<ListEqualityComparer<int>>();
    }
}
