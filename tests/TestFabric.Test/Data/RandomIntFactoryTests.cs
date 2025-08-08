using JetBrains.Annotations;
using TestFabric.Data;
using Xunit.Abstractions;

namespace TestFabric.Test.Data;

[TestSubject(typeof(RandomIntFactory))]
public class RandomIntFactoryTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Given_Nothing_When_Create_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = sut.Create();
        outputHelper.WriteLine($"{-RandomIntFactory.NormalBound} <= {actual} < {RandomIntFactory.NormalBound}");

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
        outputHelper.WriteLine($"{-RandomIntFactory.SmallBound} < {actual} < {RandomIntFactory.SmallBound}");

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
        outputHelper.WriteLine($"{-RandomIntFactory.LargeBound} < {actual} < {RandomIntFactory.LargeBound}");

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
        outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual} < {RandomIntFactory.NormalBound}");

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
        outputHelper.WriteLine($"0 <= {actual} < {RandomIntFactory.SmallBound}");

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
        outputHelper.WriteLine($"{RandomIntFactory.NormalBound} <= {actual} < {RandomIntFactory.LargeBound}");

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
        outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual} < {RandomIntFactory.NormalBound}");

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
        outputHelper.WriteLine($"{RandomIntFactory.SmallestBound} <= {actual} < {RandomIntFactory.SmallBound}");

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
        outputHelper.WriteLine($"{RandomIntFactory.NormalBound} <= {actual} < {RandomIntFactory.LargeBound}");

        // Assert
        Assert.True(RandomIntFactory.NormalBound <= actual);
        Assert.True(actual < RandomIntFactory.LargeBound);
    }
}
