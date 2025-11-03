namespace TestFabric.Test;

public class ListEqualityComparerTests
{
    [Fact]
    public void Given_TwoNullLists_When_Equals_Then_True()
    {
        // Arrange
        var sut = new ListEqualityComparer<int>();

        // Act
        var actual = sut.Equals(null, null);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_OneNullList_When_Equals_Then_False()
    {
        // Arrange
        var first = new List<int>();
        var sut = new ListEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, null);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_TwoEmptyLists_When_Equals_Then_True()
    {
        // Arrange
        var first = new List<int>();
        var second = new List<int>();
        var sut = new ListEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_TwoListsWithDifferentCount_When_Equals_Then_False()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(2).ToList();
        var second = fix.CreateMany<int>(3).ToList();
        var sut = new ListEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_OneListTwice_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToList();
        var sut = new ListEqualityComparer<int>();

        // Act
        var actual = sut.Equals(first, first);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_OneListTwiceButManipulateItemComparerToFalse_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToList();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        var sut = new ListEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, first);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_TwosListTwiceWithItemComparerToFalse_When_Equals_Then_False()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToList();
        var second = first.ToList();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(false);
        var sut = new ListEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_TwosListAndDifferentOrderWithItemComparerToTrue_When_Equals_Then_True()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(7).ToList();
        var second = first.OrderBy(x => x).ToList();
        var mockComp = new Mock<IEqualityComparer<int>>(MockBehavior.Strict);
        mockComp
            .Setup(x => x.Equals(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(true);
        var sut = new ListEqualityComparer<int>(mockComp.Object);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_MockedOutputHelperAndListsOfDifferentLength_When_Equals_Then_OneMessageWritten()
    {
        // Arrange
        var fix = new Fixture();
        var first = fix.CreateMany<int>(2).ToList();
        var second = fix.CreateMany<int>(3).ToList();
        var mockTestOutputHelper = new Mock<ITestOutputHelper>(MockBehavior.Strict);
        mockTestOutputHelper.Setup(x => x.WriteLine(It.IsAny<string>()));
        var sut = new ListEqualityComparer<int>(null, mockTestOutputHelper.Object.WriteLine);

        // Act
        var actual = sut.Equals(first, second);

        // Assert
        actual.Should().BeFalse();
        mockTestOutputHelper.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once);
    }
}
