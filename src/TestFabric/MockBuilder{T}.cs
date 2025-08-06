using System.Linq.Expressions;

namespace TestFabric;

/// <summary>
///     Abstract class for building mock objects of type T.
///     This builder provides a fluent interface to configure and define the behavior of mock objects.
/// </summary>
/// <typeparam name="T">Type of the object to mock.</typeparam>
public abstract class MockBuilder<T> where T : class
{
    /// <summary>
    ///     Represents a builder for creating and configuring mock objects of type T.
    ///     Can be accessed from implementations for custom mock object configuration and setup.
    /// </summary>
    protected Mock<T> Mock { get; } = new(MockBehavior.Strict);

    /// <summary>
    ///     Creates and returns the configured mock object of type T.
    /// </summary>
    /// <returns>A mock object of type T.</returns>
    public Mock<T> Create()
    {
        return Mock;
    }

    #region Properties Setup

    /// <summary>
    ///     Configures the mock object to have a property with an initial value.
    /// </summary>
    /// <param name="expression">Lambda expression specifying the property.</param>
    /// <param name="initialValue">The initial value for the property.</param>
    protected void WithProperty<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult initialValue = default)
    {
        Mock.SetupProperty(expression, initialValue);
    }

    /// <summary>
    ///     Sets up a getter for a property on the mock object.
    /// </summary>
    /// <param name="expression">An expression representing the property to set up.</param>
    /// <param name="value">The value to return when the property is accessed.</param>
    protected void WithPropertyGet<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult value)
    {
        Mock.SetupGet(expression).Returns(value);
    }

    /// <summary>
    ///     Configures the mock to return a value for a property's getter based on a delegate.
    /// </summary>
    /// <param name="expression">The expression representing the property.</param>
    /// <param name="delegate">A delegate that returns the value to be returned by the property's getter.</param>
    protected void WithPropertyGet<TResult>(
        Expression<Func<T, TResult>> expression,
        Func<TResult> @delegate)
    {
        Mock.SetupGet(expression).Returns(@delegate);
    }

    #endregion

    #region Actions Setup

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (0 arguments).
    /// </summary>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction(Expression<Action<T>> expression, Action callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (1 argument).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction<T1>(Expression<Action<T>> expression, Action<T1> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (2 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction<T1, T2>(Expression<Action<T>> expression, Action<T1, T2> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (3 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction<T1, T2, T3>(Expression<Action<T>> expression, Action<T1, T2, T3> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (4 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction<T1, T2, T3, T4>(Expression<Action<T>> expression, Action<T1, T2, T3, T4> callback = null)
    {
        var setup = Mock.Setup(expression);
        if (callback != null)
        {
            setup.Callback(callback);
        }
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when a method is called (5 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method call.</param>
    /// <param name="callback">
    ///     The action to be executed when the method is called. The parameters of the callback should match
    ///     those of the method being mocked.
    /// </param>
    protected void WithAction<T1, T2, T3, T4, T5>(
        Expression<Action<T>> expression,
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

    /// <summary>
    ///     Configures the mock object to return a specific value when the specified function is called.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">The expression representing the function to configure.</param>
    /// <param name="result">The result that the function should return.</param>
    protected void WithFunction<TResult>(
        Expression<Func<T, TResult>> expression,
        TResult result)
    {
        Mock.Setup(expression).Returns(result);
    }

    /// <summary>
    ///     Configures a mock to return a sequence of results for the specified function.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">The expression representing the method call.</param>
    /// <param name="resultSequence">An array of results to be returned sequentially when the function is called.</param>
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

    /// <summary>
    ///     Configures and sets up a function for the mock object (0 arguments).
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<TResult>(
        Expression<Func<T, TResult>> expression,
        Func<TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    /// <summary>
    ///     Configures and sets up a function for the mock object (1 argument).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<T1, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    /// <summary>
    ///     Configures and sets up a function for the mock object (2 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<T1, T2, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    /// <summary>
    ///     Configures and sets up a function for the mock object (3 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<T1, T2, T3, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    /// <summary>
    ///     Configures and sets up a function for the mock object (4 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<T1, T2, T3, T4, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, T4, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    /// <summary>
    ///     Configures and sets up a function for the mock object (5 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="delegate">Delegate that defines the behavior of the mocked function.</param>
    protected void WithFunction<T1, T2, T3, T4, T5, TResult>(
        Expression<Func<T, TResult>> expression,
        Func<T1, T2, T3, T4, T5, TResult> @delegate)
    {
        Mock.Setup(expression).Returns(@delegate);
    }

    #endregion

    #region Tasks (async actions) Setup

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (0 arguments).
    /// </summary>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (1 argument).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (2 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (3 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (4 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures an asynchronous action for the specified expression on the mock object (5 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth argument.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="callback">The callback to invoke when the method is executed.</param>
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

    /// <summary>
    ///     Configures the mock to return a specific value asynchronously when the specified function is called.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to configure.</param>
    /// <param name="result">The result to return asynchronously from the method.</param>
    protected void WithFunctionAsync<TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        TResult result)
    {
        Mock.Setup(expression).ReturnsAsync(result);
    }

    /// <summary>
    ///     Configures the mock object to return a sequence of results asynchronously when the specified function is called.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="resultSequence">An array of results to be returned in sequence.</param>
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

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (0 arguments).
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (1 argument).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<T1, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (2 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<T1, T2, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (3 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<T1, T2, T3, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (4 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<T1, T2, T3, T4, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, T4, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    /// <summary>
    ///     Configures the mock object to asynchronously return a value for a given method (5 arguments).
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <typeparam name="T5">The type of the fifth argument.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the function.</typeparam>
    /// <param name="expression">Lambda expression representing the method to be configured.</param>
    /// <param name="delegate">The delegate that specifies the value or behavior to return when the method is called.</param>
    protected void WithFunctionAsync<T1, T2, T3, T4, T5, TResult>(
        Expression<Func<T, Task<TResult>>> expression,
        Func<T1, T2, T3, T4, T5, TResult> @delegate)
    {
        Mock.Setup(expression).ReturnsAsync(@delegate);
    }

    #endregion

    #region TryGet Functions Setup

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<TOut>(out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="arg1">The first input parameter.</param>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<in T1, TOut>(T1 arg1, out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<T1, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (T1 _, out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<T1, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<T1, TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="arg1">The first input parameter.</param>
    /// <param name="arg2">The second input parameter.</param>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<in T1, in T2, TOut>(T1 arg1, T2 arg2, out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<T1, T2, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (T1 _, T2 _, out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<T1, T2, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<T1, T2, TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="arg1">The first input parameter.</param>
    /// <param name="arg2">The second input parameter.</param>
    /// <param name="arg3">The third input parameter.</param>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<in T1, in T2, in T3, TOut>(T1 arg1, T2 arg2, T3 arg3, out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<T1, T2, T3, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (T1 _, T2 _, T3 _, out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<T1, T2, T3, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<T1, T2, T3, TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="arg1">The first input parameter.</param>
    /// <param name="arg2">The second input parameter.</param>
    /// <param name="arg3">The third input parameter.</param>
    /// <param name="arg4">The fourth input parameter.</param>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<in T1, in T2, in T3, in T4, TOut>(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<T1, T2, T3, T4, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (T1 _, T2 _, T3 _, T4 _, out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<T1, T2, T3, T4, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<T1, T2, T3, T4, TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    /// <summary>
    ///     Delegate representing a method that returns an output value based on input parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="T5">Type of the fifth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="arg1">The first input parameter.</param>
    /// <param name="arg2">The second input parameter.</param>
    /// <param name="arg3">The third input parameter.</param>
    /// <param name="arg4">The fourth input parameter.</param>
    /// <param name="arg5">The fifth input parameter.</param>
    /// <param name="outValue">Output value to be returned by the method.</param>
    protected delegate void OutCallback<in T1, in T2, in T3, in T4, in T5, TOut>(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        out TOut outValue);

    /// <summary>
    ///     Configures the mock to have a method that returns a value based on a condition and an output parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="T5">Type of the fifth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression representing the method to configure.</param>
    /// <param name="result">The result of the condition check (true or false).</param>
    /// <param name="outValue">The value to be returned when the condition is true.</param>
    protected void WithTryGet<T1, T2, T3, T4, T5, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        TOut outValue)
    {
        WithTryGet(
            expression,
            result,
            (T1 _, T2 _, T3 _, T4 _, T5 _, out TOut y) =>
            {
                y = outValue;
            });
    }

    /// <summary>
    ///     Sets up the mock to return a specified value when calling a method that returns a boolean result and an output
    ///     parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the first input parameter.</typeparam>
    /// <typeparam name="T2">Type of the second input parameter.</typeparam>
    /// <typeparam name="T3">Type of the third input parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth input parameter.</typeparam>
    /// <typeparam name="T5">Type of the fifth input parameter.</typeparam>
    /// <typeparam name="TOut">Type of the output value.</typeparam>
    /// <param name="expression">The expression that represents the method to be mocked.</param>
    /// <param name="result">The boolean result to be returned by the method.</param>
    /// <param name="callback">The delegate that sets up the output parameter value.</param>
    protected void WithTryGet<T1, T2, T3, T4, T5, TOut>(
        Expression<Func<T, bool>> expression,
        bool result,
        OutCallback<T1, T2, T3, T4, T5, TOut> callback)
    {
        Mock
            .Setup(expression)
            .Callback(callback)
            .Returns(result);
    }

    #endregion
}
