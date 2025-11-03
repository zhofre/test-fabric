namespace TestFabric.Test;

public class ObjectEqualityComparerTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Given_NullAndNull_When_Equals_Then_True()
    {
        // Arrange
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(null!, null!);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_FooAndNull_When_Equals_Then_False()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(foo, null!);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_NullAndFoo_When_Equals_Then_False()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(null!, foo);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_Foo_When_EqualsSameReference_Then_True()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(foo, foo);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_FooAndSameFoo_When_EqualsSameReference_Then_True()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var sameFoo = new Foo(foo.Id, "John", 1);
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(foo, sameFoo);

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Given_FooAndOtherFoo_When_EqualsSameReference_Then_True()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var otherFoo = foo with { Age = 3 };
        var sut = new FooEqualityComparer(outputHelper.WriteLine);

        // Act
        var actual = sut.Equals(foo, otherFoo);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Given_Foo_When_GetHashCode_Then_Same()
    {
        // Arrange
        var foo = new Foo(Guid.NewGuid(), "John", 1);
        var sut = new FooEqualityComparer(outputHelper.WriteLine);
        var expected = foo.GetHashCode();

        // Act
        var actual = sut.GetHashCode(foo);

        // Assert
        actual.Should().Be(expected);
    }

    public record Foo(Guid Id, string Name, int Age);

    public class FooEqualityComparer(Action<string>? writeLine = null) : ObjectEqualityComparer<Foo>(writeLine)
    {
        protected override bool EqualsImpl(Foo x, Foo y)
        {
            WriteLine?.Invoke($"Comparing {x.Id} and {y.Id}");
            return x.Id == y.Id && x.Name == y.Name && x.Age == y.Age;
        }
    }
}
