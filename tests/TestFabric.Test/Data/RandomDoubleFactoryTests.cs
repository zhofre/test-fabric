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
            Assert.True(-RandomDoubleFactory.MediumBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
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
            Assert.True(-RandomDoubleFactory.SmallBound <= item);
            Assert.True(item <= RandomDoubleFactory.SmallBound);
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
            Assert.True(-RandomDoubleFactory.MediumBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
            Assert.True(Math.Abs(item) >= RandomDoubleFactory.SmallBound);
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
            Assert.True(-RandomDoubleFactory.LargeBound <= item);
            Assert.True(item <= RandomDoubleFactory.LargeBound);
            Assert.True(Math.Abs(item) >= RandomDoubleFactory.MediumBound);
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
            Assert.True(RandomDoubleFactory.SmallBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
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
            Assert.True(0 <= item);
            Assert.True(item <= RandomDoubleFactory.SmallBound);
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
            Assert.True(RandomDoubleFactory.SmallBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
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
            Assert.True(RandomDoubleFactory.MediumBound <= item);
            Assert.True(item <= RandomDoubleFactory.LargeBound);
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
            Assert.True(RandomDoubleFactory.SmallBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
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
            Assert.True(RandomDoubleFactory.Epsilon <= item);
            Assert.True(item <= RandomDoubleFactory.SmallBound);
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
            Assert.True(RandomDoubleFactory.SmallBound <= item);
            Assert.True(item <= RandomDoubleFactory.MediumBound);
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
            Assert.True(RandomDoubleFactory.MediumBound <= item);
            Assert.True(item <= RandomDoubleFactory.LargeBound);
        }
    }
}
