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
        actual.Should().BeOfType<Mock<IFoo>>();
        actual.Object.Should().BeAssignableTo<IFoo>();
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
        actual.Object.Id.Should().Be(expectedId);
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
        actual.Object.Id.Should().Be(expectedId);
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
        actual.Object.Value.Should().BeApproximately(expectedValue, 0.000001);
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
        callbackExecuted.Should().BeTrue();
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
        callbackExecuted.Should().BeTrue();
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
        callbackExecuted.Should().BeTrue();
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
        callbackExecuted.Should().BeTrue();
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
        callbackExecuted.Should().BeTrue();
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
        callbackExecuted.Should().BeTrue();
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
        actual.Should().Be(expectedResult);
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
        actual1.Should().Be(expectedResult[0]);
        actual2.Should().Be(expectedResult[1]);
        actual3.Should().Be(expectedResult[2]);
    }

    [Fact]
    public async Task Given_EmptyResultSequence_When_ConfigureCalculateFromArray_Then_NotConfigured()
    {
        // Arrange
        int[]? expectedResult = null;
        var sut = new FooMockBuilder()
            .WithCalculate(expectedResult!);

        // Act + Assert
        var mock = sut.Create();
        Func<Task> f = async () => await mock.Object.CalculateAsync();
        await f.Should().ThrowAsync<MockException>();
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
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
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsync_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var sut = new FooMockBuilder()
            .WithDoAsync(() => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync();

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsyncWithOneArgument_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithDoAsync(_ => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync(arg1);

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsyncWithTwoArguments_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<double>();
        var arg2 = _fix.Create<bool>();
        var sut = new FooMockBuilder()
            .WithDoAsync((_, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync(arg1, arg2);

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsyncWithThreeArguments_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<bool>();
        var arg2 = _fix.Create<double>();
        var arg3 = _fix.Create<string>();
        var sut = new FooMockBuilder()
            .WithDoAsync((_, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync(arg1, arg2, arg3);

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsyncWithFourArguments_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<string>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<Guid>();
        var sut = new FooMockBuilder()
            .WithDoAsync((_, _, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync(arg1, arg2, arg3, arg4);

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Nothing_When_ConfigureDoAsyncWithFiveArguments_Then_CallbackExecuted()
    {
        // Arrange
        var callbackExecuted = false;
        var arg1 = _fix.Create<Guid>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<string>();
        var arg5 = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithDoAsync((_, _, _, _, _) => { callbackExecuted = true; });

        // Act
        var actual = sut.Create();
        await actual.Object.DoAsync(arg1, arg2, arg3, arg4, arg5);

        // Assert
        callbackExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsync_Then_MockReturnsResult()
    {
        // Arrange
        var expectedResult = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync(expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync();

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Results_When_ConfigureCalculateAsync_Then_MockReturnsResult()
    {
        // Arrange
        var expectedResult = _fix.CreateMany<int>(3)
            .ToArray();
        var sut = new FooMockBuilder()
            .WithCalculateAsync(expectedResult);

        // Act
        var mock = sut.Create();
        var actual1 = await mock.Object.CalculateAsync();
        var actual2 = await mock.Object.CalculateAsync();
        var actual3 = await mock.Object.CalculateAsync();

        // Assert
        actual1.Should().Be(expectedResult[0]);
        actual2.Should().Be(expectedResult[1]);
        actual3.Should().Be(expectedResult[2]);
    }

    [Fact]
    public async Task Given_EmptyResultSequence_When_ConfigureCalculateAsync_Then_NotConfigured()
    {
        // Arrange
        int[]? expectedResult = null;
        var sut = new FooMockBuilder()
            .WithCalculateAsync(expectedResult!);

        // Act + Assert
        var mock = sut.Create();
        Func<Task> f = () => mock.Object.CalculateAsync();
        await f.Should().ThrowAsync<MockException>();
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFunc_Then_MockReturnsResult()
    {
        // Arrange
        var expectedResult = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync(() => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync();

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFuncWithOneArgument_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var expectedResult = _fix.Create<bool>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync(_ => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync(arg1);

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFuncWithTwoArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<double>();
        var arg2 = _fix.Create<bool>();
        var expectedResult = _fix.Create<string>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync((_, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync(arg1, arg2);

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFuncWithThreeArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<bool>();
        var arg2 = _fix.Create<double>();
        var arg3 = _fix.Create<string>();
        var expectedResult = _fix.Create<double>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync((_, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync(arg1, arg2, arg3);

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFuncWithFourArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<string>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<Guid>();
        var expectedResult = _fix.Create<Guid>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync((_, _, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync(arg1, arg2, arg3, arg4);

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public async Task Given_Result_When_ConfigureCalculateAsyncFromFuncWithFiveArguments_Then_MockReturnsResult()
    {
        // Arrange
        var arg1 = _fix.Create<Guid>();
        var arg2 = _fix.Create<bool>();
        var arg3 = _fix.Create<double>();
        var arg4 = _fix.Create<string>();
        var arg5 = _fix.Create<int>();
        var expectedResult = _fix.Create<byte>();
        var sut = new FooMockBuilder()
            .WithCalculateAsync((_, _, _, _, _) => expectedResult);

        // Act
        var mock = sut.Create();
        var actual = await mock.Object.CalculateAsync(arg1, arg2, arg3, arg4, arg5);

        // Assert
        actual.Should().Be(expectedResult);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValue_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValueWithOneArgument_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(
            _fix.Create<string>(),
            out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValueWithTwoArguments_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(
            _fix.Create<string>(),
            _fix.Create<int>(),
            out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValueWithThreeArguments_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(
            _fix.Create<string>(),
            _fix.Create<int>(),
            _fix.Create<bool>(),
            out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValueWithFourArguments_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(
            _fix.Create<string>(),
            _fix.Create<int>(),
            _fix.Create<bool>(),
            _fix.Create<double>(),
            out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }

    [Fact]
    public void Given_ResultAndOutValue_When_ConfigureTryGetValueWithFiveArguments_Then_MockReturnsResultAndOutValue()
    {
        // Arrange
        var result = _fix.Create<bool>();
        var outValue = _fix.Create<int>();
        var sut = new FooMockBuilder()
            .WithTryGetValue(result, outValue);

        // Act
        var mock = sut.Create();
        var actualResult = mock.Object.TryGetValue(
            _fix.Create<string>(),
            _fix.Create<int>(),
            _fix.Create<bool>(),
            _fix.Create<double>(),
            _fix.Create<Guid>(),
            out var actualOutValue);

        // Assert
        actualResult.Should().Be(result);
        actualOutValue.Should().Be(outValue);
    }
}
