namespace TestFabric.Test.Data;

[TestSubject(typeof(Factory))]
public class FactoryTests
{
    private static Factory CreateSut(params IPicker[] pickers)
    {
        return new Factory(
            new Fixture(),
            null,
            pickers);
    }

    [Fact]
    public void Given_Nothing_When_CreatePerson_Then_HasName()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.Create<Person>();

        // Assert
        actual.Name.Should().NotBeNull();
    }

    [Fact]
    public void Given_Nothing_When_CreateManyPerson_Then_Multiple()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>();

        // Assert
        actual.Should().NotBeEmpty();
    }

    [Fact]
    public void Given_Count_When_CreateManyPerson_Then_ExactCount()
    {
        // Arrange
        const int count = 7;
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>(count);

        // Assert
        actual.Count().Should().Be(count);
    }

    [Fact]
    public void Given_MinAndMaxCount_When_CreateManyPerson_Then_InclusiveBetweenCount()
    {
        // Arrange
        const int minCount = 7;
        const int maxCount = 10;
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>(minCount, maxCount);
        var actualCount = actual.Count();

        // Assert
        actualCount.Should().BeGreaterThanOrEqualTo(minCount);
        actualCount.Should().BeLessThanOrEqualTo(maxCount);
    }

    [Fact]
    public void Given_Nothing_When_CreateErrorPerson_Then_FactoryException()
    {
        // Arrange
        var sut = CreateSut();

        // Act + Assert
        Action act1 = () => sut.Create<ErrorPerson>();
        act1.Should().Throw<FactoryException>();
    }

    [Fact]
    public void Given_Nothing_When_CreateManyErrorPerson_Then_FactoryException()
    {
        // Arrange
        var sut = CreateSut();

        // Act + Assert
        Action act2 = () => sut.CreateMany<ErrorPerson>();
        act2.Should().Throw<FactoryException>();
    }

    [Fact]
    public void Given_IntRange_When_CreateFromRange_Then_NoPickerException()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut();

        // Act + Assert
        Action act3 = () => sut.CreateFromRange(minIncl, maxExcl);
        act3.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateFromRange(minIncl, maxExcl);

        // Assert
        actual.Should().BeGreaterThanOrEqualTo(minIncl);
        actual.Should().BeLessThan(maxExcl);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateManyFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(minIncl, maxExcl);

        // Assert
        foreach (var item in actual)
        {
            item.Should().BeGreaterThanOrEqualTo(minIncl);
            item.Should().BeLessThan(maxExcl);
        }
    }

    [Fact]
    public void Given_IntRangeAndPickerAndCount_When_CreateManyFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        const int count = 9;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(count, minIncl, maxExcl);

        // Assert
        actual.Count().Should().Be(count);
        foreach (var item in actual)
        {
            item.Should().BeGreaterThanOrEqualTo(minIncl);
            item.Should().BeLessThan(maxExcl);
        }
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateFromRange(items);

        // Assert
        items.Should().Contain(actual);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateManyFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(items);

        // Assert
        foreach (var item in actual)
        {
            items.Should().Contain(item);
        }
    }

    [Fact]
    public void Given_IntRangeAndPickerAndCount_When_CreateManyFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        const int count = 9;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(count, items);

        // Assert
        actual.Count().Should().Be(count);
        foreach (var item in actual)
        {
            items.Should().Contain(item);
        }
    }

    [Fact]
    public void Given_Nothing_When_BuildPerson_Then_BuilderReturned()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.Build<Person>();

        // Assert
        actual.Should().BeOfType<ObjectBuilder<Person>>();
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_BuildConstrainedFromRange_Then_ConstrainedBuilderOfInt()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.BuildConstrainedFromRange(minIncl, maxExcl);

        // Assert
        actual.Should().BeOfType<ConstrainedBuilder<int>>();
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_BuildConstrainedFromItems_Then_ConstrainedBuilderOfInt()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.BuildConstrainedFromRange(items);

        // Assert
        actual.Should().BeOfType<ConstrainedBuilder<int>>();
    }

    public class Person
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string? Name { get; set; }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public class ErrorPerson : Person
    {
        public ErrorPerson()
        {
            throw new Exception();
        }
    }
}
