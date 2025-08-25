namespace TestFabric.Test.Data;

[TestSubject(typeof(RandomIntFactory))]
public class RandomIntFactoryTests(ITestOutputHelper outputHelper)
{
    private const int TestCount = 20; // only increase in case of development testing

    [Fact]
    public void Given_Nothing_When_Create_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.Create();
            outputHelper.WriteLine($"{-RandomIntFactory.MediumBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomIntFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmall_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmall();
            outputHelper.WriteLine($"{-RandomIntFactory.SmallBound} <= {actual[i]} <= {RandomIntFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomIntFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMedium_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMedium();
            outputHelper.WriteLine($"{-RandomIntFactory.MediumBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomIntFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
            Math.Abs(item).Should().BeGreaterThanOrEqualTo(RandomIntFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLarge_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLarge();
            outputHelper.WriteLine($"{-RandomIntFactory.LargeBound} <= {actual[i]} <= {RandomIntFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(-RandomIntFactory.LargeBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.LargeBound);
            Math.Abs(item).Should().BeGreaterThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreatePositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreatePositive();
            outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmallPositive();
            outputHelper.WriteLine($"0 <= {actual[i]} <= {RandomIntFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(0);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMediumPositive_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMediumPositive();
            outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLargePositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLargePositive();
            outputHelper.WriteLine($"{RandomIntFactory.MediumBound} <= {actual[i]} <= {RandomIntFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.LargeBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateStrictlyPositive_Then_WithinNormalBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateStrictlyPositive();
            outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateSmallStrictlyPositive_Then_WithinSmallBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateSmallStrictlyPositive();
            outputHelper.WriteLine($"{RandomIntFactory.Epsilon} <= {actual[i]} <= {RandomIntFactory.SmallBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.Epsilon);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.SmallBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateMediumStrictlyPositive_Then_WithinMediumBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateMediumStrictlyPositive();
            outputHelper.WriteLine($"{RandomIntFactory.SmallBound} <= {actual[i]} <= {RandomIntFactory.MediumBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.SmallBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.MediumBound);
        }
    }

    [Fact]
    public void Given_Nothing_When_CreateLargeStrictlyPositive_Then_WithinLargeBounds()
    {
        // Arrange
        var sut = new RandomIntFactory();

        // Act
        var actual = new int[TestCount];
        for (var i = 0; i < TestCount; i++)
        {
            actual[i] = sut.CreateLargeStrictlyPositive();
            outputHelper.WriteLine($"{RandomIntFactory.MediumBound} <= {actual[i]} <= {RandomIntFactory.LargeBound}");
        }

        // Assert
        for (var i = 0; i < TestCount; i++)
        {
            var item = actual[i];
            item.Should().BeGreaterThanOrEqualTo(RandomIntFactory.MediumBound);
            item.Should().BeLessThanOrEqualTo(RandomIntFactory.LargeBound);
        }
    }
}
