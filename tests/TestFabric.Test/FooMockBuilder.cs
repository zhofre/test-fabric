namespace TestFabric.Test;

internal class FooMockBuilder : MockBuilder<IFoo>
{
    public FooMockBuilder WithId(int value)
    {
        WithPropertyGet(x => x.Id, value);
        return this;
    }

    public FooMockBuilder WithId(Func<int> @delegate)
    {
        WithPropertyGet(x => x.Id, @delegate);
        return this;
    }

    public FooMockBuilder WithValue(double initialValue)
    {
        WithProperty(x => x.Value, initialValue);
        return this;
    }

    public FooMockBuilder WithDo()
    {
        WithAction(x => x.Do());
        return this;
    }

    public FooMockBuilder WithDo(Action callback)
    {
        WithAction(x => x.Do(), callback);
        return this;
    }

    public FooMockBuilder WithDo(Action<int> callback)
    {
        WithAction(x => x.Do(It.IsAny<int>()), callback);
        return this;
    }

    public FooMockBuilder WithDo(Action<double, bool> callback)
    {
        WithAction(x => x.Do(It.IsAny<double>(), It.IsAny<bool>()), callback);
        return this;
    }

    public FooMockBuilder WithDo(Action<bool, double, string> callback)
    {
        WithAction(x => x.Do(It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>()), callback);
        return this;
    }

    public FooMockBuilder WithDo(Action<string, bool, double, Guid> callback)
    {
        WithAction(x => x.Do(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<Guid>()), callback);
        return this;
    }

    public FooMockBuilder WithDo(Action<Guid, bool, double, string, int> callback)
    {
        WithAction(
            x => x.Do(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<int>()),
            callback);
        return this;
    }

    public FooMockBuilder WithCalculate(int result)
    {
        WithFunction(x => x.Calculate(), result);
        return this;
    }

    public FooMockBuilder WithCalculate(int[] result)
    {
        WithFunction(x => x.Calculate(), result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<int> @delegate)
    {
        WithFunction(x => x.Calculate(), @delegate);
        return this;
    }

    public FooMockBuilder WithCalculate(bool result)
    {
        WithFunction(x => x.Calculate(It.IsAny<double>()), result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<double, bool> @delegate)
    {
        WithFunction(x => x.Calculate(It.IsAny<double>()), @delegate);
        return this;
    }

    public FooMockBuilder WithCalculate(string result)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<double>(), It.IsAny<bool>()),
            result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<double, bool, string> @delegate)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<double>(), It.IsAny<bool>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculate(double result)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>()),
            result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<bool, double, string, double> @delegate)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculate(Guid result)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<Guid>()),
            result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<string, bool, double, Guid, Guid> @delegate)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<Guid>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculate(byte result)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>(),
                It.IsAny<int>()),
            result);
        return this;
    }

    public FooMockBuilder WithCalculate(Func<Guid, bool, double, string, int, byte> @delegate)
    {
        WithFunction(
            x => x.Calculate(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>(),
                It.IsAny<int>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action callback)
    {
        WithActionAsync(x => x.DoAsync(), callback);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action<int> callback)
    {
        WithActionAsync(x => x.DoAsync(It.IsAny<int>()), callback);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action<double, bool> callback)
    {
        WithActionAsync(x => x.DoAsync(It.IsAny<double>(), It.IsAny<bool>()), callback);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action<bool, double, string> callback)
    {
        WithActionAsync(
            x => x.DoAsync(It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>()),
            callback);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action<string, bool, double, Guid> callback)
    {
        WithActionAsync(
            x => x.DoAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<Guid>()),
            callback);
        return this;
    }

    public FooMockBuilder WithDoAsync(Action<Guid, bool, double, string, int> callback)
    {
        WithActionAsync(
            x => x.DoAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<int>()),
            callback);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(int result)
    {
        WithFunctionAsync(x => x.CalculateAsync(), result);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(int[] result)
    {
        WithFunctionAsync(x => x.CalculateAsync(), result);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<int> @delegate)
    {
        WithFunctionAsync(x => x.CalculateAsync(), @delegate);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<double, bool> @delegate)
    {
        WithFunctionAsync(x => x.CalculateAsync(It.IsAny<double>()), @delegate);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<double, bool, string> @delegate)
    {
        WithFunctionAsync(
            x => x.CalculateAsync(It.IsAny<double>(), It.IsAny<bool>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<bool, double, string, double> @delegate)
    {
        WithFunctionAsync(
            x => x.CalculateAsync(It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<string>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<string, bool, double, Guid, Guid> @delegate)
    {
        WithFunctionAsync(
            x => x.CalculateAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<double>(), It.IsAny<Guid>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithCalculateAsync(Func<Guid, bool, double, string, int, byte> @delegate)
    {
        WithFunctionAsync(
            x => x.CalculateAsync(
                It.IsAny<Guid>(),
                It.IsAny<bool>(),
                It.IsAny<double>(),
                It.IsAny<string>(),
                It.IsAny<int>()),
            @delegate);
        return this;
    }

    public FooMockBuilder WithTryGetValue(bool result, int outValue)
    {
        WithTryGet(
            x => x.TryGetValue(out outValue),
            result,
            outValue);
        WithTryGet<string, int>(
            x => x.TryGetValue(It.IsAny<string>(), out outValue),
            result,
            outValue);
        WithTryGet<string, int, int>(
            x => x.TryGetValue(It.IsAny<string>(), It.IsAny<int>(), out outValue),
            result,
            outValue);
        WithTryGet<string, int, bool, int>(
            x => x.TryGetValue(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>(), out outValue),
            result,
            outValue);
        WithTryGet<string, int, bool, double, int>(
            x => x.TryGetValue(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<double>(),
                out outValue),
            result,
            outValue);
        WithTryGet<string, int, bool, double, Guid, int>(
            x => x.TryGetValue(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<double>(),
                It.IsAny<Guid>(),
                out outValue),
            result,
            outValue);
        return this;
    }
}
