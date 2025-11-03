namespace TestFabric.Test;

public class ArrayEqualityComparerTests
{
    [Fact]
    public void Given_TwoNullArrays_When_Equals_Then_True()
    {
        // Arrange
        var sut = new ArrayEqualityComparer<int>();

        // Act
        var actual = sut.Equals(null, null);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_OneNullArray_When_Equals_Then_False()
    {
        // Arrange
        var first = Array.Empty<int>();
        var sut = new ArrayEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, null);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_TwoEmptyArrays_When_Equals_Then_True()
    {
        // Arrange
        var first = Array.Empty<int>();
        var second = Array.Empty<int>();
        var sut = new ArrayEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_TwoArraysWithDifferentCount_When_Equals_Then_False()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(2).ToArray();
        var second = fix.CreateMany<int>(3).ToArray();
        var sut = new ArrayEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_OneArrayTwice_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToArray();
        var sut = new ArrayEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, first);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_OneArrayTwiceButManipulateItemComparerToFalse_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToArray();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        var sut = new ArrayEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, first);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_TwosArrayTwiceWithItemComparerToFalse_When_Equals_Then_False()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToArray();
        var second = first.ToArray();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        var sut = new ArrayEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_TwosArrayAndDifferentOrderWithItemComparerToTrue_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToArray();
        var second = first.OrderBy(x => x).ToArray();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(true);
        var sut = new ArrayEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_MockedOutputHelperAndArraysOfDifferentLength_When_Equals_Then_OneMessageWritten()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(2).ToArray();
        var second = fix.CreateMany<int>(3).ToArray();
        var mockTestOutputHelper = new Mock<ITestOutputHelper>(MockBehavior.Strict);
        mockTestOutputHelper.Setup(x => x.WriteLine(It.IsAny<string>()));
        var sut = new ArrayEqualityComparer<int>(null, mockTestOutputHelper.Object.WriteLine);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
        mockTestOutputHelper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
    }
}
