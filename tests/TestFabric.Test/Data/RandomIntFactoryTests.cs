using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(RandomIntFactory))]
public class RandomIntFactoryTests
{
    [Fact]
    public void Given_Nothing_When_Create_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.Create();

        // Assert
        Assert.True(-RandomIntFactory.NormalBound < actual);
        Assert.True(actual < RandomIntFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmall_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateSmall();

        // Assert
        Assert.True(-RandomIntFactory.SmallBound < actual);
        Assert.True(actual < RandomIntFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLarge_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateLarge();

        // Assert
        Assert.True(-RandomIntFactory.LargeBound < actual);
        Assert.True(actual < RandomIntFactory.LargeBound);
    }

    [Fact]
    public void Given_Nothing_When_CreatePositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreatePositive();

        // Assert
        Assert.True(RandomIntFactory.SmallBound <= actual);
        Assert.True(actual < RandomIntFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateSmallPositive();

        // Assert
        Assert.True(0 <= actual);
        Assert.True(actual < RandomIntFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLargePositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateLargePositive();

        // Assert
        Assert.True(RandomIntFactory.NormalBound <= actual);
        Assert.True(actual < RandomIntFactory.LargeBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateStrictlyPositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateStrictlyPositive();

        // Assert
        Assert.True(RandomIntFactory.SmallBound <= actual);
        Assert.True(actual < RandomIntFactory.NormalBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallStrictlyPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateSmallStrictlyPositive();

        // Assert
        Assert.True(RandomIntFactory.SmallestBound <= actual);
        Assert.True(actual < RandomIntFactory.SmallBound);
    }

    [Fact]
    public void Given_Nothing_When_CreateLargeStrictlyPositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.CreateLargeStrictlyPositive();

        // Assert
        Assert.True(RandomIntFactory.NormalBound <= actual);
        Assert.True(actual < RandomIntFactory.LargeBound);
    }
}
