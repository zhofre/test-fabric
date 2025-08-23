namespace TestFabric.Test.Data;

[TestSubject(typeof(RandomDoubleFactory))]
public class RandomDoubleFactoryTests(ITestOutputHelper outputHelper)
{
    private const int TestCount = 20; // only increase in case of development testing

    [Fact]
    public void Given_Nothing_When_Create_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.Create();
            outputHelper.WriteLine(
                $"{-RandomDoubleFactory.MediumBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomDoubleFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmall_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmall();
            outputHelper.WriteLine(
                $"{-RandomDoubleFactory.SmallBound} <= {actual[i]} <= {RandomDoubleFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomDoubleFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMedium_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMedium();
            outputHelper.WriteLine(
                $"{-RandomDoubleFactory.MediumBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomDoubleFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
            Math.Abs(item).Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLarge_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLarge();
            outputHelper.WriteLine(
                $"{-RandomDoubleFactory.LargeBound} <= {actual[i]} <= {RandomDoubleFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomDoubleFactory.LargeBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.LargeBound);
            Math.Abs(item).Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreatePositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreatePositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.SmallBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmallPositive();
            outputHelper.WriteLine($"0 <= {actual[i]} <= {RandomDoubleFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(0);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMediumPositive_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMediumPositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.SmallBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLargePositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLargePositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.MediumBound} <= {actual[i]} <= {RandomDoubleFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.LargeBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateStrictlyPositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateStrictlyPositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.SmallBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallStrictlyPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmallStrictlyPositive();
            outputHelper.WriteLine($"{RandomDoubleFactory.Epsilon} <= {actual[i]} <= {RandomDoubleFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.Epsilon);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMediumStrictlyPositive_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMediumStrictlyPositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.SmallBound} <= {actual[i]} <= {RandomDoubleFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLargeStrictlyPositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomDoubleFactory();

        // Act
        var actual = new double[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLargeStrictlyPositive();
            outputHelper.WriteLine(
                $"{RandomDoubleFactory.MediumBound} <= {actual[i]} <= {RandomDoubleFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomDoubleFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomDoubleFactory.LargeBound);
        }
    }
}
