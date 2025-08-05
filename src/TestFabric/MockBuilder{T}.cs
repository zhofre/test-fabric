using System.Linq.Expressions;

namespace TestFabric;

public abstract class MockBuilder<T> where T : class
{
    protected Mock<T> Mock { get; } = new(MockBehavior.Strict);

    public Mock<T> Create()
    {
        return Mock;
    }

    #region Properties Setup

    protected void WithProperty<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult initialValue = default)
    {
        Mock.SetupProperty(expression, initialValue);
    }

    protected void WithPropertyGet<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult value)
    {
        Mock.SetupGet(expression).Returns(value);
    }

    protected void WithPropertyGet<TResult>(
        Expression<Func<T, TResult>> expression,
        Func<TResult> @delegate)
    {
        Mock.SetupGet(expression).Returns(@delegate);
    }

    #endregion

    #region Actions Setup

    protected void WithAction(Expression<Action<T>> expression, Action callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithAction<T1>(Expression<Action<T>> expression, Action<T1> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithAction<T1, T2>(Expression<Action<T>> expression, Action<T1, T2> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithAction<T1, T2, T3>(Expression<Action<T>> expression, Action<T1, T2, T3> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithAction<T1, T2, T3, T4>(Expression<Action<T>> expression, Action<T1, T2, T3, T4> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithAction<T1, T2, T3, T4, T5>(Expression<Action<T>> expression,
        Action<T1, T2, T3, T4, T5> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    #endregion

    #region Functions Setup

    protected void WithFunction<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult result)
    {
        Mock.Setup(expression).Returns(result);
    }

    protected void WithFunction<TResult>(
        Expression<Func<T, TResult>> expression,
        params TResult[] resultSequence)
    {
        if (resultSequence == null)
        {
            return;
        }

        var config = Mock.SetupSequence(expression);
        foreach (var result in resultSequence)
        {
            config = config.Returns(result);
        }
    }

    protected void WithFunction<TResult>(
        Expression<Func<T, TResult>> expression,
        Func<TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    protected void WithFunction<T1, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    protected void WithFunction<T1, T2, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    protected void WithFunction<T1, T2, T3, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    protected void WithFunction<T1, T2, T3, T4, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, T4, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    protected void WithFunction<T1, T2, T3, T4, T5, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, T4, T5, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    #endregion

    #region Tasks (async actions) Setup

    protected void WithActionAsync(
        Expression<Func<T, Task>> expression,
        Action callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithActionAsync<T1>(
        Expression<Func<T, Task>> expression,
        Action<T1> callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithActionAsync<T1, T2>(
        Expression<Func<T, Task>> expression,
        Action<T1, T2> callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithActionAsync<T1, T2, T3>(
        Expression<Func<T, Task>> expression,
        Action<T1, T2, T3> callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithActionAsync<T1, T2, T3, T4>(
        Expression<Func<T, Task>> expression,
        Action<T1, T2, T3, T4> callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    protected void WithActionAsync<T1, T2, T3, T4, T5>(
        Expression<Func<T, Task>> expression,
        Action<T1, T2, T3, T4, T5> callback = null)
    {
        var setup = Mock.Setup(expression).Returns(Task.FromResult(false));

        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    #endregion

    #region Tasks (async functions) Setup

    protected void WithFunctionAsync<TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        TResult result)
    {
        Mock.Setup(expression).ReturnsAsync(result);
    }

    protected void WithFunctionAsync<TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        params TResult[] resultSequence)
    {
        if (resultSequence == null)
        {
            return;
        }

        var config = Mock.SetupSequence(expression);
        foreach (var result in resultSequence)
        {
            config = config.ReturnsAsync(result);
        }
    }

    protected void WithFunctionAsync<TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    protected void WithFunctionAsync<T1, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    protected void WithFunctionAsync<T1, T2, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    protected void WithFunctionAsync<T1, T2, T3, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    protected void WithFunctionAsync<T1, T2, T3, T4, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, T4, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    protected void WithFunctionAsync<T1, T2, T3, T4, T5, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, T4, T5, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    #endregion
}
