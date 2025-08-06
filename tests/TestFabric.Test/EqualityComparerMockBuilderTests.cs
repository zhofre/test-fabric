namespace TestFabric.Test;

public class EqualityComparerMockBuilderTests
{
    private IFixture _fix = new Fixture();

    [Fact]
    public void Given_Result_When_Equals_Then_Expected()
    {
        // Arrange
        var expected = _fix.Create<bool>();
        var arg1 = _fix.Create<int>();
        var arg2 = _fix.Create<int>();
        var sut = new EqualityComparerMockBuilder<int>()
            .WithEquals(expected)
            .Create();

        // Act
        var actual = sut.Object.Equals(arg1, arg2);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_ResultAndCallback_When_Equals_Then_ExpectedWithDelegateCalled()
    {
        // Arrange
        var callbackExecuted = false;
        var expected = _fix.Create<bool>();
        var arg1 = _fix.Create<int>();
        var arg2 = _fix.Create<int>();
        var sut = new EqualityComparerMockBuilder<int>()
            .WithEquals((x, y) =>
            {
                callbackExecuted = true;
                return expected;
            })
            .Create();

        // Act
        var actual = sut.Object.Equals(arg1, arg2);

        // Assert
        Assert.Equal(expected, actual);
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Result_When_GetHashCode_Then_Expected()
    {
        // Arrange
        var expected = _fix.Create<int>();
        var arg = _fix.Create<int>();
        var sut = new EqualityComparerMockBuilder<int>()
            .WithGetHashCode(expected)
            .Create();

        // Act
        var actual = sut.Object.GetHashCode(arg);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Given_ResultAndCallback_When_GetHashCode_Then_ExpectedWithDelegateCalled()
    {
        // Arrange
        var callbackExecuted = false;
        var expected = _fix.Create<int>();
        var arg = _fix.Create<int>();
        var sut = new EqualityComparerMockBuilder<int>()
            .WithGetHashCode(_ =>
            {
                callbackExecuted = true;
                return expected;
            })
            .Create();

        // Act
        var actual = sut.Object.GetHashCode(arg);

        // Assert
        Assert.Equal(expected, actual);
        Assert.True(callbackExecuted);
    }
}
