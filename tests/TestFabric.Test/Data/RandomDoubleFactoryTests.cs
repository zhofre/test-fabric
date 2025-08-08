using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(RandomDoubleFactory))]
public class RandomDoubleFactoryTests
{
    [Fact]
    public void Given_Nothing_When_Create_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.Create();

        // Assert
        Assert.True(-RandomDoubleFactory.NormalBound < actual);
        Assert.True(actual < RandomDoubleFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmall_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateSmall();

        // Assert
        Assert.True(-RandomDoubleFactory.SmallBound < actual);
        Assert.True(actual < RandomDoubleFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLarge_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateLarge();

        // Assert
        Assert.True(-RandomDoubleFactory.LargeBound < actual);
        Assert.True(actual < RandomDoubleFactory.LargeBound);
    }

    [Fact]
    public void Given_Nothing_When_CreatePositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreatePositive();

        // Assert
        Assert.True(RandomDoubleFactory.SmallBound <= actual);
        Assert.True(actual < RandomDoubleFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateSmallPositive();

        // Assert
        Assert.True(0 <= actual);
        Assert.True(actual < RandomDoubleFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLargePositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateLargePositive();

        // Assert
        Assert.True(RandomDoubleFactory.NormalBound <= actual);
        Assert.True(actual < RandomDoubleFactory.LargeBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateStrictlyPositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateStrictlyPositive();

        // Assert
        Assert.True(RandomDoubleFactory.SmallBound <= actual);
        Assert.True(actual < RandomDoubleFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallStrictlyPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateSmallStrictlyPositive();

        // Assert
        Assert.True(RandomDoubleFactory.SmallestBound <= actual);
        Assert.True(actual < RandomDoubleFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLargeStrictlyPositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = sut.CreateLargeStrictlyPositive();

        // Assert
        Assert.True(RandomDoubleFactory.NormalBound <= actual);
        Assert.True(actual < RandomDoubleFactory.LargeBound);
    }
}
