namespace TestFabric.Test;

public class MockBuilderTests
{
    private readonly IFixture _fix = new Fixture();

    [Fact]
    public void Given_Nothing_When_Build_Then_MockOfCorrectType()
    {
        // Arrange
        var sut = new FooMockBuilder();

        // Act
        var actual = sut.Create();

        // Assert
        Assert.IsType<Mock<IFoo>>(actual);
        Assert.IsAssignableFrom<IFoo>(actual.Object);
    }

    [Fact]
    public void Given_Id_When_ConfigureGetIdAndBuild_Then_MockReturnsId()
    {
        // Arrange
        var expectedId = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithId(expectedId);

        // Act
        var actual = sut.Create();

        // Assert
        Assert.Equal(expectedId, actual.Object.Id);
    }

    [Fact]
    public void Given_Id_When_ConfigureGetIdFromFuncAndBuild_Then_MockReturnsId()
    {
        // Arrange
        var expectedId = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithId(() => expectedId);

        // Act
        var actual = sut.Create();

        // Assert
        Assert.Equal(expectedId, actual.Object.Id);
    }

    [Fact]
    public void Given_Value_When_ConfigureValueProperty_Then_CanSetAndGetValue()
    {
        // Arrange
        var expectedValue = _fix.Create<double>();
        var sut = new FooMockBuilder()
            .WithValue(0.0);

        // Act
        var actual = sut.Create();
        actual.Object.Value = expectedValue;

        // Assert
        Assert.Equal(expectedValue, actual.Object.Value, 6);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithoutArguments_Then_NoException()
    {
        // Arrange
        var sut = new FooMockBuilder()
            .WithDo();

        // Act
        var actual = sut.Create();
        actual.Object.Do();
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithoutArgumentsAndWithCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var sut = new FooMockBuilder()
            .WithDo(() => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do();

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithOneArgumentAndCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithDo(_ => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do(arg1);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithTwoArgumentsAndCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<double>();
        var arg2 = _fix.Create<bool>();
        var sut = new FooMockBuilder()
            .WithDo((_, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do(arg1, arg2);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithThreeArgumentsAndCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<bool>();
        var arg2 = _fix.Create<double>();
        var arg3 = _fix.Create<string>();
        var sut = new FooMockBuilder()
            .WithDo((_, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do(arg1, arg2, arg3);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithFourArgumentsAndCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<string>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<Guid>();
        var sut = new FooMockBuilder()
            .WithDo((_, _, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do(arg1, arg2, arg3, arg4);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Nothing_When_ConfigureDoWithFiveArgumentsAndCallback_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<Guid>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<string>();
        var arg5 = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithDo((_, _, _, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        actual.Object.Do(arg1, arg2, arg3, arg4, arg5);

        // Assert
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculate_Then_MockReturnsResult()
    {
        // Arrange
        var expectedResult = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate();

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateFromArray_Then_ArrayContentReturned()
    {
        // Arrange
        var expectedResult = _fix.CreateMany<int>(3)
            .ToArray();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual1 = mock.Object.Calculate();
        var actual2 = mock.Object.Calculate();
        var actual3 = mock.Object.Calculate();

        // Assert
        Assert.Equal(expectedResult[0], actual1);
        Assert.Equal(expectedResult[1], actual2);
        Assert.Equal(expectedResult[2], actual3);
    }

    [Fact]
    public void Given_EmptyResultSequence_When_ConfigureCalculateFromArray_Then_NotConfigured()
    {
        // Arrange
        int[]? expectedResult = null;
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult!);

        // Act + Assert
        var mock = sut.Create();
        Assert.Throws<MockException>(() => mock.Object.Calculate());
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var expectedResult = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithCalculate(() => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate();

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateWithOneArgument_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var expectedResult = _fix.Create<bool>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateWithOneArgumentFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var expectedResult = _fix.Create<bool>();
        var sut = new FooMockBuilder()
            .WithCalculate(_ => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateWithTwoArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var arg2 = _fix.Create<bool>();
        var expectedResult = _fix.Create<string>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateWithTwoArgumentsFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var arg2 = _fix.Create<bool>();
        var expectedResult = _fix.Create<string>();
        var sut = new FooMockBuilder()
            .WithCalculate((_, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateWithThreeArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<bool>();
        var arg2 = _fix.Create<double>();
        var arg3 = _fix.Create<string>();
        var expectedResult = _fix.Create<double>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateWithThreeArgumentsFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<bool>();
        var arg2 = _fix.Create<double>();
        var arg3 = _fix.Create<string>();
        var expectedResult = _fix.Create<double>();
        var sut = new FooMockBuilder()
            .WithCalculate((_, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateWithFourArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<string>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<Guid>();
        var expectedResult = _fix.Create<Guid>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3, arg4);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateWithFourArgumentsFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<string>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<Guid>();
        var expectedResult = _fix.Create<Guid>();
        var sut = new FooMockBuilder()
            .WithCalculate((_, _, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3, arg4);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Result_When_ConfigureCalculateWithFiveArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<Guid>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<string>();
        var arg5 = _fix.Create<int>();
        var expectedResult = _fix.Create<byte>();
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3, arg4, arg5);

        // Assert
        Assert.Equal(expectedResult, actual);
    }

    [Fact]
    public void Given_Delegate_When_ConfigureCalculateWithFiveArgumentsFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<Guid>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<string>();
        var arg5 = _fix.Create<int>();
        var expectedResult = _fix.Create<byte>();
        var sut = new FooMockBuilder()
            .WithCalculate((_, _, _, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = mock.Object.Calculate(arg1, arg2, arg3, arg4, arg5);

        // Assert
        Assert.Equal(expectedResult, actual);
    }
}
